using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public Vector3 arrivalOffset = Vector3.zero;
    public Vector3 inboundMovePointOffset = Vector3.zero;
    public Vector3 inboundDirection = Vector3.zero;

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var pac = other.GetComponent<PacStudentController>() ?? other.GetComponentInParent<PacStudentController>();
        if (pac == null) return;
        if (destination == null) return;

        Vector3 arrivalPos = destination.position + arrivalOffset;
        Vector3 inboundMovePointPos = arrivalPos + inboundMovePointOffset;

        pac.transform.position = arrivalPos;
        if (pac.movePoint != null)
            pac.movePoint.position = inboundMovePointPos;

        Vector3 dir = inboundDirection;
        if (dir == Vector3.zero && inboundMovePointOffset.sqrMagnitude > 0.0001f)
            dir = inboundMovePointOffset.normalized;

        pac.TeleportArriveSetDirection(dir);
    }
}
