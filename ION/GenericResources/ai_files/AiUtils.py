###############################################################################
# This file is responsible for handling the batch export of .ai files to either
# PNGs or PDF image files.
# 
# Change Log:
#   12 Aug 2015:
#     Gensis
###############################################################################

import os
from subprocess import call
import sys

INKSCAPE='/Applications/Inkscape.app/Contents/Resources/bin/inkscape'

def export_to_png(inputFile, outputDir, temporaryDir='/output', width=-1, height=-1):
  '''
  Given a valid Adobe Illustrator file, we will convert the file in to a temporary
  SVG and the export the SVG to a PNG. The size of the exported png will be equal
  the given width and height. If a width or height is given and the other dimension
  is -1, then the export will automatically size the PNG to its desired size. This
  is useful for images whose size are not square. If both width and height are
  -1, then the image will be exported as a 64x64 pixel square. 
  '''
  if not os.path.exists(temporaryDir):
    print 'Temporary output directory not valid. Creating new one.'
    os.makedirs(temporaryDir)

  fn = get_file_name(inputFile)[0]
  tempFile = os.path.join(temporaryDir, fn + '.svg')

  # Convert the illustrator file into a SVG file
  optInFile = '-f ' + inputFile
  optOutFile = ' -l ' + tempFile
  ret_code = call([INKSCAPE, optInFile, optOutFile])
  if ret_code != 0:
    print 'Failed to convert', inputFile
    return 1

  # Convert the SVG file to a PNG
  print 'Converting', fn, 'to PNG...'
  ret_code = call([INKSCAPE, '--export-png', os.path.join(outputDir, fn + '.png'), optSizes, tempFile])
  if ret_code != 0:
    print 'Failed to export', fn, 'to PNG'
    return 1

def get_file_name(path):
  '''
  Queries the name of the file using the given path. If the path has a file
  extension, the extension will be removed and just the file name will be returned.
  '''
  return os.path.splitext(os.path.basename(path))