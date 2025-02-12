using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLaser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("Collided with " + other.name);
        if (other.gameObject.TryGetComponent(out Player player))
        {
            GetComponentInParent<SpottingCamera>().Detected(player.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            GetComponentInParent<SpottingCamera>().Lost(player.gameObject);
        }
    }
}
