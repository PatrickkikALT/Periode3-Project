using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
}
