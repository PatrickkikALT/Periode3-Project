using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class HackCameras : MonoBehaviour, IAbility
{
    public Ability scriptableObject { get; set; }

    [SerializeField] private Ability so;

    void Start(){
      scriptableObject = so;
    }
    public void UseAbility() {
    Collider[] col = new Collider[10];
    Physics.OverlapSphereNonAlloc(transform.position, scriptableObject.radius, col);
    if (col.Length > 0) {
      foreach (Collider c in col) {
        if (c.TryGetComponent(out SpottingCamera cam)) {
          cam.Hack(scriptableObject.effectLength);
        }
      }
    }
  }
}
