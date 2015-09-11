###############################################################################
# This file is responsible exporting .ai files to a square .png file.
# Change Log:
#   28 July 2015:
#     Genesis
###############################################################################


# Expects an integer string indicating the pixel size of the export.
convertPng() {
  for file in *.ai
  do
    dir=`pwd`
    mkdir -p "${dir}/output"
    fn=${file%%.*}

    mkdir -p "${dir}/output/${1}"
    echo "Converting {$file}"
    /Applications/Inkscape.app/Contents/Resources/bin/inkscape -f "${dir}/${file}" -l "${dir}/${fn}.svg" 
    if [ $? -ne 0 ]; then
      echo "Failed to convert ${file}"
      return
    fi 
    
    echo "Writing ${file}"
    /Applications/Inkscape.app/Contents/Resources/bin/inkscape --export-png "${dir}/output/${1}/${fn}.png" -w $1 -h $1 "${dir}/${fn}.svg"
    if [ $? -ne 0 ]; then
      echo "Failed to export {$file}"
    fi 
    rm "${dir}/${fn}.svg"
  done
}

convertPdf() {
  for file in *.ai
  do
    dir=`pwd`
    output="pdf"
    mkdir -p "${dir}/output"
    fn=${file%%.*}

    mkdir -p "${dir}/output/${output}"
    echo "Converting {$file}"
    /Applications/Inkscape.app/Contents/Resources/bin/inkscape -f "${dir}/${file}" -l "${dir}/${fn}.svg" 
    if [ $? -ne 0 ]; then
      echo "Failed to convert ${file}"
      return
    fi 
    
    echo "Writing ${file}"
    /Applications/Inkscape.app/Contents/Resources/bin/inkscape --export-pdf "${dir}/output/${output}/${fn}.pdf" -w $1 -h $1 "${dir}/${fn}.svg"
    if [ $? -ne 0 ]; then
      echo "Failed to export {$file}"
    fi 
    rm "${dir}/${fn}.svg"
  done
}

convertPdf
#convertPng 32
#convertPng 64
#convertPng 96