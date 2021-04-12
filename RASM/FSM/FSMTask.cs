using System;

namespace RASM.FSM
{
    [Serializable]
    public abstract class FSMTask
    {
        [NonSerialized]
        private StateMachine _owner;

        [NonSerialized]
        private FSMState _parent;

        public StateMachine Owner => _owner;

        public FSMState Parent => _parent;

        public void Awake(StateMachine owner, FSMState fsmState)
        {
            _owner  = owner;
            _parent = fsmState;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Update(float dt);
        public abstract bool IsFinished { get; protected set; }
    }
}