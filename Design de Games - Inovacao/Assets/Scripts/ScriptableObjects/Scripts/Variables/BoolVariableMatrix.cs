using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/BoolMatrix")]

public class BoolVariableMatrix : ScriptableObject
{
	public bool Value(int x, int y)
	{
		return rows[x].row[y];
	}


	[System.Serializable]
	public struct rowData
	{
		public bool[] row;
	}
	public rowData[] rows = new rowData[10];

	
}
