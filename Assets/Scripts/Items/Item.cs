using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour, IPickupable, IInteractable
{
	public ItemSO item;
	public bool PickUp() {
		bool result = InventoryManager.Instance.AddItem(item, item.weight);
		if (!result) {
			return false;
		}
		Destroy(gameObject, 0.01f);
		return true;
	}

	public void Interact() {
		PickUp();
	}

	public int GetWorth() {
		return item.worth;
	}
}
