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
        public struct SrlTransitionPair
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
            EntryState = GetState(entryState);
        }

        #endregion

        public FSMState GetState(int stateId)
        {
            return stateId < states.Count && stateId >= 0 ? states[stateId] : null;
        }

        #region Properties

        public FSMState CurrentState { get; private set; }

        public GameObject Agent     { get; private set; }
        public bool       IsRunning { get; private set; }

        public List<FSMState> States => states;

        #endregion

        private bool _didInit;

        private void Init()
        {
            if (_didInit) return;
            _didInit = true;

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
        }

        public void Start()
        {
            Init();

            if (IsRunning) return;
            IsRunning = true;
            if (CurrentState == null)
            {
                SetState(EntryState);
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Update(float dt)
        {
            if (!IsRunning) return;
            if (CurrentState != null)
            {
                CurrentState.Update(dt);
                foreach (TransitionStatePair pair in TransitionMap[CurrentState])
                {
                    if (pair.Transition?.DoTransition(CurrentState) == true)
                    {
                        SetState(pair.State);
                        break;
                    }
                }
            }
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