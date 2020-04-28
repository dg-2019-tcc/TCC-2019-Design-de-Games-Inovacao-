using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class StateController : MonoBehaviour
    {
        public AIStats botStats;
        public Transform pos;
        public State remainState;
        public State currentState;
        public int maxPoints;
        public FloatVariable botScore;

        public Rigidbody2D rb;

        [HideInInspector] public List<Transform> wayPointList;
        public int nextWayPoint;
        [HideInInspector] public float stateTimeElapsed;
        [HideInInspector] public Transform target;
        [HideInInspector] public bool canJump;
        [HideInInspector] public float jumpCooldown;
        [HideInInspector] public float forceVertical;
	[HideInInspector] public AIMovement movement;
        private bool aiActive;
        private StateController controller;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            controller = GetComponent<StateController>();

            botScore.Value = 0;
        }



        public void SetupAI(bool aiActivationFromAIManager, List<Transform> wayPointsFromTankManager)
        {

            wayPointList = wayPointsFromTankManager;
            aiActive = aiActivationFromAIManager;
		movement = GetComponent<AIMovement>();
        }


        void Update()
        {
            if (!aiActive)
                return;
            currentState.UpdateState(this);

            if (botScore.Value >= maxPoints)
            {
                controller.enabled = false;
            }
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                currentState = nextState;
                //OnExitState();
            }
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }
    }

