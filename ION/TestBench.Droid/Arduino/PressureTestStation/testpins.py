'''

import serial

def write(s, msg):
    s.write(msg)
    s.flush()
    return s.readline()

def test(host):
    s = serial.Serial(host, timeout=10)

    print("*IDN?", '\\r\\n', write(s, b'HC_OFF\r\n'))
    print("*IDN?", '\\r', write(s, b'HC_OFF\r'))
    print("*IDN?", '\\n', write(s, b'HC_OFF\n'))

    s.close()
'''


import time
import serial

#addr = input('Enter COM: ')
addr = '/dev/tty.usbserial-DN00S0FK'
s = serial.Serial(port=addr, baudrate=9600, timeout=15, parity=serial.PARITY_NONE, stopbits=serial.STOPBITS_ONE, bytesize=serial.EIGHTBITS)

print('"Q" will quit')

run = True
while run:
    i = input('>> ')
    if i == 'Q' or i == 'q' or i == 'exit':
        run = False
        s.close()
        exit()
    else:
        out = s.write(bytes(i + '\r\n', 'ascii'))
        print('wrote:', out, 'bytes')
        time.sleep(0.5)
        print(s.readline())
