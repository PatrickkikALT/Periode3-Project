using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class HeadbobSystem : MonoBehaviour
{
    [Range(0.001f, 0.01f)]
    [SerializeField] private float amount = 0.002f;
    [Range(1f, 30f)]
    [SerializeField] private float frequency = 10.0f;
    [Range(10f, 100f)]
    [SerializeField] private float smooth = 10.0f;
    private Vector3 _startPos;
    private Movement _playerMovement;
    void Start()
    {
        _startPos = transform.localPosition;
        _playerMovement = GameManager.Instance.player.GetComponent<Movement>();
    }
    void Update()
    {
        CheckForHeadbobTrigger();
        StopHeadbob();
    }
    private void CheckForHeadbobTrigger()
    {
        float inputMagnitude = _playerMovement.input.magnitude;
        if (inputMagnitude > 0)
        {
            StartHeadBob();
        }
    }
    private Vector3 StartHeadBob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
        transform.localPosition += pos;
        return pos;
    }
    private void StopHeadbob()
    {
        if (transform.localPosition == _startPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, 1 * Time.deltaTime);
    }
}