using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    private Camera _camera;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject laptopUI;
    private Player _player;
    private Vector3 _position;

    private Movement _movement;
    private CameraMovement _camMovement;

    private void Start() {
      _camera = Camera.main;
      _player = GameManager.Instance.player;
      _movement = _player.GetComponent<Movement>();
      _camMovement = _player.GetComponent<CameraMovement>();  
    }
    public void Interact() {
      _position = _camera.transform.position;
      StartCoroutine(LerpToTarget(target, 1 * Time.deltaTime));
      laptopUI.SetActive(true);
      _movement.canMove = false;
      _camMovement.canMove = false;
      Cursor.lockState = CursorLockMode.None;
    }

    public void ExitLaptop() {
      StopAllCoroutines();
      _movement.canMove = true;
      _camMovement.canMove = true;
      Cursor.lockState = CursorLockMode.Locked;
      laptopUI.SetActive(false);
      _camera.transform.position = _position;
    }


  private IEnumerator LerpToTarget(Transform to, float t) {
    while (_camera.transform.position != to.position) {
      _camera.transform.position = Vector3.Lerp(_camera.transform.position, to.position, t);
      _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, to.rotation, t);
      yield return null;
    }
  }
}
