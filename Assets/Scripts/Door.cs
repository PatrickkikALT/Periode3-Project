using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
  private bool _opened;
  private Quaternion _openTarget, _closeTarget;
  [SerializeField] private SoundDetector.Severity severity = SoundDetector.Severity.QUIET;
  public bool locked;

  [SerializeField] private AudioClip[] openAudio, closeAudio;
  [SerializeField] private AudioSource door;
 
  private void Start() {
    locked = UnityEngine.Random.Range(0, 2) == 1;
    _openTarget = transform.rotation * Quaternion.Euler(0, -90, 0);
    _closeTarget = transform.rotation;
  }

  public void Interact() {
    if (locked) {
      Lockpick.Instance.StartLockpick(this);
    }
    else {
      OpenDoor();
    }
  }

  private void OpenDoor() {
    if (SoundDetector.Instance != null) {
      SoundDetector.Instance.ReceiveSound(gameObject, severity);
    }
    if (!_opened) {
      StopAllCoroutines();
      door.clip = openAudio[UnityEngine.Random.Range(0, openAudio.Length - 1)];
      door.Play();
      print($"Target rotation {_openTarget.eulerAngles}");
      StartCoroutine(DoorAnimation(_openTarget));
      _opened = true;
    }
    else {
      StopAllCoroutines();
      door.clip = closeAudio[UnityEngine.Random.Range(0, closeAudio.Length - 1)];
      door.Play();
      print($"Target rotation {_closeTarget.eulerAngles}");
      StartCoroutine(DoorAnimation(_closeTarget));
      _opened = false;
    }
  }

  public IEnumerator DoorAnimation(Quaternion target) {
    while (Quaternion.Angle(transform.rotation, target) > 0.5f) {
      transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 2f);
      yield return null;
    }
    transform.rotation = target;
  }
}
