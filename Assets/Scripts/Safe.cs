using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Safe : MonoBehaviour
{
  [SerializeField] private Transform cameraTarget;
  private Vector3 _previousPosition;
  private GameObject _camera;
  private bool _isUsingSafe;
  private InputAction _action;
  private InputAction _spaceAction;
  [SerializeField] private GameObject turnDial;
  [SerializeField] private int buffer;
  [SerializeField] private int[] code;
  [SerializeField] private int currentNum = 0;
  [SerializeField] private GameObject door;
  [SerializeField] private Transform doorTarget;

  private void Start() {
    code = new int[] {
      Random.Range(1, 9), Random.Range(1, 9), Random.Range(1, 9), Random.Range(1, 9),
    };
    _camera = Camera.main.gameObject;
  }
  public void OpenSafe() {
    _previousPosition = _camera.transform.position;
    StartCoroutine(LerpToTarget(cameraTarget, 1 * Time.deltaTime));
    GameManager.Instance.player.GetComponent<CameraMovement>().canMove = false;
    GameManager.Instance.player.GetComponent<Movement>().canMove = false;
    _action = GameManager.Instance.player.GetComponent<PlayerInput>().actions["Safe"];
    _action.performed += SafeInput;
    _spaceAction = GameManager.Instance.player.GetComponent<PlayerInput>().actions["OpenSafe"];
    _spaceAction.performed += ConfirmNumber; 
    _isUsingSafe = true;
    cameraTarget.GetComponent<Light>().enabled = true;
  }

  public void FinishSafe() {
    GameManager.Instance.player.GetComponent<CameraMovement>().canMove = true;
    GameManager.Instance.player.GetComponent<Movement>().canMove = true;
    StopAllCoroutines();
    StartCoroutine(LerpDoor());
    _camera.transform.position = _previousPosition;
    _action.performed -= SafeInput;
    _action = null;
    _spaceAction.performed -= ConfirmNumber;
    _spaceAction = null;
    cameraTarget.GetComponent<Light>().enabled = false;
  }

  private bool IsInRangeWithBuffer(int valueToCompare, int target) {
    int adjustedMin = target - buffer;
    int adjustedMax = target + buffer;
    return (valueToCompare >= adjustedMin && valueToCompare <= adjustedMax) || valueToCompare == target;
  }

  private void SafeInput(InputAction.CallbackContext ctx) {
    float rotationAmount = Mathf.Approximately(ctx.ReadValue<float>(), -1) ? 45f : -45f;
    Quaternion currentRotation = turnDial.transform.rotation;
    Quaternion newRotation = Quaternion.Euler(rotationAmount, 0, 0) * currentRotation;
    turnDial.transform.rotation = newRotation;
    float currentXAngle = NormalizeAngle(turnDial.transform.eulerAngles.x);
  }
  private void ConfirmNumber(InputAction.CallbackContext ctx) {
    if (IsInRangeWithBuffer(Mathf.RoundToInt(NormalizeAngle(turnDial.transform.eulerAngles.x)), 
                            Mathf.RoundToInt(GameManager.Instance.safeNumberRotations[code[currentNum] - 1].x))) {
      print("Met rotation requirement");
      currentNum++;
      if (currentNum == code.Length) {
        print("Safe opened");
        FinishSafe();
      }
    }
    else {
      print($"Does not meet rotation requirement {GameManager.Instance.safeNumberRotations[code[currentNum] - 1].x}");
    }
  }
  
  private float NormalizeAngle(float angle) {
    return (angle + 180) % 360 - 180;
  }


  private IEnumerator LerpToTarget(Transform to, float t) {
    while (transform.position != to.position) {
      _camera.transform.position = Vector3.Lerp(_camera.transform.position, to.position, t);
      _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, to.rotation, t);
      yield return null;
    }
  }
  private IEnumerator LerpDoor() {
    while (!Mathf.Approximately(door.transform.rotation.eulerAngles.y, doorTarget.rotation.eulerAngles.y)) {
      door.transform.rotation = Quaternion.Lerp(door.transform.rotation, doorTarget.rotation, 1 * Time.deltaTime);
      yield return null;
    }
  }
}
