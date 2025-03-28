using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour {
	public List<ItemSO> items;
	[SerializeField] private int maxItems;
	public static InventoryManager Instance;
	[SerializeField] private GameObject inventoryPanel;
	[SerializeField] private GameObject inventoryContent;
	[SerializeField] private TMP_Text infoText;
	public int maxWeight;
	private void Start() {
		if (Instance == null) Instance = this; else Destroy(this);
	}

	public ItemSO GetItem(int index) {
		return items[index];
	}

	//Tries to add the item to the inventory but if it fails returns false so it can be handled further within the item script.
	public bool AddItem(ItemSO item, int weight) {
		if (weight > maxWeight) {
			SetInfoText("This item is too heavy...");
			return false;
		}
		if (items.Count + 1 >= maxItems) {
			SetInfoText("My backpack is full..");
			return false;
		}
		items.Add(item);
		return true;
	}

	public void SetInfoText(string text) {
			StopAllCoroutines();
			infoText.gameObject.SetActive(true);
			infoText.text = text;
			StartCoroutine(TextDisappear());
	}


	private IEnumerator TextDisappear() {
		yield return new WaitForSeconds(1f);
		var oldLength = infoText.text.Length;
		for (int i = 0; i < oldLength; i++) {
			infoText.text = infoText.text[0..^1];
			yield return new WaitForSeconds(0.05f);
		}
	}

	public bool DropItem(int index) {
		if (index < 0 || index >= items.Count) return false;
		var item = items[index];
		items.RemoveAt(index);
		Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit hit, 5f);
		if (hit.point != Vector3.zero) {
			Instantiate(item.obj, hit.point, item.obj.transform.rotation);
		}
		else {
			var v3 = transform.position + transform.forward;
			Instantiate(item.obj, v3, item.obj.transform.rotation);
		}
		return true;
	}

	public bool RemoveItem(ItemSO item) {
		if (!items.Contains(item)) return false;
		items.Remove(item);
		return true;
	}
	public bool RemoveItemAt(int index) {
		if (index < 0 || index >= items.Count) return false;
		try {
			items.RemoveAt(index);
			Destroy(inventoryContent.transform.GetChild(index).gameObject);
			return true;
		}
		catch {
			return false;
		}
	}
	
	public ItemSO[] GetItems() => items.ToArray();

	public void OpenInventoryUI(InputAction.CallbackContext ctx) {
		if (GameManager.Instance.isPlayerUsingSafe) return;
		GameManager.Instance.player.GetComponent<CameraMovement>().canMove = false;
		inventoryPanel.SetActive(!inventoryPanel.activeSelf);

	}

	public int GetIndex(ItemSO item) {
		return items.IndexOf(item);
	}
}
