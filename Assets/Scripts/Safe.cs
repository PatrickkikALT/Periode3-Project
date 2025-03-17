using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Safe : MonoBehaviour, IInteractable
{
  [SerializeField] private Transform cameraTarget;
  private Vector3 _previousPosition;
  private GameObject _camera;
  private InputAction _turnAction;
  private InputAction _spaceAction;
  private InputAction _exitAction;
  [SerializeField] private GameObject turnDial;
  [SerializeField] private int buffer;
  [SerializeField] private int[] code;
  [SerializeField] private int currentNum = 0;
  [SerializeField] private GameObject door;
  [SerializeField] private Transform doorTarget;
  [SerializeField] private int amountToRotate;
  private bool _hasOpenedSafe;

  private void Start() {
    code = new int[] {
      Random.Range(1, 9), Random.Range(1, 9), Random.Range(1, 9), Random.Range(1, 9),
    };
    _camera = Camera.main.gameObject;
  }

  public void Interact() {
    if (_hasOpenedSafe) return;
    OpenSafe();
  }
  public void OpenSafe() {
    _previousPosition = _camera.transform.position;
    StartCoroutine(LerpToTarget(cameraTarget, 1 * Time.deltaTime));
    GameManager.Instance.player.GetComponent<CameraMovement>().canMove = false;
    GameManager.Instance.player.GetComponent<Movement>().canMove = false;
    _turnAction = GameManager.Instance.player.GetComponent<PlayerInput>().actions["Safe"];
    _turnAction.performed += SafeInput;
    _spaceAction = GameManager.Instance.player.GetComponent<PlayerInput>().actions["OpenSafe"];
    _spaceAction.performed += ConfirmNumber;
    _exitAction = GameManager.Instance.player.GetComponent<PlayerInput>().actions["Quit"];
    _exitAction.performed += CloseSafe;
    GameManager.Instance.isPlayerUsingSafe = true;
    cameraTarget.GetComponent<Light>().enabled = true;
  }

  private void CloseSafe(InputAction.CallbackContext ctx) {
    StopAllCoroutines();
    GameManager.Instance.player.GetComponent<CameraMovement>().canMove = true;
    GameManager.Instance.player.GetComponent<Movement>().canMove = true;
    _turnAction.performed -= SafeInput;
    _turnAction = null;
    _spaceAction.performed -= ConfirmNumber;
    _spaceAction = null;
    _exitAction.performed -= CloseSafe;
    _exitAction = null;
    cameraTarget.GetComponent<Light>().enabled = false;
    GameManager.Instance.isPlayerUsingSafe = false;
    _camera.transform.position = _previousPosition;
  }
  
  public void FinishSafe() {
    GameManager.Instance.player.GetComponent<CameraMovement>().canMove = true;
    GameManager.Instance.player.GetComponent<Movement>().canMove = true;
    StopAllCoroutines();
    StartCoroutine(LerpDoor());
    _turnAction.performed -= SafeInput;
    _turnAction = null;
    _spaceAction.performed -= ConfirmNumber;
    _spaceAction = null;
    _exitAction.performed -= CloseSafe;
    _exitAction = null;
    cameraTarget.GetComponent<Light>().enabled = false;
    GameManager.Instance.isPlayerUsingSafe = false;
    _camera.transform.position = _previousPosition;
    
  }
  
  private void SafeInput(InputAction.CallbackContext ctx) {
    float rotationAmount = Mathf.Approximately(ctx.ReadValue<float>(), -1) ? amountToRotate : -amountToRotate;
    Quaternion currentRotation = turnDial.transform.rotation;
    Quaternion newRotation = Quaternion.Euler(rotationAmount, 0, 0) * currentRotation;
    turnDial.transform.rotation = newRotation;
  }
  
  private void ConfirmNumber(InputAction.CallbackContext ctx) {
    if (IsInRangeWithBuffer(Mathf.RoundToInt(NormalizeAngle(turnDial.transform.eulerAngles.x)), Mathf.RoundToInt(GameManager.Instance.safeNumberRotations[code[currentNum] - 1].x), buffer)) {
      print("Met rotation requirement");
      currentNum++;
      if (currentNum == code.Length) {
        print("Safe opened");
        FinishSafe();
      }
    }
    else {
      print($"Does not meet rotation requirement {GameManager.Instance.safeNumberRotations[code[currentNum] - 1].x}");
      currentNum = 0;
    }
  }
  
  #region Lerp Coroutines
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
  #endregion
  
  #region Static Calculation Methods
  private static float NormalizeAngle(float angle) {
    return (angle + 180) % 360 - 180;
  }

  private static bool IsInRangeWithBuffer(int valueToCompare, int target, int b) {
    int adjustedMin = target - b;
    int adjustedMax = target + b;
    return (valueToCompare >= adjustedMin && valueToCompare <= adjustedMax) || valueToCompare == target;
  }
  #endregion
}
