using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  [SerializeField] private GameObject redCircle;

  void IPointerEnterHandler.OnPointerEnter(PointerEventData baseData) {
    redCircle.SetActive(true);
    print("Cursor hovered over object.");
  }
  void IPointerExitHandler.OnPointerExit(PointerEventData eventData) {
    redCircle.SetActive(false);
    print("Cursor left object");
  }

  public void SwitchScene(int id) {
    StartCoroutine(FadeOut(id));
  }

  private IEnumerator FadeOut(int id) {
    while (GameManager.Instance.fadeImage.color != Color.black) {
      var c = GameManager.Instance.fadeImage.color;
      c = Color.Lerp(c, Color.black, 5 * Time.deltaTime);
      GameManager.Instance.fadeImage.color = c;
      yield return null;
    }
    SceneManager.LoadScene(id);
  }
}
