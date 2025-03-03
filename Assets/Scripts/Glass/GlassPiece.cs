using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPiece : MonoBehaviour
{
  private void Start() {
    StartCoroutine(Dissappear());
  }

  IEnumerator Dissappear() {
    while (transform.localScale.z > 0.1) {
      transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
      yield return null;
    }
    Destroy(gameObject);
  }
}
