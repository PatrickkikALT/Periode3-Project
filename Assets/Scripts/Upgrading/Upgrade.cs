using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Upgrade : MonoBehaviour, IUpgrade
{
  [SerializeField] private int id;
  [SerializeField] private int[] neededToUnlock;
  [SerializeField] private int cost;
  public UpgradeManager upgradeManager;
  public void AddUpgrade()
  {
    if (CheckIfUpgradesMet()) {
      if (GameManager.Instance.player.RemoveMoney(cost)) {
        upgradeManager.upgrades[this] = true;
      }
      else {
        print($"Can't afford, cost is {cost}");
      }
    }
    else {
      print($"Missing upgrades");
    }
  }

  public bool CheckIfUpgradesMet() {
    foreach (int i in neededToUnlock) {
      if (!upgradeManager.upgrades.ElementAt(i).Value) {
        return false;
      }
    }
    return true;
  }
}
