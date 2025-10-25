using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MenuBorder : MonoBehaviour
{
    public GameObject hatFall;
    public float spawnInterval = 0.5f;
    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = 6f;
    public float fallSpeed = 2f;
    public float spinSpeed = 50f;
    public bool looping = false;
    private float timer;

    void HatRain()
    {
        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), spawnY, 0f);
        GameObject hat = Instantiate(hatFall, spawnPos, Quaternion.identity, transform);
        hat.AddComponent<MenuBorderDistribution>().Init(fallSpeed, spinSpeed, looping);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            HatRain();
            timer = 0f;
        }
    }
}
