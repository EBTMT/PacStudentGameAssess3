using System.Collections;
using UnityEngine;

public class DustDestroyer : MonoBehaviour
{
 
    public float trailTime = 0.2f;

    private void Start()
    {
        Destroy(gameObject, trailTime);
    }

}
