using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Animator skeletonAnimator;
    public bool isDead = false;

    public void Start()
    {
        skeletonAnimator = GetComponent<Animator>();
    }

}
