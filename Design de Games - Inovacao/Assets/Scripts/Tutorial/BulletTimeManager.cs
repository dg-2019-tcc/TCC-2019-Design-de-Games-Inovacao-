using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTimeManager : MonoBehaviour
{
	public GameObject targetObject;
	public float targetTimeScale = 0.2f;
	public float bulletTimeSeconds = 3;
	private bool started;

	private bool slow;
	private bool fast;

	private float originalTimeScale;

	[SerializeField]
	private int momentOfEngine;
	
    void Start()
    {
		started = false;
		slow = false;
		originalTimeScale = Time.timeScale;
		momentOfEngine = 0;
    }
	

    void Update()
    {
		if (targetObject == null && !started)
		{
			started = true;
			momentOfEngine = 1;
		}


		switch(momentOfEngine)
		{
			case 1:
				Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.1f);
				if (Time.timeScale <= targetTimeScale)
				{
					momentOfEngine++;
					Time.timeScale = targetTimeScale;
					StartCoroutine(DelayBT());
				}
				break;
			case 3:
				Time.timeScale = Mathf.Lerp(Time.timeScale, originalTimeScale, 0.1f);
				break;


			default:
				break;
		}
		

		
	}

	private IEnumerator DelayBT()
	{
		yield return new WaitForSeconds(bulletTimeSeconds*targetTimeScale);
		momentOfEngine++;

	}


	
}
