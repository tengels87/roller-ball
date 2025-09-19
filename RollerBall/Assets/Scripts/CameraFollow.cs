using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public GameObject followTarget;

    [Header("Camera Settings")]
    public float heightAboveBall = 10f;
    public float distance = 0f;
    public float followSpeed = 5f;

    void LateUpdate() {
        followTarget = GameObject.FindGameObjectWithTag("Player");

        if (followTarget == null) return;

        Vector3 targetPos = followTarget.transform.position + Vector3.up * heightAboveBall;

        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
        transform.position = targetPos;

        Vector3 forward = (followTarget.transform.position - transform.position).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}
