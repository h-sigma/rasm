using System;

namespace RASM.FSM.Transitions
{
    [Serializable]
    public class StateFinishedTransition : FSMTransition
    {
        #region Overrides of FSMTransition

        public override bool DoTransition(FSMState from)
        {
            return from.IsFinished;
        }

        #endregion
    }
}