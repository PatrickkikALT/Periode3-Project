using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    [SerializeField] private Light flashlight;
    [SerializeField] private GameObject youLost;
    [SerializeField] private TMP_Text moneyText;
    private int _money;
    public int money
    {
        get {
            return _money;
        }
        set {
            _money = value;
            UpdateMoneyText();
        }
    }

    public void UpdateMoneyText() => moneyText.text = $"${money}";
    public void AddMoney(int value) => money += value;
    public bool RemoveMoney(int value) {
        if (money - value < 0) return false;
        money -= value;
        return true;
    }
    public void Lose() {
        print("Player lost");
        youLost.SetActive(true);
        GetComponent<CameraMovement>().canMove = false;
        GetComponent<Movement>().canMove = false;
    }
    
    public void ToggleFlashlight(InputAction.CallbackContext ctx) {
        flashlight.enabled = !flashlight.enabled;
    }
}
