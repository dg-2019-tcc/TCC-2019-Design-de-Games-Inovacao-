using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class StateController : MonoBehaviour
    {
        public AIStats botStats;
        public Transform pos;
        public int maxPoints;
        public FloatVariable botScore;

        public Rigidbody2D rb;

        [HideInInspector] public List<GameObject> wayPointList;
        public int nextWayPoint;
        [HideInInspector] public float stateTimeElapsed;
        [HideInInspector] public Transform target;
        [HideInInspector] public bool canJump;
        [HideInInspector] public float jumpCooldown;
        [HideInInspector] public float forceVertical;
	    //[HideInInspector] public AIMovement movement;
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

            //wayPointList = wayPointsFromTankManager;
            aiActive = aiActivationFromAIManager;
		    //movement = GetComponent<AIMovement>();
        }


        void Update()
        {
            if (!aiActive)
                return;
            //currentState.UpdateState(this);

            if (botScore.Value >= maxPoints)
            {
                controller.enabled = false;
            }
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }
    }

