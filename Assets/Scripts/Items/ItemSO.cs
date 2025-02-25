using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items")]
[System.Serializable]
public class ItemSO : ScriptableObject {
	public int worth;
	public int weight;
	public GameObject obj;
	public Sprite sprite;
}
