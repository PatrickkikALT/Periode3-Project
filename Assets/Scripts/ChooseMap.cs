using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMap : MonoBehaviour, IInteractable
{
    private Camera _camera;

    [SerializeField] private Transform target;

    public void Interact() {
        StartCoroutine(LerpToTarget(target, 1 * Time.deltaTime));
    }

    private IEnumerator LerpToTarget(Transform to, float t) {
        while (transform.position != to.position) {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, to.position, t);
            _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, to.rotation, t);
        yield return null;
        }
    }
}
