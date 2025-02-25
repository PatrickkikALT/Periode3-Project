using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Balancing + Start reducing counter if there hasn't been audio in a while.
public class SoundDetector : MonoBehaviour
{
    public enum Severity {
        LOUD = 20,
        AUDIBLE = 10,
        QUIET = 5,
        NOTHING = 0
    }
    private int _counter;
    [SerializeField] private Player player;
    public static SoundDetector Instance;

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    public void ReceiveSound(GameObject sender, Severity severity) {
        print($"Received Sound from {sender.name}");
        if (sender.layer != 8) return;
        print($"{sender.name} can send sound.");
        float soundSeverity = GetSoundSeverity(severity, Vector3.Distance(transform.position, sender.transform.position));
        switch (soundSeverity) {
            case <= 1:
                return;
            case <= 3:
                _counter++;
                break;
            case <= 6:
                _counter += 4;
                break;
            default:
                Alarm();
                break;
        }
    }
    
    private static float GetSoundSeverity(Severity severity, float distance) {
        float result = distance switch {
            <= 10f => 6,
            <= 15f => 5,
            <= 20f => 4,
            <= 25f => 3,
            <= 30f => 2,
            _ => 1
        };
        print(result * ((float)severity / 10));
        return result * ((float)severity / 10);
    }

    private void Alarm() {
        print("Alarm went off");
        player.Lose();
    }
}
