using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using UnityEngine;
using UnityEngine.UI;
public class SoundDetector : MonoBehaviour
{
  public enum Severity
  {
    LOUD = 20,
    AUDIBLE = 10,
    QUIET = 5,
    NOTHING = 0
  }
  private int _counter;

  private int Counter
  {
    get
    {
      return _counter;
    }
    set
    {
      _counter = value;
      UpdateSoundIcon(value);
      if (value >= counterUntilAlarm)
      {
        Alarm();
      }
    }
  }
  [SerializeField] private Player player;
  public static SoundDetector Instance;
  [SerializeField] private Image soundIcon;
  [SerializeField] private Sprite[] sprites;
  [SerializeField] private int counterUntilAlarm;
  private bool _canCurrentlyReceiveSound = true;

  private void Awake()
  {
    if (Instance == null) Instance = this;
  }

  void Start()
  {
    StartCoroutine(ReduceCounter());
  }

  private void UpdateSoundIcon(int value)
  {
    switch (value)
    {
      case <= 1:
        soundIcon.sprite = sprites[0];
        soundIcon.color = Color.white;
        break;
      case <= 3:
        soundIcon.sprite = sprites[1];
        soundIcon.color = Color.white;
        break;
      case <= 6:
        soundIcon.sprite = sprites[2];
        soundIcon.color = Color.red;
        break;
      default:
        soundIcon.color = Color.red;
        soundIcon.sprite = sprites[3];
        break;
    }
    soundIcon.sprite = value switch
    {
      <= 1 => sprites[0],
      <= 3 => sprites[1],
      <= 6 => sprites[2],
      _ => sprites[3],
    };
    print($"Set soundIcon to value {value}");
  }

  public void ReceiveSound(GameObject sender, Severity severity)
  {
    if (!_canCurrentlyReceiveSound) return;
    float soundSeverity = GetSoundSeverity(severity, Vector3.Distance(transform.position, sender.transform.position));
    switch (soundSeverity)
    {
      case <= 1:
        break;
      case <= 3:
        Counter++;
        break;
      case <= 6:
        Counter += 4;
        break;
      default:
        Counter += 6;
        break;
    }
  }

  private static float GetSoundSeverity(Severity severity, float distance)
  {
    float result = distance switch
    {
      <= 10f => 6,
      <= 15f => 5,
      <= 20f => 4,
      <= 25f => 3,
      <= 30f => 2,
      _ => 1
    };
    return result * ((float)severity / 10);
  }

  private void Alarm()
  {
    print("Alarm went off");
    player.Lose();
  }

  private IEnumerator ReduceCounter()
  {
    while (true)
    {
      yield return new WaitForSeconds(10);
      if (Counter - 1 >= 0)
      {
        Counter -= 1;
      }
    }
  }

  public void Hack(int time)
  {
    Counter = 0;
    _canCurrentlyReceiveSound = false;
    StartCoroutine(DuringHack(time));
  }

  private IEnumerator DuringHack(int time)
  {
    yield return new WaitForSeconds(time);
    _canCurrentlyReceiveSound = true;
  }
}
