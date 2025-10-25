using System.Collections;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject bonusCherryPrefab;
    public float cherrySpeed = 2f;
    public float spawnDelay = 5f;
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public Vector2 levelCenter = new Vector2(13.5f, -14f);
    private GameObject activeCherry;

    void Start()
    {
        StartCoroutine(CherrySpawnRoutine());
    }

    IEnumerator CherrySpawnRoutine()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (true)
        {
            SpawnCherry();

            yield return new WaitUntil(() => activeCherry == null);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnCherry()
    {
        int side = Random.Range(0, 4);
        Vector2 spawnPos = Vector2.zero;
        switch (side)
        {
            case 0: 
                spawnPos = new Vector2(topLeft.x - 1f, Random.Range(bottomRight.y, topLeft.y));
                break;
            case 1: 
                spawnPos = new Vector2(bottomRight.x + 1f, Random.Range(bottomRight.y, topLeft.y));
                break;
            case 2: 
                spawnPos = new Vector2(Random.Range(topLeft.x, bottomRight.x), topLeft.y + 1f);
                break;
            case 3:
                spawnPos = new Vector2(Random.Range(topLeft.x, bottomRight.x), bottomRight.y - 1f);
                break;
        }

        Vector2 dirToCenter = (levelCenter - spawnPos).normalized;

        Vector2 centerToSpawn = spawnPos - levelCenter;
        Vector2 endPos = levelCenter - centerToSpawn + dirToCenter * 2f; 

        activeCherry = Instantiate(bonusCherryPrefab, spawnPos, Quaternion.identity);

        SpriteRenderer sr = activeCherry.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sortingOrder = 50; 

        StartCoroutine(MoveCherry(activeCherry, spawnPos, endPos));
    }

    IEnumerator MoveCherry(GameObject cherry, Vector2 start, Vector2 end)
    {
        float totalDistance = Vector2.Distance(start, end);
        float travel = 0f;

        while (cherry != null && travel < totalDistance)
        {
            travel += cherrySpeed * Time.deltaTime;
            float t = travel / totalDistance;
            cherry.transform.position = Vector2.Lerp(start, end, t);
            yield return null;
        }

        if (cherry != null)
            Destroy(cherry);
    }
}
