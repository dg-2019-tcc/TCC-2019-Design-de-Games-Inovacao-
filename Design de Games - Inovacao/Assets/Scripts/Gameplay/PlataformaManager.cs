using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlataformaManager : MonoBehaviour
{
    private PlatformEffector2D effector;

    public Joystick joyStick;
	public BoolVariable jump;

    public GameObject jumpButtonImage;
    public Image arrowImage;

	

    public bool turnPlataforma;

     void Start()
    {
        effector = GetComponent<PlatformEffector2D>();

        joyStick = FindObjectOfType<Joystick>();

		jump = Resources.Load<BoolVariable>("Jump");

        jumpButtonImage = GameObject.FindGameObjectWithTag("ArrowImage");

        arrowImage = jumpButtonImage.GetComponent<Image>();
    }

    private void Update()
    {

		if (joyStick == null)
		{
			joyStick = FindObjectOfType<Joystick>();
		}

        if( jumpButtonImage == null)
        {
            jumpButtonImage = GameObject.FindGameObjectWithTag("ArrowImage");
        }

        if(turnPlataforma == true)
        {
            StartCoroutine("PlatDown");
        }

        else
        {
            StopAllCoroutines();
        }
    }

	

	void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("GroundCheck") && joyStick.Vertical <= -0.8)
        {
            arrowImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        }

        if (col.CompareTag("GroundCheck") && joyStick.Vertical >= -0.8)
        {
            arrowImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }

        if (col.CompareTag("GroundCheck") && joyStick.Vertical <= -0.5 && jump.Value || Input.GetKey(KeyCode.S) && jump.Value)
        {
            turnPlataforma = true;
			jump.Value = false;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("GroundCheck"))
        {

            arrowImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);

            //turnPlataforma = true;
        }


    }


    IEnumerator PlatDown()
    {
        effector.rotationalOffset = 180f;
        yield return new WaitForSeconds(1f);
        effector.rotationalOffset = 0f;
        turnPlataforma = false;
    }
}
