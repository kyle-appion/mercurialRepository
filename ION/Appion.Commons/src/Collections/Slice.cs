namespace Appion.Commons.Collections {

  using System;
  using System.Collections.Generic;

  public class Slice<T> : IList<T> {

    // Implemented from IList
    public int Count { get { return endIndex - startIndex + 1; } }
    // Implemented from IList
    public bool IsFixedSize { get { return true; } }
    // Implemented from IList
    public bool IsReadOnly { get { return true; } }
    // Implemented from IList
    public bool IsSynchronized { get { return false; } }
    // Implemented from IList
    public T this[int index] { get { return buffer[index]; } set { } }

    /// <summary>
    /// The buffer that is wrapped by the slice.
    /// </summary>
    private T[] buffer;
    /// <summary>
    /// The start index within the buffer.
    /// </summary>
    private int startIndex;
    /// <summary>
    /// The non-inclusive end index within the buffer.
    /// </summary>
    private int endIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Appion.Commons.Collections.Slice`1"/> class.
    /// </summary>
    /// <param name="buffer">Buffer.</param>
    public Slice(T[] buffer) : this(buffer, 0, buffer.Length) {
    }

    public Slice(T[] buffer, int endIndex) : this(buffer, 0, endIndex) {
    }

    public Slice(T[] buffer, int startIndex, int endIndex) {
      this.buffer = buffer;
      this.startIndex = startIndex;
      this.endIndex = endIndex;
    }

    // Implemented from IList
    public void Add(T value) {
    }

    // Implemented from IList
    public void Clear() {
    }

    // Implemented from IList
    public bool Contains(T value) {
      return false;
    }

    // Implemented from IList
    public void CopyTo(T[] dest, int obj) {
    }

    // Implemented from IList
    public IEnumerator<T> GetEnumerator() {
      return new Enumerator(this);
    }

    // Implemented from IList
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }

    // Implemented from IList
    public int IndexOf(T obj) {
      for (int i = startIndex; i <= endIndex; i++) {
        if (buffer[i].Equals(obj)) {
          return i - startIndex;
        }
      }
      return -1;
    }

    // Implemented from IList
    public void Insert(int index, T obj) {
    }

    // Implemented from IList
    public bool Remove(T obj) {
      return false;
    }

    // Implemented from IList
    public void RemoveAt(int index) {
    }

    /// <summary>
    /// The enumerator that is used to walk through the content of the slice.
    /// </summary>
    private class Enumerator : IEnumerator<T> {
      // Implemented from IEnumerator
      object System.Collections.IEnumerator.Current { get { return slice[currentIndex]; } }
      // Implemented from IEnumerator
      public T Current { get { return slice[currentIndex]; } }

      /// <summary>
      /// The slice that we are enumerating over.
      /// </summary>
      private Slice<T> slice;
      /// <summary>
      /// The current index within the slice.
      /// </summary>
      private int currentIndex;

      public Enumerator(Slice<T> slice) {
        this.slice = slice;
        currentIndex = slice.startIndex;
      }

      // Implemented from IEnumerator
      public void Dispose() {
      }

      // Implemented from IEnumerator
      public bool MoveNext() {
        currentIndex++;
        return !(currentIndex > slice.endIndex);
      }

      // Implemented from IEnumerator
      public void Reset() {
        currentIndex = slice.startIndex;
      }
    }
  }
}
