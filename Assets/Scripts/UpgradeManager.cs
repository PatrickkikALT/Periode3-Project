using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Dictionary<Upgrade, bool> upgrades;

    void Start()
    {
      foreach (Upgrade upgrade in upgrades.Keys) {
        upgrade.upgradeManager = this;
      }  
    }
    public void AddUpgrade(int id) {
        upgrades.ElementAt(id).Key.AddUpgrade();
    }
}
