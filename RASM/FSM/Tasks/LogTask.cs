using System;
using UnityEngine;

namespace RASM.FSM.Tasks
{
    [Serializable]
    public class LogTask : FSMTask
    {
        [SerializeField]
        private string onEnterLog;

        [SerializeField]
        private string onExitLog;

        [SerializeField]
        private string onUpdateLog;

        [SerializeField]
        private bool isFinished;

        #region Overrides of FSMTask

        public override void OnEnter()
        {
            if (string.IsNullOrEmpty(onEnterLog)) return;
            Debug.Log(onEnterLog);
        }

        public override void OnExit()
        {
            if (string.IsNullOrEmpty(onExitLog)) return;
            Debug.Log(onExitLog);
        }

        public override void Update(float dt)
        {
            if (string.IsNullOrEmpty(onUpdateLog)) return;
            Debug.Log(string.Format(onUpdateLog, dt));
        }

        public override bool IsFinished
        {
            get => isFinished;
            protected set => isFinished = value;
        }

        #endregion
    }
}