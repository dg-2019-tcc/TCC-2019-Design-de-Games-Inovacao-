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

		
        /*private void Start()
        {
			if (OfflineMode.modoDoOffline)
			{
				//SpawnAI();
			}
        }*/
		

        public void SpawnAI()
        {
     
            
                m_AI[0].m_Instance =
                    Instantiate(aiPrefab[0], m_AI[0].m_SpawnPoint.position, m_AI[0].m_SpawnPoint.rotation) as GameObject;
                m_AI[0].SetupAI(wayPointsForAI);

            Debug.Log("Spawn");
            
        }
    }
}