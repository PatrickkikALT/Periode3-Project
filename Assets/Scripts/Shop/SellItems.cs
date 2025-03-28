using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItems : MonoBehaviour
{
  private Player _player;
  
  public void SellItemAt(int index) {
    if (_player == null) {
      _player = GameManager.Instance.player;
    }
    var playerInventory = _player.GetComponent<InventoryManager>();
    var items = playerInventory.GetItems();
      if (playerInventory.RemoveItemAt(index)) {
        _player.AddMoney(items[index].worth);
      }
      else {
        Debug.Log($"Failed to sell item at index {index}");
    }
  }
  public void SellAllItems() {
    if (_player == null) {
      _player = GameManager.Instance.player;
    }

    var inventory = _player.GetComponent<InventoryManager>();
    var items = inventory.GetItems();
    foreach (var item in items) {
      if (inventory.RemoveItem(item)) {
        _player.AddMoney(item.worth);
      }
    }
    Cursor.lockState = CursorLockMode.None;
  }
}
