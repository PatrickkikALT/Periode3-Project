using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    public Vector2 input;
    private Rigidbody _rb;
    public bool canMove = true;
    [SerializeField] private LayerMask stairLayer;
    private Transform _camera;
    private bool _isTouchingStairs => Physics.OverlapSphereNonAlloc(transform.position, overlapSphereSize, new Collider[2], stairLayer) > 0;
    public void OnMove(InputAction.CallbackContext ctx) {
        input = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx) {
        if (!ctx.performed || !canMove) return;
        if (onGround) {
            jumping = true;
            _rb.velocity = new Vector3(0, jumpStrength, 0);
        }
    }
    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main.transform;
    }

    private void Update() {
        if (!canMove) return;
        Vector3 dir = transform.TransformDirection(new Vector3(input.x, 0, input.y));
        _rb.velocity = new Vector3(dir.x * speed, _rb.velocity.y, dir.z * speed);
        if (_isTouchingStairs) {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z) * speed;
        }
        if (jumping) {
            UpdateJumpMomentum();
        }
    }
    
    [SerializeField] private bool jumping;
    [SerializeField] private float fallVel;
    [SerializeField] private float jumpStrength;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float overlapSphereSize;
    public bool onGround =>
        Physics.OverlapSphereNonAlloc(groundCheckTransform.position, overlapSphereSize, new Collider[10], groundLayers) > 0;


    private void UpdateJumpMomentum() {
        if (_rb.velocity.y < 0.5) {
            _rb.velocity -= new Vector3(0, fallVel * Time.deltaTime, 0);

            if (onGround) {
                jumping = false;
            }
        }
    }
}
