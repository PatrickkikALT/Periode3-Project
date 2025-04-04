using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
  [SerializeField] private AudioMixer mixer;
  [SerializeField] private TMP_Text sfxText, musicText;


  public void ChangeSfx(float value) {
    mixer.SetFloat("Sfx", Mathf.Log10(value) * 20);
    sfxText.text = $"{Mathf.RoundToInt(value * 100)}";
  }
  public void ChangeMusic(float value) {
    mixer.SetFloat("Music",Mathf.Log10(value) * 20);
    musicText.text = $"{Mathf.RoundToInt(value * 100)}";
  }

  public void SetFPS(string value) {
    int i = int.Parse(value);
    Application.targetFrameRate = i;
    if (i <= 0) Application.targetFrameRate = -1;
  }
  
  [SerializeField] private TMP_Dropdown[] resolutionDropdown;
  private Resolution[] _resolutions;
  private List<Resolution> resolutionList = new();
  private double _currentRefRate;
  [SerializeField] private int currentResolutionIndex = 0;
  void Start()
  {
    foreach(var v in resolutionDropdown) {
      v.ClearOptions();
    }
    _resolutions = Screen.resolutions;
    resolutionList = new List<Resolution>();
    _currentRefRate = Screen.currentResolution.refreshRateRatio.value;
    Debug.LogWarning(_resolutions.Length);
    foreach (var t in _resolutions) {
      float refRate = (float)_currentRefRate;
      float rate = (float)t.refreshRateRatio.value;
      if (Mathf.Approximately(refRate, rate)) {
        resolutionList.Add(t);
      }
      else {
#if UNITY_EDITOR
        Debug.LogWarning("Game started in Minimized, resolutions will be broken.");
#endif

      }
    }

    List<string> options = new List<string>();
    for (int i = 0; i < resolutionList.Count; i++) {
      string resolutionOption = resolutionList[i].width + "x" + resolutionList[i].height + " " + (int)resolutionList[i].refreshRateRatio.value + " Hz";
      options.Add(resolutionOption);
      if (resolutionList[i].width == Screen.width && resolutionList[i].height == Screen.height) {
        currentResolutionIndex = i;
      }
      foreach (var v in resolutionDropdown) {
        v.RefreshShownValue();
      }
    }
    foreach (var v in resolutionDropdown) {
      v.AddOptions(options);
      v.value = currentResolutionIndex;
      v.RefreshShownValue();
    }
    Debug.Log(resolutionList.Count.ToString());
    SetResolution(resolutionList.Count - 1);
  }
  public void SetResolution(int resolutionIndex) {
    Resolution resolution = resolutionList[resolutionIndex];
    Screen.SetResolution(resolution.width, resolution.height, true);
    foreach(var v in resolutionDropdown) {
      v.value = resolutionIndex;
      v.RefreshShownValue();
    }
  }
}
