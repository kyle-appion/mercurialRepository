namespace ION.Core.StateMachine {
	
	using System;

	/// <summary>
	/// The enumerations of the possible return codes for a state.
	/// </summary>
	public enum ERetCode {
		/// <summary>
		/// The state function returned a result of ok. This generally means that the state is complete and that the engine
		/// should move to the next state.
		/// </summary>
		Ok,
		/// <summary>
		/// The state function returned that it needs another pass to complete its task.
		/// </summary>
		Repeat,
		/// <summary>
		/// The state function returned that it failed at its task.
		/// </summary>
		Failure,
		/// <summary>
		/// The state function returned that an error happened. This will send the state machine into an error state.
		/// </summary>
		Error,
	}
}

