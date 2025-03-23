using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackSoundDetector : MonoBehaviour, IAbility
{
  public Ability scriptableObject { get; set; }

  [SerializeField] private Ability so;

  void Start(){
      scriptableObject = so;
  }
  public void UseAbility() {
    if (SoundDetector.Instance) {
      SoundDetector.Instance.Hack(scriptableObject.effectLength);
    }
  }
}
