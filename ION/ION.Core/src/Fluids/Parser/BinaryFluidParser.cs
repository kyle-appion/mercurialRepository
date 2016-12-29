namespace ION.Core.Fluids.Parser {

	using System.IO;

	using Appion.Commons.Util;

	using ION.Core.IO;

  /// <summary>
  /// A FluidParser that will parse a Fluid from a binary stream.
  /// </summary>
  public class BinaryFluidParser : IFluidParser {
    /// <summary>
    /// The primary version of the fliud parser.
    /// </summary>
    private const int V1 = 1;
    // Overridden from IFluidParser
    public Fluid ParseFluid(Stream stream) {
      using (BinaryReader reader = new BinaryReader(stream)) {
        // Read the exported version of the fluid. This is used to determine how the fluid data is packed
        var version = reader.ReadByte();
        // Read the name of the fluid
        var nameLen = reader.ReadByte();
        var rawName = reader.ReadBytes(nameLen);

        // Read the tmin and tmax
        var tmin = reader.ReadInt32BE();
        var tmax = reader.ReadInt32BE();

        // Read interpolation step
        var step = reader.ReadFloat32BE();

        // Read the magnitude of the table
        var rowCount = (int)reader.ReadUInt32BE();

        // Reads whether or not the fluid is a mixture
        var mixture = !reader.ReadBoolean();
        Log.D(this, "Is Mixture: " + mixture);

        // Prepare for table construction
        var temps = new double[rowCount];
        var values = new double[rowCount * (mixture ? 2 : 1)];

        switch (version) {
          case V1:
            // Read the table, and assign pressures to temperatures
            for (int i = 0; i < rowCount; i++) {
              temps[i] = tmin + (i * step);
              values[i] = reader.ReadFloat32BE();
            }

            if (mixture) {
              for (int i = 0; i < rowCount; i++) {
                values[rowCount + i] = reader.ReadFloat32BE();
              }
            }
            break;
          default:
            throw new IOException("Failed to load the fluid: the version was not recognized.");
        }

        // All done, return this bitch
        return new Fluid(System.Text.Encoding.UTF8.GetString(rawName, 0, nameLen), mixture, tmin, tmax, step, rowCount, temps, values);
      }
    }
  }
}
