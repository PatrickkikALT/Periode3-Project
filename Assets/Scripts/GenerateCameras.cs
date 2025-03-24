using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class GenerateCameras : MonoBehaviour{
  [SerializeField] private GameObject cameraPrefab;
  private List<Transform> _cameraPositions = new();
  private List<int> seenPositionIndex = new();
  void Start(){
    foreach (Transform t in transform) _cameraPositions.Add(t);

    for (int i = 0; i < Mathf.FloorToInt(_cameraPositions.Count * 0.75f); i++) {
      retry:
      var v = Random.Range(0, _cameraPositions.Count);
      if (seenPositionIndex.Contains(v)) goto retry;
      seenPositionIndex.Add(v);
      var cam = Instantiate(cameraPrefab, _cameraPositions[v].position, _cameraPositions[v].rotation);
      cam.transform.localScale = Vector3.one;
      cam.transform.GetChild(0).GetComponent<SpottingCamera>().lTarget = _cameraPositions[v].GetComponent<CameraPosition>().targetL;
      cam.transform.GetChild(0).GetComponent<SpottingCamera>().rTarget = _cameraPositions[v].GetComponent<CameraPosition>().targetR;
    }
  }
}
