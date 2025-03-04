using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
  private bool _opened;
  private Vector3 _openTarget, _closeTarget;
  private void Start() {
    _openTarget = transform.rotation.eulerAngles;
    _openTarget.y -= 90;
    _closeTarget = transform.rotation.eulerAngles;
  }
  public void OpenDoor() {
    if (!_opened) {
      StopAllCoroutines();
      print($"Target rotation {_openTarget}");
      StartCoroutine(DoorAnimation(_openTarget));
      _opened = true;
    }
    else {
      StopAllCoroutines();
      print($"Target rotation {_closeTarget}");
      StartCoroutine(DoorAnimation(_closeTarget));
      _opened = false;
    }
  }

  public IEnumerator DoorAnimation(Vector3 target) {
    while (Math.Abs(transform.rotation.y - target.y) > 1) {
      transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(target), 1 * Time.deltaTime);
      yield return null;
    }
  }
}
