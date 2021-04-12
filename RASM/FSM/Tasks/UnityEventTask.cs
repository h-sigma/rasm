using System;
using UnityEngine;
using UnityEngine.Events;

namespace RASM.FSM.Tasks
{
    [Serializable]
    public class UnityEventTask : FSMTask
    {
        [Serializable]
        public class UpdateEvent : UnityEvent<float>
        {
        }

        [Serializable]
        public class StateChangeEvent : UnityEvent<FSMState>
        {
        }

        [SerializeField]
        private StateChangeEvent onEnter;

        [SerializeField]
        private UpdateEvent onUpdate;

        [SerializeField]
        private StateChangeEvent onExit;

        #region Overrides of FSMTask

        public override void OnEnter()
        {
            onEnter.Invoke(Parent);
        }

        public override void OnExit()
        {
            onExit.Invoke(Parent);
        }

        public override void Update(float dt)
        {
            onUpdate.Invoke(dt);
        }

        public override bool IsFinished
        {
            get => true;
            protected set { }
        }

        #endregion
    }
}