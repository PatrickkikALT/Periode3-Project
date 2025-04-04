using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
	[SerializeField] private GameObject itemPanel;
	[SerializeField] private GameObject inventoryContent; 
	[SerializeField] private Player player;
	[SerializeField] private TMP_Text limitText;
	private InventoryManager _inventory;

	private void Start() {
		player = GameManager.Instance.player;
		_inventory = player.GetComponent<InventoryManager>();
	}
	public void OnEnable() {
		if (player == null) {
			player = GameManager.Instance.player;
		}
		if (_inventory == null) {
			_inventory = player.GetComponent<InventoryManager>();
		}
		limitText.text = $"{_inventory.GetItems().Length}/{_inventory.maxItems}";
		Cursor.lockState = CursorLockMode.None;
		foreach (ItemSO item in InventoryManager.Instance.GetItems()) {
			var p = Instantiate(itemPanel, inventoryContent.transform);
			p.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = item.sprite;
			p.GetComponent<ItemReference>().item = item;
		}
	}

	public void OnDisable() {
		if (player == null) player = GameManager.Instance.player;
		player.GetComponent<CameraMovement>().canMove = true;
		Cursor.lockState = CursorLockMode.Locked;
		foreach (Transform child in inventoryContent.transform) {
			Destroy(child.gameObject);
		}
	}
}
