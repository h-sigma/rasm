using System;
using UnityEngine;

namespace RASM.FSM.Transitions
{
    [Serializable]
    public class ElapsedTimeTransition : FSMTransition
    {
        [SerializeField]
        private float duration;

        #region Overrides of FSMTransition

        public override bool DoTransition(FSMState fromState)
        {
            return fromState.ElapsedTime >= duration;
        }

        #endregion
    }
}