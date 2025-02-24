using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLaser : MonoBehaviour
{
    private SpottingCamera _spottingCamera;

    private void Start() {
        _spottingCamera = GetComponentInParent<SpottingCamera>();
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out Player player)) {
            _spottingCamera.Detected(player.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.TryGetComponent(out Player player)) {
            _spottingCamera.Lost(player.gameObject);
        }
    }
}
