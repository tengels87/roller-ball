using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    [Header("Tilt Settings")]
    public float maxTiltAngle = 30f;     // Maximum tilt in degrees
    public float sensitivity = 2f;       // Higher = more responsive
    public float smoothing = 5f;         // Higher = smoother, but slower

    private Vector3 smoothedAcceleration;

    void Start() {

        // Initialize with current acceleration so it doesn't snap on start
        smoothedAcceleration = Input.acceleration;
    }

    void Update() {
        // Raw accelerometer input
        Vector3 rawAcceleration = Input.acceleration;

        // Smooth using a low-pass filter
        smoothedAcceleration = Vector3.Lerp(
            smoothedAcceleration,
            rawAcceleration,
            Time.deltaTime * smoothing
        );

        // Map acceleration X/Y to tilt angles
        float tiltX = Mathf.Clamp(smoothedAcceleration.x * sensitivity * maxTiltAngle, -maxTiltAngle, maxTiltAngle);
        float tiltZ = Mathf.Clamp(smoothedAcceleration.y * sensitivity * maxTiltAngle, -maxTiltAngle, maxTiltAngle);

        // Apply tilt to platform
        Quaternion targetRotation = Quaternion.Euler(tiltZ, 0f, -tiltX);
        transform.rotation = targetRotation;
    }
}
