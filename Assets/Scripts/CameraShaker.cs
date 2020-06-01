using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float period = 15f;
    public float shakeDuration = 2f;
    public float shakeMagnitude = 0.7f;

    private float periodCountdown;
    private float shakeCountdown;

    // The initial position of the GameObject
    Vector3 startPosition;

    void Start()
    {
        shakeCountdown = shakeDuration;
        periodCountdown = period;
        startPosition = transform.localPosition;
    }

    void Update()
    {
        periodCountdown -= Time.deltaTime;
        if (periodCountdown <= 0)
        {
            shakeCountdown = shakeDuration;
            periodCountdown = period;
        }

        if (shakeCountdown > 0)
        {
            transform.localPosition = startPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeCountdown -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = startPosition;
        }
    }
}
