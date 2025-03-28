using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackSoundDetector : MonoBehaviour, IAbility
{
  public Ability scriptableObject { get; set; }
  public bool activated;
  [SerializeField] private Ability so;

  void Start(){
      scriptableObject = so;
      activated = GameManager.Instance.abilities[1];
  }
  public void UseAbility() {
    if (SoundDetector.Instance) {
      SoundDetector.Instance.Hack(scriptableObject.effectLength);
    }
  }
}
