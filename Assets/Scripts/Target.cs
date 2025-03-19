using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject redCircle;

    public void OnEnter() {
        print("Seen mouse");
        redCircle.SetActive(true);
    }

    public void OnExit() {
        print("Lost mouse");
        redCircle.SetActive(false);
    }
}
