using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Safe : MonoBehaviour
{
  [SerializeField] private Transform cameraTarget;
  private Vector3 _previousTransform;
  private GameObject _camera;
  private bool _isUsingSafe;
  private InputAction _action;
  [SerializeField] private GameObject turnDial;

  private void Start() {
    _camera = Camera.main.gameObject;
  }
  public void OpenSafe() {
    _previousTransform = _camera.transform.position;
    StartCoroutine(LerpToTarget(cameraTarget, 1 * Time.deltaTime));
    GameManager.Instance.player.GetComponent<CameraMovement>().canMove = false;
    GameManager.Instance.player.GetComponent<Movement>().canMove = false;
    _action = GameManager.Instance.player.GetComponent<PlayerInput>().actions["Safe"];
    _action.performed += SafeInput;
    _isUsingSafe = true;
    cameraTarget.GetComponent<Light>().enabled = true;
  }

  public void CloseSafe() {
    GameManager.Instance.player.GetComponent<CameraMovement>().canMove = true;
    GameManager.Instance.player.GetComponent<Movement>().canMove = true;
    _action.performed -= SafeInput;
    _action = null;
    cameraTarget.GetComponent<Light>().enabled = false;
  }

  private void SafeInput(InputAction.CallbackContext ctx) {
    var input = ctx.ReadValue<float>();
    Vector3 rot = turnDial.transform.rotation.eulerAngles + new Vector3(input, 0, 0);
    turnDial.transform.rotation = Quaternion.Euler(rot);
  }

  private IEnumerator LerpToTarget(Transform to, float t) {
    while (transform.position != to.position) {
      _camera.transform.position = Vector3.Lerp(_camera.transform.position, to.position, t);
      _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, to.rotation, t);
      yield return null;
    }
  }
}
