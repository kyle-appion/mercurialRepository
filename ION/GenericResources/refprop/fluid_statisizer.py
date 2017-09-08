
import os


def statistize(dirpath = '../../ION.Core/src/Fluids/Embedded'):
  fluids = []
  tot = 0
  for dp, dn, files in os.walk(dirpath):
    print 'Walking:', dp, dn
    for f in files:
      if f.endswith(".fluid") and not '.PPF' in f:
        path = os.path.join(dp, f)
        size = os.stat(path).st_size # in bytes
        fluids.append(Fluid(f, size))
        tot += size
    break

  fluids.sort(key=lambda x: x.size, reverse=False)

  for f in fluids:
    print f.name, f.size

  l = len(fluids)
  print 'File Count:', len(files)
  print 'Total File Size:', tot
  print 'Average File Size:', (tot / l)
  print 'Median File Size:', fluids[l / 2].size
  print 'Smallest File Size:', fluids[0].size, '(', fluids[0].name, ')'
  print 'Largetst File Size:', fluids[l - 1].size, '(', fluids[l - 1].name, ')'
  return

class Fluid:
  def __init__(self, name, size):
    self.name = name
    self.size = size


if __name__ == '__main__':
  statistize()
