namespace Appion.Commons.Collections {

	using System;
	using System.Collections;

	/// <summary>
	/// A simple data structure that add items to an array in a ring like fashion. The best way to think of how this data
	/// structure works, is by imagining that the head and tail of an array are stiched together to create a continuous
	/// entity. This structure is usful in create a buffer that needs to always walk forward, but needs a limited amount
	/// of space.
	/// </summary>
	public class RingBuffer<T> : IEnumerable {

		/// <summary>
		/// Queries whether or not the ring buffer is empty.
		/// </summary>
		/// <value><c>true</c> if is empty; otherwise, <c>false</c>.</value>
		public bool isEmpty { get { return head == tail; } }

		/// <summary>
		/// Queries the first entity in the ring buffer.
		/// Note: you should check if the buffer is empty before getting the first item or you may get undefined results.
		/// </summary>
		/// <value>The first.</value>
		public T first { 
			get {
				if (isEmpty) {
					return default(T);
				} else if (head == 0) {
					return buffer[buffer.Length - 1];
				} else {
					return buffer[head - 1];
				}
			}
		}

		/// <summary>
		/// Queries the last (oldest) entity in the ring buffer.
		/// Note: you chould check if the buffer is empty before getting the last item or you may get undefined results.
		/// </summary>
		/// <value>The last.</value>
		public T last {
			get {
				if (isEmpty) {
					return default(T);
				} else {
					return buffer[tail];
				}
			}
		}

		/// <summary>
		/// Queries the number of items that have been added to the ring buffer.
		/// </summary>
		/// <value>The count.</value>
		public int count {
			get {
				if (head == tail) {
					return 0;
				} else if (head > tail) {
					return head - tail;
				} else {
					return buffer.Length - tail + head;
				}
			}
		}
		/// <summary>
		/// Queries the maximum number of items that may be added to the ring buffer.
		/// </summary>
		/// <value>The capacity.</value>
		public int capacity { get { return buffer.Length - 1; } }

		private T[] buffer;
		/// <summary>
		/// The exclusive pointer to the current insertion point of the next item.
		/// </summary>
		private int head;
		/// <summary>
		/// The inclusive pointer to the current end point of the oldest item.
		/// </summary>
		private int tail;

		public RingBuffer(RingBuffer<T> other) {
			this.buffer = new T[other.buffer.Length];
			this.head = other.head;
			this.tail = other.tail;

			Array.Copy(buffer, other.buffer, buffer.Length);
		}

		public RingBuffer(int size) {
			Resize(size);
			head = tail = 0;
		}

		// Implemented from IEnumerable
		public IEnumerator GetEnumerator() {
			return new Enumerator<T>(this);
		}

		/// <summary>
		/// Clears the buffer.
		/// </summary>
		public void Clear() {
			head = tail = 0;
		}

		/// <summary>
		/// Adds an item to the ring buffer. If the added item would go out of "bounds" of the ring buffer, we will
		/// overwrite the oldest data point in the buffer.
		/// </summary>
		/// <returns>The add.</returns>
		/// <param name="t">T.</param>
		public void Add(T t) {
			// Insert the item into the current head position.
			buffer[head] = t;
			// Increment the head by one. If we overflow, wrap.
			head = (head + 1) % buffer.Length;
			// Move the tail if necessary.
			if (tail == head) {
				// Increment the tail by one. If we overflow, wrap.
				tail = (tail + 1) % buffer.Length;
			}
		}

		/// <summary>
		/// Removes the last item.
		/// </summary>
		/// <returns>True if anything was removed.</returns>
		public bool RemoveLast() {
			if (head != tail) {
				tail = (tail + 1) % this.buffer.Length;
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Resizes the ring buffer. If the new size reduces the size of the buffer, we will keep only the newest data.
		/// </summary>
		/// <returns>The resize.</returns>
		/// <param name="newSize">New size.</param>
		public void Resize(int newSize) {
			// Increment new size by one because the head is a dead index within the buffer.
			T[] newContent = new T[newSize + 1];
			// The actual amount of data that is being moved into the new resized buffer.
			int cnt = Math.Min(this.count, newSize);

			if (head > tail) {
				// The buffer is in order, this is a simple copy.
				Array.Copy(buffer, newContent, head - tail);
			} else {
				// Otherwise, we need to do two copies as the head wraps the content array.
				// TODO
			}

			buffer = newContent;
			head = cnt;
			tail = 0; 
		}

		/// <summary>
		/// Queries an array of the contents within the ring buffer sorted from newest to oldest point.
		/// </summary>
		/// <returns>The array.</returns>
		public T[] ToArray() {
			var ret = new T[count];
			var j = 0;

			if (count > 0) {
				if (head > tail) {
					for (int i = head - 1; i >= tail; i--) {
						ret[j++] = buffer[i];
					}
				} else {
					if (head == 0) {
						for (int i = buffer.Length - 1; i >= tail; i--) {
							ret[j++] = buffer[i];
						}
					} else {
						for (int i = head - 1; i >= 0; i--) {
							ret[j++] = buffer[i];
						}

						for (int i = buffer.Length - 1; i >= tail; i--) {
							ret[j++] = buffer[i];
						}
					}
				}
			}

			return ret;
		}

		// Overridden from object
		public override string ToString() {
			return string.Format("[RingBuffer: isEmpty={0}, first={1}, last={2}, count={3}, capacity={4}]", isEmpty, first, last, count, capacity);
		}

		/// <summary>
		/// The enumerator that will index items from the RingBuffer.
		/// </summary>
		private class Enumerator<T> : IEnumerator {
			// Implemented from IEnumerator
			public object Current {
				get {
					return default(T);
				}
			}

			/// <summary>
			/// The ring buffer that we are enumerating over.
			/// </summary>
			private RingBuffer<T> buffer;
			/// <summary>
			/// The current index of the iterator.
			/// </summary>
			private int index;

			public Enumerator(RingBuffer<T> buffer) {
				this.buffer = buffer;
				index = buffer.tail;
			}

			// Implemented from IEnumerator
			public void Dispose() {
			}

			// Implemented from IEnumerator
			public bool MoveNext() {
				index = (index + 1) % buffer.buffer.Length;
				return index != buffer.head;
			}

			// Implemented from IEnumerator
			public void Reset() {
				index = buffer.tail;
			}
		}
	}
}
