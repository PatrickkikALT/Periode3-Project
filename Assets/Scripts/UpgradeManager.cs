using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Dictionary<Upgrade, bool> upgrades;

    public static UpgradeManager Instance;


    void Awake() {
        if (Instance == null) Instance = this; else Destroy(this);
    }
    void Start() {
      foreach (Upgrade upgrade in upgrades.Keys) {
        upgrade.upgradeManager = this;
      }  
    }
}
