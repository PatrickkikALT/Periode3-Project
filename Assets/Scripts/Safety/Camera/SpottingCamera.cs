using System;
using System.Collections;
using UnityEngine;

public class SpottingCamera : MonoBehaviour
{
  [Header("Camera Movement")]
  [Tooltip("The transform of the point it should rotate around.")]
  [SerializeField] private Transform turnPoint;
  [Tooltip("The target of the rotation.")]
  public int lTarget;
  public int rTarget;
  [Tooltip("How fast the camera moves in degrees.")]
  [SerializeField] private int speed;

  [SerializeField] private bool doesTurn;
  private bool _movingRight = true;
  [Header("Player Detection")]
  public bool hasSpottedPlayer;
  private GameObject _spottedPlayer;
  private Coroutine _current;
  [SerializeField] private int secondsUntilDetection;
  private bool _canMove = true;
  [SerializeField] private GameObject sight;
  #region Detection
  private void FixedUpdate()
  {
    if (!hasSpottedPlayer && _canMove && doesTurn) {
      HandleCameraTurn();
    }
    // Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity);
    // if (hit.collider is not null && hit.collider.TryGetComponent(out Player player)) {
    //   Detected(player.gameObject);
    // }
  }

  public void Detected(GameObject player) {
    print("Seen player");
    _spottedPlayer = player;
    hasSpottedPlayer = true;
    _current = StartCoroutine(DetectPlayer());
  }
     
  public void Lost(GameObject player) {
    print("Lost player");
    if (_current == null) return;
    hasSpottedPlayer = false;
    _spottedPlayer = null;
    StopCoroutine(_current);
  }
     
     
  private IEnumerator DetectPlayer() {
    if (!hasSpottedPlayer) yield break;
    yield return new WaitForSeconds(secondsUntilDetection);
    print("Detected, you lost");
    _spottedPlayer.GetComponent<Player>().Lose();
  }
  #endregion
  #region Movement
  private void HandleCameraTurn() {
    float currentZ = transform.localEulerAngles.z;
    _movingRight = (!_movingRight || !(currentZ >= rTarget)) && (!_movingRight && currentZ <= lTarget || _movingRight);
    Vector3 direction = _movingRight ? Vector3.up : -Vector3.up;
    transform.RotateAround(turnPoint.position, direction, speed * Time.deltaTime);
  }
  #endregion
  #region Hacking
  public void Hack(int hackingDuration) {
    StartCoroutine(DuringHack(hackingDuration));
    sight.SetActive(false);
  }

  private IEnumerator DuringHack(int hackingDuration) {
    _canMove = false;
    yield return new WaitForSeconds(hackingDuration);
    _canMove = true;
    sight.SetActive(true);
  }
  #endregion
}
