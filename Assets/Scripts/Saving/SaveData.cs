using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
[Serializable]
public class SaveData
{
  public int backpackSize;
  public int walkingSpeed;
  public int lockpickSpeed;
  public int money;
  public SerializedDictionary<Upgrade, bool> upgrades;
}
