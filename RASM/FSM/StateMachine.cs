using System;
using System.Collections.Generic;
using UnityEngine;

namespace RASM.FSM
{
    [Serializable]
    public class StateMachine : ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<FSMState> states = new List<FSMState>();

        [SerializeField]
        private int entryState;

        [SerializeField]
        private SrlTransitionPair[] transitions;

        [Serializable]
        internal struct SrlTransitionPair
        {
            public int fromState;
            public int toState;

            [SerializeReference, SerializeReferenceButton]
            public FSMTransition transition;
        }

        public Dictionary<FSMState, List<TransitionStatePair>> TransitionMap { get; } =
            new Dictionary<FSMState, List<TransitionStatePair>>();

        public FSMState EntryState { get; private set; }

        #region Implementation of ISerializationCallbackReceiver

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            for (int index = 0; index < states.Count; index++)
            {
                FSMState fsmState = states[index];
                if (fsmState == null) return;
                fsmState.Awake(this);
                var outgoingTransitions = new List<TransitionStatePair>();
                TransitionMap[fsmState] = outgoingTransitions;
                foreach (SrlTransitionPair srlTransitionPair in transitions)
                {
                    if (srlTransitionPair.fromState == index)
                    {
                        outgoingTransitions.Add(new TransitionStatePair()
                        {
                            State      = GetState(srlTransitionPair.toState),
                            Transition = srlTransitionPair.transition
                        });
                    }
                }
            }

            EntryState = GetState(entryState);
        }

        private FSMState GetState(int stateId)
        {
            return stateId < states.Count && stateId >= 0 ? states[stateId] : null;
        }

        #endregion

        public FSMState CurrentState { get; private set; }

        public GameObject Agent { get; private set; }

        public void Update(float dt)
        {
            CurrentState?.Update(dt);
        }

        public void SetAgent(GameObject gameObject)
        {
            Agent = gameObject;
        }

        public void SetState(FSMState state)
        {
            CurrentState?.OnExit();
            CurrentState = state;
            CurrentState?.OnEnter();
        }
    }
}