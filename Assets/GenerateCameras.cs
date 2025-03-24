using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCameras : MonoBehaviour{
  [SerializeField] private GameObject cameraPrefab;

  void Start(){
    foreach(Transform t in transform) {
      if (Random.Range(0, 5) == 3) {
        Instantiate(cameraPrefab, t.position, t.rotation);
      }
    }
  }
}
