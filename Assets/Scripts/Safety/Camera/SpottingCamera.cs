using System;
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
  [SerializeField] private int lTarget;
  [SerializeField] private int rTarget;
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
  
  [SerializeField] private Camera camera;
  [SerializeField] private float length = 10f;
  [SerializeField] private int segments = 20;

  [SerializeField] private MeshFilter meshFilter;

  #region Detection
  private void FixedUpdate()
  {
    if (!hasSpottedPlayer && _canMove && doesTurn) {
      HandleCameraTurn();
    }
    Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity);
    if (hit.collider is not null && hit.collider.TryGetComponent(out Player player)) {
      Detected(player.gameObject);
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
  }

  private IEnumerator DuringHack(int hackingDuration) {
    _canMove = false;
    yield return new WaitForSeconds(hackingDuration);
    _canMove = true;
  }
  #endregion
  #region Sight Renderer

  private void RenderSight()
  {
    Mesh mesh = new Mesh();
    
    Vector3[] vertices = new Vector3[segments + 2];
    Vector2[] uv = new Vector2[vertices.Length];
    int[] triangles = new int[segments * 3];
    
    float angle = GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad / 2f;
    float aspect = GetComponent<Camera>().aspect;

    Vector3 origin = GetComponent<Camera>().transform.position;
    Vector3 forward = GetComponent<Camera>().transform.forward;
    Vector3 right = GetComponent<Camera>().transform.right;
    Vector3 up = GetComponent<Camera>().transform.up;
    
    vertices[0] = origin + forward * length;
    uv[0] = new Vector2(0.5f, 0f); 
    
    for (int i = 0; i < segments; i++)
    {
      float theta = i * Mathf.PI * 2f / segments;
      float x = Mathf.Sin(theta) * Mathf.Tan(angle) * length * aspect;
      float y = Mathf.Cos(theta) * Mathf.Tan(angle) * length;
      vertices[i + 1] = origin + forward * length;
      uv[i + 1] = new Vector2(x, y);

      if (i < segments - 1)
      {
        triangles[i * 3] = 0;
        triangles[i * 3 + 1] = i + 1;
        triangles[i * 3 + 2] = i + 2;
      }
      else
      {
        triangles[i * 3] = 0;
        triangles[i * 3 + 1] = i + 1;
        triangles[i * 3 + 2] = 1;
      }
    }


    mesh.vertices = vertices;
    mesh.uv = uv;
    mesh.triangles = triangles;

    meshFilter.mesh = mesh;
  }
  #endregion
}
