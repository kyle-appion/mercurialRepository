namespace ION.Core.StateMachine {

	using System;
	using System.Collections.Generic;
	using System.Reflection;

	using ION.Core.Util;

	/// <summary>
	/// A state function that is used to perform state logic.
	/// </summary>
	public delegate ERetCode StateFunction(StateEngine engine);
	/// <summary>
	/// The delegate that is used when a state function returns an error result.
	/// </summary>
	public delegate void OnStateMachineError(StateEngine engine, StateFunction triggeringFunction);
	/// <summary>
	/// The delegate that is used when a state is changed within the engine.
	/// </summary>
	public delegate void OnStateChanged(StateEngine engine, StateFunction oldState, StateFunction newState);

	/// <summary>
	/// A StateEngine is an entirely build state machine assembly.
	/// </summary>
	public class StateEngine {

		/// <summary>
		/// The event that will be triggered when an actual state changes within the state engine.
		/// </summary>
		public event OnStateChanged onStateChanged;
		/// <summary>
		/// The dictionary that contains the lookup used to find where a state function should go based on its return code.
		/// </summary>
		private readonly Dictionary<Tuple<StateFunction, ERetCode>, StateFunction> transitionTable;

		/// <summary>
		/// The current state of the engine.
		/// </summary>
		public StateFunction currentState { get; private set; }

		private StateEngine(Dictionary<Tuple<StateFunction, ERetCode>, StateFunction> transitionTable, StateFunction initialState) {
			this.transitionTable = transitionTable;
			this.currentState = initialState;
		}

		/// <summary>
		/// Advances the current state of the state engine.
		/// </summary>
		/// <returns>True if the advance occured with no errors.</returns>
		public bool Advance() {
			var prevState = currentState;
			var ret = currentState(this);

			var key = new Tuple<StateFunction, ERetCode>(currentState, ret);
			if (transitionTable.ContainsKey(key)) {
				var newState = transitionTable[key];
				currentState = newState;
				if (newState != prevState) {
					onStateChanged(this, prevState, newState);
				}
				return key.Item2 == ERetCode.Error;
			} else {
				// TODO ERROR
				return false;
			}
		}

		/// <summary>
		/// Notifies the OnStateChanged event of a new state change.
		/// </summary>
		/// <returns>The state changed.</returns>
		/// <param name="prevState">Previous state.</param>
		/// <param name="newState">New state.</param>
		private void NotifyStateChanged(StateFunction prevState, StateFunction newState) {
			if (onStateChanged != null) {
				onStateChanged(this, prevState, newState);
			}
		}

		/// <summary>
		/// A Builder that will allow for the creation of new state engines.
		/// </summary>
		public class Builder {
//			private Dictionary<StateFunction, string> stateIds = new Dictionary<StateFunction, string>();
			private Dictionary<Tuple<StateFunction, ERetCode>, StateFunction> transitionTable = new Dictionary<Tuple<StateFunction, ERetCode>, StateFunction>();

//			private List<StateFunction> stateFunctions;
//			private StateFunction rootStartState;
//			private StateFunction errorState;

			/// <summary>
			/// Adds a new transition for the state machine.
			/// </summary>
			/// <returns>The transition.</returns>
			public Builder AddTransition(StateFunction from, ERetCode retCode, StateFunction to) {
/*
				if (!stateFunctions.Contains(from)) {
					throw new Exception("Cannot add transition: does not contain state {" + from.GetType().Name + "}");
				}

				if (!stateFunctions.Contains(to)) {
					throw new Exception("Cannot add transition: does not contain state {" + to.GetType().Name + "}");
				}
*/

				transitionTable.Add(new Tuple<StateFunction, ERetCode>(from, retCode), to);
				return this;
			}

			/// <summary>
			/// Builds a new state engine using the currently defined transition table as the engine's life cycle.
			/// </summary>
			public StateEngine Build(StateFunction initialState) {
				return new StateEngine(transitionTable, initialState);
			}
		}
	}
}

