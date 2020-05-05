using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICustom : MonoBehaviour
{
    public int hairIndex;
    public int shirtIndex;
    public int shortsIndex;
    public int shoesIndex;


    public ChangeMultipleCustom[] hairModels;
    public ChangeMultipleCustom[] shirtModels;
    public ChangeMultipleCustom[] legModels;
    public ChangeMultipleCustom[] shoeModels;
    public ChangeMultipleCustom[] hair2Models;
    public ChangeMultipleCustom[] shirt2Models;
    public ChangeMultipleCustom[] leg2Models;
    public ChangeMultipleCustom[] shoe2Models;

    // Start is called before the first frame update
    void Start()
    {
        hairIndex = Random.Range(0, 9);
        shirtIndex = Random.Range(0, 8);
        shortsIndex = Random.Range(0, 2);
        shoesIndex = Random.Range(0, 2);


        TrocaCabelo(hairIndex);
        TrocaCamisa(shirtIndex);
        TrocaCalca(shortsIndex);
        TrocaSapato(shoesIndex);

    }

    private void TrocaCabelo(int onlineIndex)
    {
        for (int i = 0; i < hairModels.Length; i++)
        {
            hairModels[i].ChangeCustom(false);
            hair2Models[i].ChangeCustom(false);
        }
        hairModels[onlineIndex].ChangeCustom(true);
        hair2Models[onlineIndex].ChangeCustom(true);
    }

    private void TrocaCamisa(int onlineIndex)
    {
        for (int i = 0; i < shirtModels.Length; i++)
        {
            shirtModels[i].ChangeCustom(false);
            shirt2Models[i].ChangeCustom(false);
        }
        shirtModels[onlineIndex].ChangeCustom(true);
        shirt2Models[onlineIndex].ChangeCustom(true);
    }

    private void TrocaCalca(int onlineIndex)
    {
        for (int i = 0; i < legModels.Length; i++)
        {
            legModels[i].ChangeCustom(false);
            leg2Models[i].ChangeCustom(false);
        }
        legModels[onlineIndex].ChangeCustom(true);
        leg2Models[onlineIndex].ChangeCustom(true);
    }

    private void TrocaSapato(int onlineIndex)
    {
        for (int i = 0; i < shoeModels.Length; i++)
        {
            shoeModels[i].ChangeCustom(false);
            shoe2Models[i].ChangeCustom(false);
        }
        shoeModels[onlineIndex].ChangeCustom(true);
        shoe2Models[onlineIndex].ChangeCustom(true);
    }
}
