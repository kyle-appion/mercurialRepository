using System;
using System.IO;

using NUnit.Framework;

using ION.Core.IO;

namespace ION.TestFixtures.IO {
  [TestFixture]
  public class TestSerialization {
    [Test]
    public void TestSerialization1() {
      // The initial object
      var animal = new Penguin("fish");

      var outStream = new MemoryStream();
      var os = new ObjectSerializer(new SerializationContext(null), outStream);
      os.WriteObject(animal);
      var inStream = new MemoryStream(outStream.ToArray());
      os = new ObjectSerializer(new SerializationContext(null), inStream);
      var obj = os.ReadObject() as Penguin;




      Assert.AreEqual(animal.name, obj.name);
      Assert.AreEqual(animal.food, obj.food);
    }
  }

  internal class Animal : ISerializable {
    public string name {
      get {
        return __name;
      }
      set {
        if (value == null) {
          value = "";
        }
        __name = value;
      } 
    } string __name;

    protected Animal() {
    }

    // Overridden from ISerializable
    public virtual void Serialize(SerializationContext context, BinaryWriter writer) {
      writer.Write(name);
    }

    // Overridden from ISerializable
    public virtual void Deserialize(SerializationContext context, BinaryReader reader) {
      name = reader.ReadString();
    }
  } // End Animal

  internal class Penguin : Animal, ISerializable {
    public string food { 
      get {
      }
      set {
        if (value == null) {
          value = "";
        }

        __food = value;
      }
    } string __food;

    public Penguin() {
    }

    public Penguin(string food) : base("Penguin") {
      this.food = food;
    }

    // Overridden from ISerializable
    public override void Serialize(SerializationContext context, BinaryWriter writer) {
      base.Serialize(context, writer);
      writer.Write(food);
    }

    // Overridden from ISerializable
    public override void Deserialize(SerializationContext context, BinaryReader reader) {
      base.Deserialize(context, reader);
      food = reader.ReadString();
    }
  }
}

