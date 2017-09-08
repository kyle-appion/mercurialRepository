namespace Appion.Commons.Security {

	using System;

  /// <summary>
  /// For some of the network traffic that is done between the app and the Appion server,
  /// Christian wished to have some "plain-text obfuscation" to prevent people from seeing
  /// that we are requesting things by serial number. This class provides APIs used in
  /// this data obfuscation. Note: This is __NOT__ true security and should __NEVER__ be
  /// considered safe and/or secure.
  /// </summary>
  public interface IObfuscator {
    /// <summary>
    /// Obfuscates the given bytes to the given key.
    /// </summary>
    /// <param name="din">The data to obfuscate.</param>
    /// <param name="key">The key to obfuscate against.</param>
    byte[] Obfuscate(byte[] din, byte[] key);

    /// <summary>
    /// Deobfuscates the given bytes with the given key.
    /// </summary>
    /// <param name="din">The data to deobfuscate.</param>
    /// <param name="key">The key of deobfuscate against.</param>
    byte[] Deobfuscate(byte[] din, byte[] key);
  }

  /// <summary>
  /// A stupid simple cyclic XOR obfuscator.
  /// </summary>
  public class XORObfuscator : IObfuscator {
    // Overridden from IObfuscator
    public byte[] Obfuscate(byte[] din, byte[] key) {
      var len = din.Length;
      var keyLen = key.Length;

      byte[] ret = new byte[len];

      for (int i = 0; i < len; i++) {
        ret[i] = (byte)(din[i] ^ key[i % keyLen]);
      }

      return ret;
    }

    // Overridden from IObfuscator
    public byte[] Deobfuscate(byte[] din, byte[] key) {
      return Obfuscate(din, key);
    }
  }
}

