using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public interface IAbility
{
  public Ability scriptableObject { get; set; }
  public void UseAbility();
  public int GetCooldown() => scriptableObject.cooldown;
}
