using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBuy : MonoBehaviour
{
	public int x;
	public int y;

	public BoolVariableMatrix blocked;

	void QualX(int value)
	{
		x = value;
	}

	void QualY(int value)
	{
		y = value;
	}


	public void LiberaCustom()
	{
		blocked.rows[x].row[y] = false;
		Destroy(gameObject);
	}
}
