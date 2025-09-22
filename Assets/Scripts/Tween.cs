using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public Transform Target { get; private set; }
    public Vector2 StartPos { get; private set; }
    public Vector2 EndPos { get; private set; }
    public float StartTime { get; private set; }
    public float Duration { get; private set; }

    public Tween(Transform setTarget, Vector2 setStartPos, Vector2 setEndPos, float setStartTime, float setDuration)
    {
        Target = setTarget;
        StartPos = setStartPos;
        EndPos = setEndPos;
        StartTime = setStartTime;
        Duration = setDuration;
    }

}
