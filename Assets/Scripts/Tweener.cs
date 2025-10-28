using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private List<Tween> activeTween;
    void Start()
    {

    }
    public Tweener()
    {
        activeTween = new List<Tween>();
    }

    void Update()
    {
        // iterate backwards so we can safely remove finished tweens
        for (int i = activeTween.Count - 1; i >= 0; i--)
        {
            Tween t = activeTween[i];

            // normalized time [0,1] based on stored StartTime and Duration
            float timeFraction = 0f;
            if (t.Duration > 0f)
                timeFraction = Mathf.Clamp01((Time.time - t.StartTime) / t.Duration);
            else
                timeFraction = 1f;

            // interpolate from the stored StartPos to EndPos for consistent timing
            t.Target.position = Vector2.Lerp(t.StartPos, t.EndPos, timeFraction);

            // finish when timeFraction reaches 1 or practically at destination
            if (timeFraction >= 1f || Vector2.Distance(t.Target.position, t.EndPos) <= 0.01f)
            {
                t.Target.position = t.EndPos;
                activeTween.RemoveAt(i);
            }
        }
    }

    public bool AddTween(Transform targetObject, Vector2 startPos, Vector2 endPos, float duration)
    {
        if (TweenExists(targetObject) == false)
        {
            Tween newTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            activeTween.Add(newTween);
            return true;
        }
        else
            return false;
    }

    public bool TweenExists(Transform target)
    {
        return activeTween.Exists(x => x.Target == target);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private List<Tween> activeTween;
    void Start()
    {

    }
    public Tweener()
    {
        activeTween = new List<Tween>();
    }

    void Update()
    {
        for (int i = 0; i < activeTween.Count; i++)
        {
            if (Vector2.Distance(activeTween[i].Target.position, activeTween[i].EndPos) > 0.1f)
            {
                float timeFraction = ((Time.time - activeTween[i].StartTime) / activeTween[i].Duration);

                activeTween[i].Target.position = Vector2.Lerp(activeTween[i].Target.position, activeTween[i].EndPos, timeFraction);

            }
            if (Vector2.Distance(activeTween[i].Target.position, activeTween[i].EndPos) <= 0.1f)
            {
                activeTween[i].Target.position = activeTween[i].EndPos;
                activeTween.Remove(activeTween[i]);
            }
        }

    }

    public bool AddTween(Transform targetObject, Vector2 startPos, Vector2 endPos, float duration)
    {
        if (TweenExists(targetObject) == false)
        {
            Tween newTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            activeTween.Add(newTween);
            return true;
        }
        else
            return false;
    }

    public bool TweenExists(Transform target)
    {
        if (activeTween.Exists(x => x.Target == target))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
