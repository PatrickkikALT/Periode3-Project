using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
  public int cooldown;
  public string abilityName;
  public string description;
  public int effectLength;
  public int radius;
}
