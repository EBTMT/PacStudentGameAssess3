using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustAlternator : MonoBehaviour
{
    public Sprite dustFrame1;
    public Sprite dustFrame2;
    public float frameDelay = 0.08f; 
    public float lifetime = 0.2f;    

    private SpriteRenderer sr;
    private bool showingFirst = true;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = dustFrame1;
        timer = frameDelay;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            showingFirst = !showingFirst;
            sr.sprite = showingFirst ? dustFrame1 : dustFrame2;
            timer = frameDelay;
        }
    }
}
