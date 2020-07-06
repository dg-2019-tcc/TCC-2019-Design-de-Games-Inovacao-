using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetavelAI : MonoBehaviour
{
    private StateController aiController;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("AI"))
        {
            aiController = col.GetComponent<StateController>();
            aiController.botScore.Value++;
            Destroy(gameObject);

        }
    }
}
