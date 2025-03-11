using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour, IPickupable, IInteractable
{
	public ItemSO item;
	public void PickUp() {
		bool result = InventoryManager.Instance.AddItem(item, item.weight);
		if (!result) {
			return;
		}
		Destroy(gameObject);
	}

	public void Interact() {
		PickUp();
	}

	public int GetWorth() {
		return item.worth;
	}
}
