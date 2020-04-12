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

    public Rigidbody2D rb;

    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool canJump;

    private bool aiActive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void SetupAI(bool aiActivationFromAIManager, List<Transform> wayPointsFromTankManager)
    {

        wayPointList = wayPointsFromTankManager;
        aiActive = aiActivationFromAIManager;
    }


    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
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
