using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Lockpick : MonoBehaviour
{
    private bool _isSpinning;
    private RectTransform _rectTransform;
    private int _targetRotation;
    
    private void Start() {
        _rectTransform = GetComponent<RectTransform>();
        _targetRotation = Random.Range(-180, 180);
    }
    
    private void FixedUpdate() {
        if (_isSpinning) {
            transform.Rotate(0, 0, 2);
        }
    }

    public void Interact(InputAction.CallbackContext ctx) {
        _isSpinning = false;
        if (Mathf.RoundToInt(_rectTransform.rotation.eulerAngles.z) == _targetRotation) {
            print("Lockpicked");
        }
    }
}
