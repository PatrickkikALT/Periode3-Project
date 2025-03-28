using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Closet : MonoBehaviour, IInteractable
{

  [SerializeField] private GameObject leftDoor, rightDoor;
  private Vector3 _targetRotationL, _targetRotationR;
  private Vector3 _previousRotationL, _previousRotationR;
  private bool _hasOpenedCloset;
  [SerializeField] private bool twoDoors;
  [SerializeField] private bool opensLeftDoor;
  [SerializeField] private bool opensOneDoor;
  private void Start()
  {
    if (opensLeftDoor || twoDoors || opensOneDoor) {
      leftDoor = transform.GetChild(0).gameObject;
      _targetRotationL = leftDoor.transform.rotation.eulerAngles;
      _targetRotationL.y += 90;
      _previousRotationL = leftDoor.transform.rotation.eulerAngles;
      if (_targetRotationL.y < 0) {
        _targetRotationL.y = 360 + _targetRotationL.y;
      }
    }
    if (twoDoors || !opensLeftDoor || !opensOneDoor) {
      rightDoor = transform.GetChild(1).gameObject;
      _targetRotationR = rightDoor.transform.rotation.eulerAngles;
      _targetRotationR.y -= 90;
      _previousRotationR = rightDoor.transform.rotation.eulerAngles;
      if (_targetRotationR.y < 0) {
        _targetRotationR.y = 360 + _targetRotationR.y;
      }
    }
  }
  public void Interact()
  {
    StopAllCoroutines();
    if (!_hasOpenedCloset)
    {
      StartCoroutine(LerpDoor(_targetRotationL, _targetRotationR));
      _hasOpenedCloset = true;
    }
    else
    {
      StartCoroutine(LerpDoor(_previousRotationL, _previousRotationR));
      _hasOpenedCloset = false;
    }
  }


  private IEnumerator LerpDoor(Vector3 toL, Vector3 toR)
  {
    if (twoDoors) {
      while (leftDoor.transform.rotation.eulerAngles != toL) {
        leftDoor.transform.rotation = Quaternion.Euler(new Vector3(leftDoor.transform.rotation.eulerAngles.x,
                                                    Mathf.LerpAngle(leftDoor.transform.rotation.eulerAngles.y, toL.y, 1 * Time.deltaTime),
                                                    leftDoor.transform.rotation.eulerAngles.z));
        rightDoor.transform.rotation = Quaternion.Euler(new Vector3(rightDoor.transform.rotation.eulerAngles.x,
                                                    Mathf.LerpAngle(rightDoor.transform.rotation.eulerAngles.y, toR.y, 1 * Time.deltaTime),
                                                    rightDoor.transform.rotation.eulerAngles.z));
        yield return null;
      }
    }
    if (opensLeftDoor) {
      while (leftDoor.transform.rotation.eulerAngles != toL) {
        leftDoor.transform.rotation = Quaternion.Euler(new Vector3(leftDoor.transform.rotation.eulerAngles.x,
                                            Mathf.LerpAngle(leftDoor.transform.rotation.eulerAngles.y, toL.y, 1 * Time.deltaTime),
                                            leftDoor.transform.rotation.eulerAngles.z));
        yield return null;
      }
    }
    else if (!opensLeftDoor) {
      while (rightDoor.transform.rotation.eulerAngles != toR) {
        rightDoor.transform.rotation = Quaternion.Euler(new Vector3(rightDoor.transform.rotation.eulerAngles.x,
                                            Mathf.LerpAngle(rightDoor.transform.rotation.eulerAngles.y, toR.y, 1 * Time.deltaTime),
                                            rightDoor.transform.rotation.eulerAngles.z));
        yield return null;
      }
    }
  }
}
