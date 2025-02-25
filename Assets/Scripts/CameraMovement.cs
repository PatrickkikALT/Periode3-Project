using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float mouseSensitivity = 1;
    [SerializeField] private float gamepadSensitivity = 10;
    private float _verticalLookRotation;
    public bool canMove = true;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void LockCamera() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockCamera() {
        Cursor.lockState = CursorLockMode.None;
    }
    public void OnMove(InputAction.CallbackContext ctx) {
        if (!canMove) return;
        bool gamepad = ctx.control.device is Gamepad;
        float sens = gamepad ? gamepadSensitivity : mouseSensitivity;
        Vector2 rotation = ctx.ReadValue<Vector2>();
        transform.Rotate(Vector3.up * rotation.x * sens);
        _verticalLookRotation -=  rotation.y * sens;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90f, 90f);
        cameraHolder.localEulerAngles = new Vector3(_verticalLookRotation, 0, 0);
    }

    public void PickUp(InputAction.CallbackContext ctx) {
        if (!ctx.started) return;
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 10f);
        if (hitInfo.collider != null && hitInfo.collider.TryGetComponent(out Item item)) {
            item.PickUp();
        }
    }
}