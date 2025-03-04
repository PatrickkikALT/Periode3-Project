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
}
