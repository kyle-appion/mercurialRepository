using System;
namespace ION.DevAssist {
	public class ConsoleOperator {

		/// <summary>
		/// Wrapper around Console.Write.
		/// </summary>
		/// <param name="message">Message.</param>
		public ConsoleOperator W(string message) {
			Console.Write(message);
			return this;
		}

		/// <summary>
		/// Writes a new line to console.
		/// </summary>
		public ConsoleOperator NL(int count=1) {
			while (count-- > 0) {
				Console.WriteLine("");
			}
			return this;

		}

		/// <summary>
		/// Wrapper around Console.WriteLine.
		/// </summary>
		/// <param name="message">Message.</param>
		public ConsoleOperator WL(string message) {
			Console.WriteLine(message);
			return this;
		}

		/// <summary>
		/// Deletes the last characters printed to the console.
		/// </summary>
		public ConsoleOperator BS() {
			Console.Write('\b');
			return this;
		}

		/// <summary>
		/// Reads a single line from the console.
		/// </summary>
		public string RL() {
			return Console.ReadLine();
		}

		/// <summary>
		/// Deletes the last line that was printed to the console.
		/// </summary>
		public ConsoleOperator DL() {
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write(new string(' ', Console.BufferWidth)); 
			Console.SetCursorPosition(0, Console.CursorTop - 1);
			return this;
		}

		/// <summary>
		/// Clears the console.
		/// </summary>
		public ConsoleOperator Clear() {
			Console.Clear();
			return this;
		}

		/// <summary>
		/// Quits the program.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="ec">Ec.</param>
		public void Quit(string message, int ec=0) {
			WL("\n\n");
			WL(message);
			System.Environment.Exit(ec); 
		}

		/// <summary>
		/// Requests user input to select and item. The tuple format is as follows:
		/// item1 is the "key" or command that the user is to type. item2 is the message that is presented for the command.
		/// </summary>
		/// <returns>The list item selection.</returns>
		/// <param name="msg">Message.</param>
		/// <param name="items">Items.</param>
		public void RequestListItemSelection(string msg, InputBuilder items) {
			WL(msg);
			NL();
			WL("Command | Option");
			foreach (var key in items.actions.Keys) {
				WL(string.Format("{0,-7} {1}", key, items.actions[key].Item1));
			}

			while (true) {
				var line = RL().Trim();

				if (!items.actions.ContainsKey(line)) {
					// Failed to read a valid input.
					WL("You entered an invalid command. Please enter one of the presented commands.");
					continue;
				}

				items.actions[line].Item2();
				return;
			}
		}
	}
}
