using System.Collections;
using UnityEngine;

//TODO: Make camera move to aim at the player instead of just stopping.
//Tried transform.LookAt but prefab isn't pointing forward correctly, don't think this will be an issue once a proper model is added.
public class SpottingCamera : MonoBehaviour
{
  [Header("Camera Movement")]
  [Tooltip("The transform of the point it should rotate around.")]
  [SerializeField] private Transform turnPoint;
  [Tooltip("The target of the rotation.")]
  [SerializeField] private int target;
  [Tooltip("How fast the camera moves in degrees.")]
  [SerializeField] private int speed;
  private bool _movingRight = true;
  [Header("Player Detection")]
  public bool hasSpottedPlayer;
  private GameObject _spottedPlayer;
  private Coroutine _current;
  [SerializeField] private int secondsUntilDetection;
  private bool _canMove = true;
 
  private void FixedUpdate()
  {
    if (!hasSpottedPlayer && _canMove) {
      HandleCameraTurn();
    }
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
     
  private void HandleCameraTurn() {
    float yRotation = Mathf.DeltaAngle(0, transform.rotation.eulerAngles.y); 
    _movingRight = (!_movingRight || !(yRotation >= target)) && (!_movingRight && yRotation <= -target || _movingRight);
    Vector3 direction = _movingRight ? Vector3.up : -Vector3.up;
    transform.RotateAround(turnPoint.position, direction, speed * Time.deltaTime);
  }

  public void Hack(int hackingDuration) {
    StartCoroutine(DuringHack(hackingDuration));
  }

  private IEnumerator DuringHack(int hackingDuration) {
    _canMove = false;
    yield return new WaitForSeconds(hackingDuration);
    _canMove = true;
  }
}
