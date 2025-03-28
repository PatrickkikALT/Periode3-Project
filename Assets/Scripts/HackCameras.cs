using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class HackCameras : MonoBehaviour, IAbility
{
    public Ability scriptableObject { get; set; }
    public bool activated;

    [SerializeField] private Ability so;

    void Start(){
      scriptableObject = so;
      activated = GameManager.Instance.abilities[0];
    }
    public void UseAbility() {
      if (!activated) {
        print("has not bought ability"); 
        return;
      }
      Collider[] col = new Collider[10];
      Physics.OverlapSphereNonAlloc(transform.position, scriptableObject.radius, col);
      if (col.Length > 0 && col != null) {
        foreach (Collider c in col) {
          if (c == null) return;
          if (c.TryGetComponent(out SpottingCamera cam)) {
            cam.Hack(scriptableObject.effectLength);
          }
        }
      }
    }
}
