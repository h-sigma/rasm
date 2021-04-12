using System;

namespace RASM.FSM
{
    /// <summary>
    /// Transition base class. A transition decides whether it's time to move to another state.
    /// A transition object will be shared among state machine instances.
    /// </summary>
    [Serializable]
    public abstract class FSMTransition
    {
        public abstract bool DoTransition(FSMState from);
    }
}