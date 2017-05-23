using System.IO;

using ION.Core.Fluids;

namespace ION.Core.Fluids.Parser {
  /// <summary>
  /// Defines a contract for a FluidParser. A FluidParser is an entity that will
  /// parse a fluid from an input stream.
  /// </summary>
  public interface IFluidParser {
    /// <summary>
    /// Parses a fluid from the given stream. Throws an Exception if the fluid
    /// cannot be parsed from the stream.
    /// </summary>
    /// <remarks>Note: This method expects the stream to spit out little endiean
    /// constructs. Ensure that the byte order of the stream is correct.</remarks>
    /// <param name="stream"></param>
    /// <returns></returns>
    Fluid ParseFluid(Stream stream);
  }
}
