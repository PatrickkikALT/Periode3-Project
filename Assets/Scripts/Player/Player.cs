using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    [SerializeField] private Light flashlight;
    public void Lose() {
        print("Player lost");
    }
    
    public void ToggleFlashlight(InputAction.CallbackContext ctx) {
        flashlight.enabled = !flashlight.enabled;
    }
}
