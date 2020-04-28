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


		private void Start()
        {
			if (!OfflineMode.modoDoOffline)
			{
                thingsOnline.SetActive(true);
                thingsAI.SetActive(false);
			}
            else
            {
                thingsOnline.SetActive(false);
                thingsAI.SetActive(true);
            }
        }

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SpawnAI();
			}
		}

		public void SpawnAI()
        {
     
            
                m_AI[0].m_Instance =
                    Instantiate(aiPrefab[0], m_AI[0].m_SpawnPoint.position, m_AI[0].m_SpawnPoint.rotation) as GameObject;
                m_AI[0].SetupAI(wayPointsForAI);

            Debug.Log("Spawn");
            
        }
    }
}