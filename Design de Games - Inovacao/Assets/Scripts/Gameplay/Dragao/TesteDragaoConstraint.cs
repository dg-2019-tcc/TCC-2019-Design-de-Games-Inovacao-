using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TesteDragaoConstraint : MonoBehaviour
{

    ConstraintSource constraintSource;

    void Start()
    {

        constraintSource.sourceTransform = GetComponent<Transform>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {


            collision.GetComponent<ParentConstraint>().AddSource(constraintSource);

            //collision.GetComponent<BoxCollider2D>().transform.parent.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<ParentConstraint>().RemoveSource(0);
            //collision.GetComponent<BoxCollider2D>().transform.parent.SetParent(null);
        }
    }





}
