using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChooseMap : MonoBehaviour, IInteractable
{
    private Camera _camera;

    [SerializeField] private Transform target;
    private Player _player;
    private Vector3 _position;
    private Movement _movement;
    private CameraMovement _camMovement;
    [SerializeField] private Target[] targets;
    [SerializeField] private int buffer;

    private InputAction _quit;

    void Start()
    {
      _camera = Camera.main;
      _player = GameManager.Instance.player;
      _movement = _player.GetComponent<Movement>();
      _camMovement = _player.GetComponent<CameraMovement>();  
      _quit = _player.GetComponent<PlayerInput>().actions["Quit"];
    }

    public void Interact() {
      _position = _camera.transform.position;
      StartCoroutine(LerpToTarget(target, 1 * Time.deltaTime));
      _movement.canMove = false;
      _camMovement.canMove = false;
      Cursor.lockState = CursorLockMode.None;
      _quit.performed += ExitBoard;

    }

    public void ExitBoard(InputAction.CallbackContext ctx) {
      StopAllCoroutines();
      _quit.performed -= ExitBoard;
      _movement.canMove = true;
      _camMovement.canMove = true;
      Cursor.lockState = CursorLockMode.Locked;
    }

    private bool CheckForOverlap(Vector3 position, Vector3 toCompare) {
      bool x = position.x >= toCompare.x - buffer && position.x <= toCompare.x + buffer;
      bool y = position.y >= toCompare.y - buffer && position.y <= toCompare.y + buffer;
      return x && y;
    }

    private IEnumerator LerpToTarget(Transform to, float t) {
      while (_camera.transform.position != to.position) {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, to.position, t);
        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, to.rotation, t);
      yield return null;
      }
    }
}
