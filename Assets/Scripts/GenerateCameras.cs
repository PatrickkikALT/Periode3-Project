using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCameras : MonoBehaviour{
  [SerializeField] private GameObject cameraPrefab;
  void Start(){
    foreach(Transform t in transform) {
      if (Random.Range(0, 2) == 1) {
        var cam = Instantiate(cameraPrefab, t.position, t.rotation);
        cam.transform.localScale = new Vector3(1, 1, 1);
        cam.transform.GetChild(0).GetComponent<SpottingCamera>().lTarget = t.GetComponent<CameraPosition>().targetL;
        cam.transform.GetChild(0).GetComponent<SpottingCamera>().rTarget = t.GetComponent<CameraPosition>().targetR;
      }
    }
  }
}
