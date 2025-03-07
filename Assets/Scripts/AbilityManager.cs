using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
  [SerializeField] private IAbility[] abilities;
  private bool _ability1OnCooldown, _ability2OnCooldown;
  public void UseAbility1(InputAction.CallbackContext ctx) {
    if (_ability1OnCooldown) {
      abilities[0].UseAbility();
      StartCoroutine(Cooldown(abilities[0], 0));
    }
  }
  public void UseAbility2(InputAction.CallbackContext ctx) {
    if (!_ability2OnCooldown) {
      abilities[1].UseAbility();
      StartCoroutine(Cooldown(abilities[1], 1));
    }
  }

  IEnumerator Cooldown(IAbility ability, int i) {
    yield return new WaitForSeconds(ability.GetCooldown());
    switch (i) {
      case 0:
        _ability1OnCooldown = true;
        break;
      case 1:
        _ability2OnCooldown = true;
        break;
    }
  }
}
