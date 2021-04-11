using System;
using UnityEngine;

namespace RASM.FSM
{
    [Serializable]
    public sealed class FSMState
    {
        [SerializeField]
        private string name;

        [SerializeReference, SerializeReferenceButton]
        private FSMTask[] tasks;

        [SerializeField, HideInInspector]
        private float elapsedTime;

        [SerializeField, HideInInspector]
        private string guid = Guid.NewGuid().ToString();
        
        [NonSerialized]
        private StateMachine _owner;

        /// <summary>
        /// Unique name of the state.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Owner of the 
        /// </summary>
        public StateMachine Owner => _owner;

        public float ElapsedTime => elapsedTime;

        public bool IsFinished => Array.TrueForAll(tasks, task => task.IsFinished);

        public void Awake(StateMachine owner)
        {
            _owner = owner;
            foreach (FSMTask fsmTask in tasks)
            {
                fsmTask?.Awake(owner, this);
            }
        }

        public void OnEnter()
        {
            elapsedTime = 0f;
            foreach (FSMTask fsmTask in tasks)
            {
                fsmTask.OnEnter();
            }
        }

        public void OnExit()
        {
            foreach (FSMTask fsmTask in tasks)
            {
                fsmTask.OnExit();
            }
        }

        public void Update(float dt)
        {
            elapsedTime += dt;
            foreach (FSMTask fsmTask in tasks)
            {
                fsmTask.Update(dt);
            }
        }
    }
}