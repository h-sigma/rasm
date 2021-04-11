using System;

namespace RASM.FSM
{
    [Serializable]
    public abstract class FSMTransition
    {
        [NonSerialized]
        private FSMState _from;

        public FSMState From => _from;

        public void Awake(FSMState transitionFrom)
        {
            _from = transitionFrom;
        }

        public abstract bool DoTransition();
    }
}