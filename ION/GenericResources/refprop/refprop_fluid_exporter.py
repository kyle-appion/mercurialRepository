
'''
This file is responsbile for exporting the REFPROP fluids into a data file that
ION will use to calculate fluid properties.

Output Format:
PL| Identifier | Bytes
--+------------+---------------------------------------------------------------
0   Version     1 (u8)
1   Name Len    1 (u8)
2   Name        len(1) as ASCII chars
3   tmin        4 (i32)
4   tmax        4 (i32)
5   step        4 (f32)
6   row count   4 (u32)
7   state fluid 1 (boolean: true = uses dew/bubble two data columns, false = one data column)
IF NOT FLUID IS STATE FLUID
8  pressures   len(6) * 4 as f32 array
ELSE IF THE FLUID IS A STATE FLUID
9  pressures   len(6) * 4 * 2 as f32 array
Units
----------------------------------------------------------------------------
temperature                         K
pressure, fugacity                  kPa
density                             mol/L
x                                   mole fraction
quality                             mole basis (moles vapor/total moles)
enthalpy, internal energy           J/mol
Gibbs, Helmholtz free energy        J/mol
entropy, heat capacity              J/(mol.K)
speed of sound                      m/s
Joule-Thompson coefficient          K/kPa
d(p)/d(rho)                         kPa.L/mol
d2(p)/d(rho)2                       kPa.(L/mol)^2
viscosity                           microPa.s (10^-6 Pa.s)
thermal conductivity                W/(m.K)
dipole moment                       debye
surface Tension                     N/m
----------------------------------------------------------------------------

Funcions used and their line numbers:
    fluidlib()      639     Gets fluids list
    setup()         1590    Setups the models for a fluid
    press()         2433    Calculates pressure
    satt()          2841    Calculates saturated pressure given a temperature
    satp()          2889    Calculates saturated temperature given a pressure
    limits()        4484    Returns the limits of the fluid
'''

import math
import os
import shutil
import struct
import sys
import traceback
import csv

# Include the refprop libs
sys.path.insert(0, 'libs')
import refprop as r

VERSION = 1
CWD = os.getcwd()
EPSILON = 0.005 # the allowance of float precision error
OUT_PATH = './output/' # the out directory for the data files
OUT_EXT = '.fluid' # the file extension for the converted files

FLAG_NONE = 0
FLAG_EXPLOSIVE = 1

ALIASES = dict([('PROPANE','R290'), ('BUTANE','R600'), ('ISOBUTAN', 'R600a'), ('PENTANE', 'R601'), ('IPENTANE', 'R601a'), \
                ('ETHANE', 'R170'), ('METHANE','R50'), ('OXYGEN', 'R732'), \
                ('AMMONIA', 'R717'), ('WATER', 'R718'), ('NITROGEN', 'R728'), ('ETHYLENE', 'R1150'), \
                ('S02', 'R764'), ('HYDROGEN', 'R702'), ('NEON', 'R720'), ('PROPYLEN', 'R1270'), \
                ('AIR', 'R759'), \
                ('ARGON', 'R740'), ('CO2', 'R744'), ('N20', '744a'), ('R134A', 'R134a')])

def run():
    '''
    Initializes Refprop
    '''
    print 'Working in directory ', CWD + "/"
    r.setpath(CWD + "/")

    if not r.test():
        print 'Failed to init refprop.py. Exiting...'
        sys.exit(1)

    fluid_lib = r.fluidlib()
    fluids = fluid_lib[CWD + '/fluids/']
    fluids = fluids + fluid_lib[CWD + '/mixtures/']
    print fluids, len(fluids)

    out = OUT_PATH

    if os.path.exists(out):
        shutil.rmtree(out)
    os.makedirs(out)

    failures = []

    with open('refrigerant_safety.csv') as f:
        SAFETY = dict(filter(None, csv.reader(f)))

    for fluid in fluids:
        tries = 0;
        if not convert_fluid(fluid, OUT_PATH):
            failures.append(fluid)

#    if not convert_fluid(u'AIR', OUT_PATH):
#        failures.append(u'AIR')

    print '\n\nFailed to create the following fluids\n'
    for fluid in failures:
        print fluid

def convert_fluid(fluid_name, out_path, step=0.25):
    '''
    Converts the fluid to the the binary format
    '''
#    if 'R' != fluid_name[0]:
#        print 'Ingnoring irrelevant refrigerant', fluid_name
#        return
    try:
        # Prepare the fluid in Refprop
        fluid = r.setup(u'def', fluid_name)
        if fluid_name in ALIASES:
            fluid_name = ALIASES[fluid_name]
        else:
            if u'.PPF' in fluid_name:
                fluid_name.replace(u'.PPF', '')

        # remove any unnecessary file extensions
        #fluid_name = r.name()['hname']
        print 'Converting ' + fluid_name

        # Get some of the meta data for the fluid
        x = [] # the fluid composition (mole fractions of mixtures). Empty for single fluid
        if 'x' in fluid:
            x = fluid['x']
        limits = r.limits(x)
        info = r.info()

#        print fluid_name, 'limits are', limits

        tmin = math.ceil(float(limits['tmin']))
        tmax = math.floor(float(r.critp(x)['tcrit']))
        tmax = math.floor(float(limits['tmax']))

        delta = (tmax - tmin) - 2
        rows = int(delta / step)
        tmax = tmin + (rows * step) # snap tmax down to step interval

        bubble = []
        dew = []

        rowsOut = 0
        for i in range(rows):
            temp = tmin + (step * i)

            try:
                bapp = r.satt(temp, x, kph=1)['p']
                dapp = r.satt(temp, x, kph=2)['p']

                bubble.append(bapp)
                dew.append(dapp)

                rowsOut = rowsOut + 1
            except:
                if rowsOut > 0:
#                    print 'Failed to get saturated temperature. Finalizing fluid at current row'
                    break

        if rowsOut <= 0:
            print 'Failed to export fluid {0} with expected rows {1}: \n{2}'.format(fluid_name, rows, traceback.format_exc())
            return False
#        else:
#            print fluid_name, 'exported', rowsOut, 'rows of lookup data with tmin=', tmin, 'and tmax=', tmax, 'and expected rows=', rows

        similar = True
        # Check if the fluid has a pressure difference for the different phases
        for i in range(rowsOut):
            similar = math.fabs(bubble[i] - dew[i]) <= EPSILON
            if not similar:
                print fluid_name, 'failed to be similar at temp', (i * step + tmin)
                break

        # Write the fluid to its file
        file = open(out_path + fluid_name + OUT_EXT, 'wb')
        data = None
        vals = bubble

        if similar:
            fmt = '>BB{0}siifI?{1}f'.format(len(fluid_name), str(rowsOut))
        else:
            fmt = '>BB{0}siifI?{1}f'.format(len(fluid_name), str(rowsOut * 2))
            vals = vals + dew

        data = struct.pack(fmt, VERSION, len(fluid_name), str(fluid_name), tmin, tmax, step, rowsOut, similar, *vals)

        file.write(data)
        file.flush()
        file.close()
        return True
    except Exception, e:
        print 'Failed to convert fluid {0}: \n{1}'.format(fluid_name, traceback.format_exc())
        return False

if __name__ == '__main__':
    run()
