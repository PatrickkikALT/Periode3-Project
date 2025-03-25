using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
  public Image fadeImage;
  [SerializeField] private Color transparent;

  private void Awake() {
    if (Instance == null) Instance = this; else Destroy(this);
    brokenGlass = Resources.Load("BrokenWindow");
    player = FindObjectOfType<Player>();
  }
  private void Start() {
    SceneManager.activeSceneChanged += OnSceneChange;
    StartCoroutine(CleanList());
  }

  private void OnSceneChange(Scene scene, Scene scene2) {
    player = FindObjectOfType<Player>();
    StartCoroutine(FadeIn());
  }

  private IEnumerator FadeIn() {
    while (fadeImage.color != transparent) {
      var c = fadeImage.color;
      c = Color.Lerp(c, transparent, 5 * Time.deltaTime);
      fadeImage.color = c;
      yield return null;
    }
  }

  private IEnumerator CleanList() {
    while (true) {
      currentlyBrokenGlass.RemoveAll(o => o == null);
      yield return new WaitForSeconds(10);
    }
  }
}
