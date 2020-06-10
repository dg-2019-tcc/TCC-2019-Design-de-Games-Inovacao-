using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Complete
{
    public class GameManager : MonoBehaviour
    {

        public GameObject[] aiPrefab;
        public AIManager[] m_AI;
        public List<Transform> wayPointsForAI;

        public GameObject thingsAI;
        public GameObject thingsOnline;

        public FloatVariable botScore;

        public AIMovement aiMovement;

        public bool comecouPartida;

        public BoolVariableArray acabou01;

		private void Start()
        {
			if (!OfflineMode.modoDoOffline)
			{
                if (thingsOnline != null)
                {
                    thingsOnline.SetActive(true);
                }
                thingsAI.SetActive(false);
			}
            else
            {
                botScore.Value = 0;
                if (thingsOnline != null)
                {
                    thingsOnline.SetActive(false);
                }
                thingsAI.SetActive(true);
            }
        }

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SpawnAI();
			}

            if(OfflineMode.modoDoOffline && comecouPartida && aiMovement.enabled == false)
            {
                aiMovement.enabled = true;
            }
		}

		public void SpawnAI()
        {
            if (acabou01.Value[7] == true)
            {
                m_AI[0].m_Instance =
                    Instantiate(aiPrefab[0], m_AI[0].m_SpawnPoint.position, m_AI[0].m_SpawnPoint.rotation) as GameObject;
                m_AI[0].SetupAI(wayPointsForAI);

                aiMovement = m_AI[0].aiMovement;
            }

            else
            {
                m_AI[0].m_Instance =
                    Instantiate(aiPrefab[1], m_AI[0].m_SpawnPoint.position, m_AI[0].m_SpawnPoint.rotation) as GameObject;
                m_AI[0].SetupAI(wayPointsForAI);

                aiMovement = m_AI[0].aiMovement;
            }
            aiMovement.enabled = false;

        }
    }
}