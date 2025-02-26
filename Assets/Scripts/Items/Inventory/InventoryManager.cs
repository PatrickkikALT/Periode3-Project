using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour {
	[SerializeField] private List<ItemSO> items;
	[SerializeField] private int maxItems;
	public static InventoryManager Instance;
	[SerializeField] private GameObject inventoryPanel;

	private void Start() {
		if (Instance == null) Instance = this;
	}

	public ItemSO GetItem(int index) {
		return items[index];
	}

	public bool AddItem(ItemSO item, int weight) {
		if (items.Count + weight > maxItems) return false;
		for (int i = 0; i <= weight; i++) {
			items.Add(item);
		}
		return true;
	}

	public bool DropItem(int index) {
		if (index < 0 || index >= items.Count) return false;
		var item = items[index];
		items.RemoveAt(index);
		Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit hit, 10f);
		if (hit.point != Vector3.zero) {
			Instantiate(item.obj, hit.point, item.obj.transform.rotation);
		}
		else {
			var v3 = transform.position + transform.forward;
			Instantiate(item.obj, v3, item.obj.transform.rotation);
		}
		return true;
	}
	
	public ItemSO[] GetItems() => items.ToArray();

	public void OpenInventoryUI(InputAction.CallbackContext ctx) {
		inventoryPanel.SetActive(!inventoryPanel.activeSelf);

	}

	public int GetIndex(ItemSO item) {
		return items.IndexOf(item);
	}
}
