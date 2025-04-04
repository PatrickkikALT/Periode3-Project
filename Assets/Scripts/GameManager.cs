using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
  public bool hasBoughtCrowbar;
  public Dictionary<int, bool> abilities = new();
  public HackCameras cameraHack;
  public HackSoundDetector soundDetectorHack;

  public int backpackSize = 20, lockpickSpeed = 10, maxWeight = 2;
  public float walkingSpeed = 5;

  public List<ItemSO> items = new();

  public float sfx, music;

  private void Awake() {
    
    if (Instance == null) Instance = this; else Destroy(gameObject);
    brokenGlass = Resources.Load("BrokenWindow");
    player = FindObjectOfType<Player>();
    abilities.Add(0, false);
    abilities.Add(1, false);
  }
  private void Start() {
    DontDestroyOnLoad(transform.parent);
    SceneManager.activeSceneChanged += OnSceneChange;
    SceneManager.sceneUnloaded += OnSceneUnload;
    StartCoroutine(CleanList());
    cameraHack = player.GetComponent<HackCameras>();
    soundDetectorHack = player.GetComponent<HackSoundDetector>();
  }

  private void OnSceneUnload(Scene scene) {
    items = player.GetComponent<InventoryManager>().GetItems().ToList();
    player = null;
  }
  private void OnSceneChange(Scene scene, Scene scene2) {
    player = FindObjectOfType<Player>();
    player.GetComponent<InventoryManager>().items = items;
    StartCoroutine(FadeIn());
    cameraHack = player.GetComponent<HackCameras>();
    soundDetectorHack = player.GetComponent<HackSoundDetector>();
  }

  private IEnumerator FadeIn() {
    while (fadeImage.color != transparent) {
      var c = fadeImage.color;
      c = Color.Lerp(c, transparent, 5 * Time.deltaTime);
      fadeImage.color = c;
      yield return null;
    }
  }
  void Update()
  {
    if (player == null) {
      player = FindAnyObjectByType<Player>();
    }
  }

  private IEnumerator CleanList() {
    while (true) {
      currentlyBrokenGlass.RemoveAll(o => o == null);
      yield return new WaitForSeconds(10);
    }
  }

  public void BuyCrowbar() {
    if (player.RemoveMoney(100)) {
      hasBoughtCrowbar = true;
    }
  }

  public void BuyAbility(int id) {
    if (player.RemoveMoney(100)) {
      abilities[id] = true;
    }
  }
}
