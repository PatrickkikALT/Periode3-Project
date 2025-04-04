using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
  [SerializeField] private AudioSource startAudio, loopAudio;
  [SerializeField] private AudioClip miku;
  [SerializeField] private AudioClip pinkPantherStart, pinkPantherLoop;
  [SerializeField] private int spawnSceneIndex;

  [SerializeField] private AudioClip ambient;
  void Start()
  {
    Coroutine coroutine = (SceneManager.GetActiveScene().buildIndex == spawnSceneIndex) ? StartCoroutine(AudioLoop(pinkPantherLoop, true)) : StartCoroutine(AudioLoop(ambient, false));
    coroutine = null;
  }

  private IEnumerator AudioLoop(AudioClip clip, bool shouldPlayStart) {
    loopAudio.clip = clip;
    if (shouldPlayStart) {
      startAudio.clip = pinkPantherStart;
      startAudio.Play();
      yield return new WaitUntil(() => !startAudio.isPlaying);
    }
    
    loopAudio.Play();
  }
}
