using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCycle : MonoBehaviour
{
	private void Start() {
		StartCoroutine(Cycle());
	}

	private IEnumerator Cycle() {
		while (true) {
			transform.Rotate(-0.1f, 0, 0);
			yield return new WaitForSeconds(0.5f);
		}
	}
}
