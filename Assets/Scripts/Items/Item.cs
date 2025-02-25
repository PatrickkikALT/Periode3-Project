using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour, IPickupable
{
	public ItemSO item;
	public void PickUp() {
		bool result = InventoryManager.Instance.AddItem(item, item.weight);
		// if (!result) {
		// 	Debug.Log($"Inventory is full, could not add {item.name}.");
		// 	return;
		// }
		Destroy(gameObject);
	}

	public int GetWorth() {
		return item.worth;
	}
}
