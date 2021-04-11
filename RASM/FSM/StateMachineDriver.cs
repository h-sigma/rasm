using UnityEngine;

namespace RASM.FSM
{
    public enum UpdateMode
    {
        Update,
        LateUpdate,
        FixedUpdate
    }

    public class StateMachineDriver : MonoBehaviour
    {
        [SerializeField]
        private StateMachine stateMachine;

        [SerializeField]
        private UpdateMode updateMode;

        public StateMachine StateMachine => stateMachine;

        public UpdateMode UpdateMode
        {
            get => updateMode;
            set => updateMode = value;
        }

        private void Awake()
        {
            stateMachine.SetAgent(gameObject);
        }

        private void Update()
        {
            if (updateMode == UpdateMode.Update) stateMachine.Update(Time.deltaTime);
        }

        private void LateUpdate()
        {
            if (updateMode == UpdateMode.LateUpdate) stateMachine.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (updateMode == UpdateMode.FixedUpdate) stateMachine.Update(Time.fixedDeltaTime);
        }
    }
}