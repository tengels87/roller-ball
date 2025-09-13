using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform ball;         // The ball to follow
    public Transform platform;     // The tilting platform

    [Header("Camera Settings")]
    public float heightAboveBall = 10f;  // How high above the ball the camera stays
    public float distance = 0f;          // Optional: keep some distance behind
    public float followSpeed = 5f;       // Smoothness of movement

    void LateUpdate() {
        if (ball == null || platform == null) return;

        // Get platform's "up" (its normal vector)
        Vector3 platformUp = platform.up;

        // Desired camera position: above the ball along platform's normal
        Vector3 targetPos = ball.position + platform.up * heightAboveBall;

        // Smoothly move the camera
        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
        transform.position = targetPos;

        // Always look at the ball, aligning "up" with the platform normal
        Vector3 forward = (ball.position - transform.position).normalized;
        Vector3 right = Vector3.Cross(platform.up, forward);
        //forward = Vector3.Cross(right, platform.up);

        transform.rotation = Quaternion.LookRotation(forward, platform.up);
    }
}
