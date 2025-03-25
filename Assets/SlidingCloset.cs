using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingCloset : MonoBehaviour, IInteractable
{
    private Vector3 _targetPosition;
    [SerializeField] private float target;

    private void Start() {
        _targetPosition.z = transform.localPosition.z + target;
    }
    public void Interact() {

    }
}
