using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
enum UpgradeType{
  BACKPACK,
  LOCKPICK,
  WALKING
}

public class UpgradeManager : MonoBehaviour
{
    public SerializedDictionary<Upgrade, bool> upgrades = new SerializedDictionary<Upgrade, bool>();
    public static UpgradeManager Instance;

    public Player player;

    private int _currentBackpackLevel = 1;
    [SerializeField] private int maxBackpackLevel;
    private int _currentLockpickLevel = 1;
    [SerializeField] private int maxLockpickLevel;
    private int _currentWalkingSpeedLevel = 1;
    [SerializeField] private int maxWalkingSpeedLevel;



    void Awake() {
        if (Instance == null) Instance = this; else Destroy(this);
    }
    void Start() {

      player = GameManager.Instance.player; 
      var dict = upgrades;
      foreach (Upgrade upgrade in dict.Keys) {
        upgrade.Init();
        if (upgrades[upgrade]) {
          upgrade.AddUpgrade();
          upgrade.UpdateSprite();
        }
      }
    }

    void SpriteUpdate() {
      var dict = upgrades;
      foreach (Upgrade upgrade in dict.Keys) {
        upgrade.UpdateSprite();
      }
    }
    public bool BackpackUpgrade() {
      SpriteUpdate();
      if (_currentBackpackLevel >= maxBackpackLevel ) return false;
      player.GetComponent<InventoryManager>().maxWeight += 2;
      player.GetComponent<InventoryManager>().maxItems += 10;
      GameManager.Instance.backpackSize = player.GetComponent<InventoryManager>().maxItems;
      GameManager.Instance.maxWeight = player.GetComponent<InventoryManager>().maxWeight;
      _currentBackpackLevel++;
      return true;
    }

    public bool LockpickUpgrade() {
      SpriteUpdate();
      if (_currentLockpickLevel >= maxLockpickLevel) return false;
      player.lockpickSpeed -= 2;
      _currentLockpickLevel++;
      GameManager.Instance.lockpickSpeed = player.lockpickSpeed;
      return true;
    }

    public bool WalkingSpeedUpgrade() {
      SpriteUpdate();
      if (_currentWalkingSpeedLevel >= maxWalkingSpeedLevel) return false;
      player.GetComponent<Movement>().speed++;
      _currentWalkingSpeedLevel++;
      GameManager.Instance.walkingSpeed = player.GetComponent<Movement>().speed;
      return true;
    }
}
