using System;

namespace RASM.FSM
{
    [Serializable]
    public struct TransitionStatePair
    {
        public FSMTransition Transition;
        public FSMState      State;
    }
}