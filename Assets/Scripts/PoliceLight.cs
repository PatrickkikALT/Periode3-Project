using System.Collections;
using UnityEngine;

public class PoliceLight : MonoBehaviour
{
  private Light _light;
  [SerializeField] private AudioSource audio;
  void Start(){
    _light = GetComponent<Light>();  
    StartCoroutine(SwitchLight());
    audio.Play();
  }
  private IEnumerator SwitchLight() {
    while (true) {
        _light.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _light.color = Color.blue;
        yield return new WaitForSeconds(0.5f);
    }
  }
}
