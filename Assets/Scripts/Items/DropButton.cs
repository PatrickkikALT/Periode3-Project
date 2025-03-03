using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropButton : MonoBehaviour
{
	public void Drop() {
		ItemSO item = transform.parent.GetComponent<ItemReference>().item;
		int index = InventoryManager.Instance.GetIndex(item);
		var b = InventoryManager.Instance.DropItem(index);
		print(b);
		Destroy(transform.parent.gameObject);
	}
}
