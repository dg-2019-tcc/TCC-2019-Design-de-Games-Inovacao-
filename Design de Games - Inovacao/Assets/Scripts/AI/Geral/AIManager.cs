using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace Complete
{
    [Serializable]
    public class AIManager
    {
        public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
        [HideInInspector] public int m_PlayerNumber;            // This specifies which player this the manager for.
        [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
        [HideInInspector] public List<Transform> m_WayPointList;

        private StateController m_StateController;				// Reference to the StateController for AI tanks
        public AIMovement aiMovement;

        public void SetupAI(List<Transform> wayPointList)
        {
            m_StateController = m_Instance.GetComponent<StateController>();
            aiMovement = m_Instance.GetComponent<AIMovement>();
            m_StateController.SetupAI(true, wayPointList);
        }

    }
}
