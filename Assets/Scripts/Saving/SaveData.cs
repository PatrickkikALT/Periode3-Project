using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
  public int backpackSize;
  public int walkingSpeed;
  public int lockpickSpeed;
  public int money;
  public int[] upgradesBought;

  public Dictionary<Upgrade, bool> upgrades;
}
