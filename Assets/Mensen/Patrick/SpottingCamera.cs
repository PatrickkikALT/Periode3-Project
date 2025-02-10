using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottingCamera : MonoBehaviour
 {
     [SerializeField] private Transform laserPoint;
     [SerializeField] private Transform turnPoint;
     [SerializeField] private Color color;
     private Quaternion _origin;
     private float _rightTarget;
     private float _leftTarget;
     private float _currentTarget;
 
     private void Start()
     {
         _origin = transform.rotation;
         _rightTarget = _origin.y + 90;
         _rightTarget = _origin.y - 90;
     }
 
     private void FixedUpdate()
     {
         //Handling turning for camera.
         Vector3 direction = transform.rotation.y >= _rightTarget ? Vector3.up : -Vector3.up;
         transform.RotateAround(turnPoint.position, direction, 10 * Time.deltaTime);
         

     }
}
