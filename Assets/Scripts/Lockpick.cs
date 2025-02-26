using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Lockpick : MonoBehaviour
{
    private bool _isSpinning = true;
    private GameObject _spinner;
    private GameObject _goal;
    private GameObject _turnPoint;
    private void Start() {
        _spinner = transform.GetChild(0).gameObject;
        _goal = transform.GetChild(1).gameObject;
        _turnPoint = _spinner.transform.GetChild(0).gameObject;
    }
    
    private void FixedUpdate() {
        if (_isSpinning) {
            _spinner.transform.RotateAround(_turnPoint.transform.position, Vector3.forward, 2);
        }
    }

    public void Interact(InputAction.CallbackContext ctx) {
        _isSpinning = false;
        if (GetWorldSpaceRect(_spinner.GetComponent<RectTransform>()).Overlaps(GetWorldSpaceRect(_goal.GetComponent<RectTransform>()), true)) {
            _goal.GetComponent<Image>().color = Color.green;
        }
        else {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait() {
        yield return new WaitForSeconds(0.5f);
        _isSpinning = true;
    }
    Rect GetWorldSpaceRect(RectTransform rt)
    {
        var r = rt.rect;
        r.center = rt.TransformPoint(r.center);
        r.size = rt.TransformVector(r.size);
        return r;
    }

}
