#!usr/bin/env python

'''
This program will export all of the .ai files within a given directory to .png
files of the given dimensions.

Example Usage:
  python export_ai_to_size.py "/path/to/ai/files" -w 64 -h 64

where 'w' and 'h' are the width and height respectively of the output .png in
pixels. If a width and/or a height are not provided, they will default to 64.
'''

import sys, getopt 

class ExecState:
  def __init__(self, directory=".", outputDirectory=".", w=64, h=64):
    self.directory = directory
    self.outputDirectory = outputDirectory
    self.w = w
    self.h = h

  def __str__(self):
    return "ExecState {directory: " + self.directory + " outputDirectory: " + self.outputDirectory + \
      ", w: " + str(self.w) + ", h: " + str(self.h) + "}"

def main(directory, argv):
  try:
    opts, args = getopt.getopt(argv, "w:h:o:",["width=", "height=", "output", "help"])
  except getopt.GetoptError:
    printUsage()
    sys.exit(1)

  state = ExecState()
  state.directory = directory

  for opt, arg, in opts:
    print 'arg is ' + arg
    if opt == '--help':
      printUsage()
      sys.exit()

    #####
    # Operations that require an argument
    #####
    if (arg != None and arg != ''):
      if opt in ('-w', '--width'):
        state.w = int(arg)
      elif opt in ('-h', '--height'):
        state.h = int(arg)
      elif opt in ('-o', '--output'):
        state.outputDirectory = arg

  print state
   

def printUsage():
  print ""
  print "Example Usage [export_ai_to_size]:"
  print "  python export_ai_to_size.py \"path/to/ai/files\" -w <pixelWidth> -h <pixelHeight>"
  print ""
  print "Possible commands"
  print "  --help       : does le help show"
  print "  -w, --width  : the width of the output png. defaults to 64 pixels"
  print "  -h, --height : the height of the output png. defaults to 64 pixels"
  print "  -o,--output  : the directory that the rendered files will be output to." 
  print ""


if __name__ == '__main__':
  main(sys.argv[1], sys.argv[2:]);

