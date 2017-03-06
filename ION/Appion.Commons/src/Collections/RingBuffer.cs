namespace Appion.Commons.Collections {

	using System;

	/// <summary>
	/// A simple data structure that add items to an array in a ring like fashion. The best way to think of how this data
	/// structure works, is by imagining that the head and tail of an array are stiched together to create a continuous
	/// entity. This structure is usful in create a buffer that needs to always walk forward, but needs a limited amount
	/// of space.
	/// </summary>
	// TODO ahodder@appioninc.com: Unfinished. Other things took priority
	public class RingBuffer<T> {
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
					return items.Length - tail + head;
				}
			}
		}

		/// <summary>
		/// Queries the maximum number of items that may be added to the ring buffer.
		/// </summary>
		/// <value>The capacity.</value>
		public int capacity { get { return items.Length - 1; } }

		private T[] items;
		/// <summary>
		/// The exclusive pointer to the current insertion point of the next item.
		/// </summary>
		private int head;
		/// <summary>
		/// The inclusive pointer to the current end point of the oldest item.
		/// </summary>
		private int tail;

		private TimeSpan span;

		public RingBuffer(int size) {
			Resize(size);
			head = tail = 0;
		}

		/// <summary>
		/// Adds an item to the ring buffer.
		/// </summary>
		/// <returns>The add.</returns>
		/// <param name="t">T.</param>
		public void Add(T t) {
			// Increment the head by one. If we overflow, wrap.
			items[head = (head + 1) % items.Length] = t;
			// Move the tail if necessary.
			if (tail == head) {
				// Increment the tail by one. If we overflow, wrap.
				tail = (tail + 1) % items.Length;
			}
		}

		/// <summary>
		/// Resizes the ring buffer. If the new size reduces the size of the buffer, we will keep only the newest data.
		/// </summary>
		/// <returns>The resize.</returns>
		/// <param name="newSize">New size.</param>
		private void Resize(int newSize) {
			// Increment new size by one because the head is a dead index within the buffer.
			T[] newContent = new T[newSize + 1];
			// The actual amount of data that is being moved into the new resized buffer.
			int cnt = Math.Min(this.count, newSize);

			if (head > tail) {
				// The buffer is in order, this is a simple copy.
				Array.Copy(items, newContent, head - tail);
			} else {
				// Otherwise, we need to do two copies as the head wraps the content array.
				// TODO
			}

			items = newContent;
			head = cnt;
			tail = 0; 
		}
	}
}
