using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.TryGetComponent(out Player player)) {
        print("collision");
        SceneManager.LoadScene(0);
      }
    }

    private IEnumerator FadeOut() {
      while (GameManager.Instance.fadeImage.color != Color.black) {
        var c = GameManager.Instance.fadeImage.color;
        print(c);
        c = Color.Lerp(c, Color.black, 5 * Time.deltaTime);
        GameManager.Instance.fadeImage.color = c;
        yield return null;
      }
      SceneManager.LoadScene(0);
  }
}
