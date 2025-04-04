using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable]
public class Upgrade : MonoBehaviour, IUpgrade
{
  public int id;
  [SerializeField] private int[] neededToUnlock;
  [SerializeField] private int cost;
  public UpgradeManager upgradeManager;
  [SerializeField] private UpgradeType type;

  [SerializeField] private Sprite bought, locked, unlocked;


    public void Init()
    {
      upgradeManager = UpgradeManager.Instance;
      //checks if this upgrades value in the manager is true or false (bought or not)
      if (upgradeManager.upgrades[this]) {
        GetComponent<UnityEngine.UI.Button>().enabled = false;
      }
      UpdateSprite();
      transform.GetChild(1).GetComponent<TMP_Text>().text = $"{cost}";
    }

    public void UpdateSprite() {
      bool upgradesMet = CheckIfUpgradesMet();
      UnityEngine.UI.Image image = GetComponent<UnityEngine.UI.Image>();
      if (!upgradesMet) {
        image.sprite = locked;
      }
      else if (upgradesMet) {
        if (upgradeManager.upgrades[this]) return;
        image.sprite = unlocked;
      }
      
    }

    public void AddUpgrade() {
      if (CheckIfUpgradesMet()) {
        if (GameManager.Instance.player.RemoveMoney(cost)) {
          upgradeManager.upgrades[this] = true;
          var @bool = type switch {
              UpgradeType.BACKPACK => upgradeManager.BackpackUpgrade(),
              UpgradeType.LOCKPICK => upgradeManager.LockpickUpgrade(),
              UpgradeType.WALKING => upgradeManager.WalkingSpeedUpgrade(),
              _ => throw new Exception($"Invalid enum value in script {this}."),
          };
          if (!@bool) {
            throw new Exception($"Adding upgrade failed, is the UpgradeManager set up correctly?");
          }
          GetComponent<UnityEngine.UI.Image>().sprite = bought;
        }
        else {
          print($"Can't afford, cost is {cost}");
        }
      }
      else {
        print($"Missing upgrades");
      }
    }

  //checks if previous upgrades have been bought
  public bool CheckIfUpgradesMet() {
    foreach (int i in neededToUnlock) {
      if (!upgradeManager.upgrades.ElementAt(i).Value) {
        return false;
      }
    }
    return true;
  }
}
