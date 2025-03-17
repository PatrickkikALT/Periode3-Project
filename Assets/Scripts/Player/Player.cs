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
    public int Money
    {
        get => _money;
        set {
            _money = value;
            UpdateMoneyText();
        }
    }

    public List<Upgrade> boughtUpgrades = new();

    public void UpdateMoneyText() => moneyText.text = $"${Money}";
    public void AddMoney(int value) => Money += value;
    public bool RemoveMoney(int value) {
        if (Money - value < 0) return false;
        Money -= value;
        return true;
    }
    public void Lose() {
        print("Player lost");
        youLost.SetActive(true);
        GetComponent<CameraMovement>().canMove = false;
        GetComponent<Movement>().canMove = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    
    public void ToggleFlashlight(InputAction.CallbackContext ctx) {
        flashlight.enabled = !flashlight.enabled;
    }

    private void Start()
    {
        Money = Save.instance.saveData.money;
    }
}
