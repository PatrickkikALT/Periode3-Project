using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
  private bool _opened;
  private Quaternion _openTarget, _closeTarget;

  private void Start() {
    _openTarget = transform.rotation * Quaternion.Euler(0, -90, 0);
    _closeTarget = transform.rotation;
  }

  public void OpenDoor() {
    if (!_opened) {
      StopAllCoroutines();
      print($"Target rotation {_openTarget.eulerAngles}");
      StartCoroutine(DoorAnimation(_openTarget));
      _opened = true;
    }
    else {
      StopAllCoroutines();
      print($"Target rotation {_closeTarget.eulerAngles}");
      StartCoroutine(DoorAnimation(_closeTarget));
      _opened = false;
    }
  }

  public IEnumerator DoorAnimation(Quaternion target) {
    while (Quaternion.Angle(transform.rotation, target) > 1f) {
      transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 2f);
      yield return null;
    }
    transform.rotation = target;
  }
}
