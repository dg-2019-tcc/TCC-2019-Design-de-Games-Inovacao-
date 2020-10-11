using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonBuy : MonoBehaviour
{
    public TMP_Text priceText;

	public int x;
	public int y;

	public BoolVariableMatrix blocked;
    public Points moedas;

    public CustomType customType;
    private int itemPrice;
    public int cabeloPrice;
    public int camisaPrice;
    public int shortsPrice;
    public int tenisPrice;
    public int oculosPrice;
    public int maskPrice;
    public int bonePrice;

	public void SetItemInfo(int getX, int getY)
	{
		x = getX;
		y = getY;
		QualItem();

	}


    private void QualItem()
    {
        switch (x)
        {
            case 0:
                customType = CustomType.Cabelo;
                break;

            case 1:
                customType = CustomType.Camisa;
                break;

            case 2:
                customType = CustomType.Tenis;
                break;

            case 3:
                customType = CustomType.Shorts;
                break;

            case 4:
                customType = CustomType.Oculos;
                break;

            case 6:
                customType = CustomType.Mask;
                break;

            case 7:
                customType = CustomType.Bone;
                break;
        }
        QualPrice();
    }

    private void QualPrice()
    {
        switch (customType)
        {
            case CustomType.Cabelo:
                itemPrice = cabeloPrice;
                break;

            case CustomType.Camisa:
                itemPrice = camisaPrice;
                break;

            case CustomType.Tenis:
                itemPrice = tenisPrice;
                break;

            case CustomType.Shorts:
                itemPrice = shortsPrice;
                break;

            case CustomType.Oculos:
                itemPrice = oculosPrice;
                break;

            case CustomType.Mask:
                itemPrice = maskPrice;
                break;

            case CustomType.Bone:
                itemPrice = bonePrice;
                break;
        }

        priceText.text = itemPrice.ToString();
    }


	public void LiberaCustom()
	{
        if (moedas.Value >= itemPrice)
        {
            moedas.Add(-itemPrice);
            blocked.rows[x].row[y] = false;

			PlayerPrefs.SetInt("Blocked_" + x + "_" + y, 0);
			Debug.Log("Blocked_" + x + "_" + y + " set to false on playerprefs");

			Destroy(gameObject);
        }
        else
        {
            moedas.JustShowCoins();
        }
	}

}
