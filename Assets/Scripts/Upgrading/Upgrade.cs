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
      //do upgrade things
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
