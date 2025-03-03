using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepRigidbody : MonoBehaviour
{
  private void Awake() {
    GetComponent<Rigidbody>().Sleep();
  }
}
