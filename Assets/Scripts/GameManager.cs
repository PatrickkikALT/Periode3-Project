using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
  public List<GameObject> currentlyBrokenGlass = new();
  public int maxBreakableGlass;
  public Object brokenGlass;
  public static GameManager Instance;
  public List<Vector3> safeNumberRotations;
  public Player player { get; private set; }
  public bool isPlayerUsingSafe;

  private void Awake() {
    if (Instance == null) Instance = this; else Destroy(this);
    brokenGlass = Resources.Load("BrokenWindow");
    player = FindObjectOfType<Player>();
  }
  private void Start() {
    
    StartCoroutine(CleanList());
  }

  private IEnumerator CleanList() {
    while (true) {
      currentlyBrokenGlass.RemoveAll(o => o == null);
      yield return new WaitForSeconds(10);
    }
  }
}
