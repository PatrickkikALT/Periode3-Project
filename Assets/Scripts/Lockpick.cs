using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Lockpick : MonoBehaviour
{
  private GameObject _spinner;
  private GameObject _goal;
  private GameObject _turnPoint;
  private GameObject _image;

  private InputAction _space;
  public static Lockpick Instance;

  private Player _player;
  private Door _currentDoor;
  [SerializeField] private int speed;

  [SerializeField] private AudioSource unlock;

  void Awake()
  {
    if (Instance == null) Instance = this; else Destroy(this);
  }
  private void Start()
  {
    _image = transform.GetChild(0).gameObject;
    _spinner = _image.transform.GetChild(0).gameObject;
    _goal = _image.transform.GetChild(1).gameObject;
    _turnPoint = _spinner.transform.GetChild(0).gameObject;
    _player = GameManager.Instance.player;
    _space = _player.GetComponent<PlayerInput>().actions["Jump"];
  }

  private void FixedUpdate()
  {
    _spinner.transform.RotateAround(_turnPoint.transform.position, Vector3.forward, speed);
  }

  public void StartLockpick(Door door)
  {
    _currentDoor = door;
    _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    _player.GetComponent<Movement>().canMove = false;
    _player.GetComponent<CameraMovement>().canMove = false;
    _space.performed += Interact;
    _image.SetActive(true);
    Vector3 pos;
    pos.x = Random.Range(0, 2) == 1 ? Random.Range(-35, -20) : Random.Range(20, 35);
    pos.y = Random.Range(0, 2) == 1 ? Random.Range(-35, -20) : Random.Range(20, 35);
    pos.z = 0;
    _goal.GetComponent<RectTransform>().localPosition = pos;
  }
  public void Interact(InputAction.CallbackContext ctx)
  {
    if (GetWorldSpaceRect(_spinner.GetComponent<RectTransform>()).Overlaps(GetWorldSpaceRect(_goal.GetComponent<RectTransform>()), true))
    {
      _currentDoor.locked = false;
      unlock.Play();
      _image.SetActive(false);
      _player.GetComponent<Movement>().canMove = true;
      _player.GetComponent<CameraMovement>().canMove = true;
    }
  }

  public Rect GetWorldSpaceRect(RectTransform rt)
  {
    Vector3[] corners = new Vector3[4];
    rt.GetWorldCorners(corners);
    float minX = corners[0].x;
    float maxX = corners[0].x;
    float minY = corners[0].y;
    float maxY = corners[0].y;
    for (int i = 1; i < 4; i++)
    {
      minX = Mathf.Min(minX, corners[i].x);
      maxX = Mathf.Max(maxX, corners[i].x);
      minY = Mathf.Min(minY, corners[i].y);
      maxY = Mathf.Max(maxY, corners[i].y);
    }
    return Rect.MinMaxRect(minX, minY, maxX, maxY);
  }
}
