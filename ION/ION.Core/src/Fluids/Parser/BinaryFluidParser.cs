using System.IO;

using ION.Core.Fluids;
using ION.Core.IO;
using ION.Core.Util;

namespace ION.Core.Fluids.Parser {
  /// <summary>
  /// A FluidParser that will parse a Fluid from a binary stream.
  /// </summary>
  public class BinaryFluidParser : IFluidParser {
    // Overridden from IFluidParser
    public Fluid ParseFluid(Stream stream) {
      using (BinaryReader reader = new BinaryReader(stream)) {

        // Read the name of the fluid
        var nameLen = reader.ReadInt32BE();
        var rawName = reader.ReadBytes(nameLen);

        // Read the tmin and tmax
        var tmin = reader.ReadInt32BE();
        var tmax = reader.ReadInt32BE();

        // Read interpolation step
        var step = reader.ReadFloat32BE();

        // Read the magnitude of the table
        var rowCount = reader.ReadInt32BE();

        // Reads whether or not the fluid is a "similar" fluid (does not use both dew and bubble measurements)
        var similer = reader.ReadBoolean();

        // Prepare for table construction
        var temps = new double[rowCount];
        var bubbles = new double[rowCount];
        var dew = new double[rowCount];

        // Read the table, and assign pressures to temperatures
        for (int i = 0; i < rowCount; i++) {
          temps[i] = tmin + (i * step);
          bubbles[i] = reader.ReadFloat32BE();
          dew[i] = reader.ReadFloat32BE();
        }

        // All done, return this bitch
        return new Fluid(System.Text.Encoding.UTF8.GetString(rawName, 0, nameLen), tmin, tmax, step, rowCount, temps, bubbles, dew);
      }
    }
  }
}
