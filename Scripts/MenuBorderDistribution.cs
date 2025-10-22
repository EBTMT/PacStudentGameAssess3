using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBorderDistribution : MonoBehaviour
{
    private float fallSpeed;
    private float spinSpeed;
    private bool looping;
    private float screenBottomY = -110f;

    public void Init(float fallSpeed, float spinSpeed, bool looping)
    {
        this.fallSpeed = fallSpeed;
        this.spinSpeed = spinSpeed;
        this.looping = looping;
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);

        if (transform.position.y < screenBottomY)
        {
            if (looping)
            {
                transform.position = new Vector3(Random.Range(-8f, 8f), 6f, 0f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
