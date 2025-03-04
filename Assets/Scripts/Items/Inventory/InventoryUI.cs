using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
	[SerializeField] private GameObject itemPanel;
	[SerializeField] private GameObject inventoryContent; 
	[SerializeField] private Player player;

	private void Start() {
		player = GameManager.Instance.player;
	}
	public void OnEnable() {
		Cursor.lockState = CursorLockMode.None;
		foreach (ItemSO item in InventoryManager.Instance.GetItems()) {
			var p = Instantiate(itemPanel, inventoryContent.transform);
			p.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
			p.GetComponent<ItemReference>().item = item;
		}
	}

	public void OnDisable() {
		player.GetComponent<CameraMovement>().canMove = true;
		Cursor.lockState = CursorLockMode.Locked;
		foreach (Transform child in inventoryContent.transform) {
			Destroy(child.gameObject);
		}
	}
}
