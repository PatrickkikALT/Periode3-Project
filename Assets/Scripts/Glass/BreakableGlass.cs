using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class BreakableGlass : MonoBehaviour, IInteractable
{
  [SerializeField] private SoundDetector.Severity severity;
  private Rigidbody _rb;
  private Object _brokenGlass;

  [SerializeField] private AudioSource @break;


  public void Interact() {
    Break();
  }
  private void Start() {
    _brokenGlass = GameManager.Instance.brokenGlass;
    _rb = GetComponent<Rigidbody>();
    @break = transform.parent.GetComponent<AudioSource>();
  }
  void FixedUpdate() {
    if (_rb.velocity.magnitude > 2f) {
      Break();
    }
  }
  [ContextMenu("Break")]
  public void Break() {
    @break.Play();
    if (SoundDetector.Instance != null) {
      SoundDetector.Instance.ReceiveSound(gameObject, severity);
    }
    if (GameManager.Instance.currentlyBrokenGlass.Count < GameManager.Instance.maxBreakableGlass) {
      var g = Instantiate(_brokenGlass, transform.position, transform.rotation);
      GameManager.Instance.currentlyBrokenGlass.Add((GameObject)g);
    }
    Destroy(transform.gameObject);
  }
}
