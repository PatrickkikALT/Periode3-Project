using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public int lockpickSpeed;
    [SerializeField] private Light flashlight;
    [SerializeField] private GameObject youLost;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private PoliceLight policeLight;
    private int _money;
    //Getter and setter to make money automatically update the moment its changed.
    public int Money
    {
        get => _money;
        set {
            _money = value;
            UpdateMoneyText();
        }
    }

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
        policeLight.gameObject.SetActive(true);
        StartCoroutine(WaitForLoss());
    }

    private IEnumerator WaitForLoss() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Main Menu");
    }
    
    public void ToggleFlashlight(InputAction.CallbackContext ctx) {
        flashlight.enabled = !flashlight.enabled;
    }

    private void Start()
    {
        Money = Save.instance.saveData.money;
    }

    [ContextMenu("Add Money")]
    private void MoneyDevTool() {
        Money += 500;
    }
}
