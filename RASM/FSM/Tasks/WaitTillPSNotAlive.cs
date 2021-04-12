using System;
using UnityEngine;

namespace RASM.FSM.Tasks
{
    [Serializable]
    public class WaitTillPSNotAlive : FSMTask
    {
        [SerializeField]
        private ParticleSystem particleSystem;

        #region Overrides of FSMTask

        public override void OnEnter()
        {
            Update(0);
        }

        public override void OnExit()
        {
            IsFinished = false;
        }

        public override void Update(float dt)
        {
            IsFinished = !particleSystem.IsAlive();
        }

        public override bool IsFinished { get; protected set; }

        #endregion
    }
}