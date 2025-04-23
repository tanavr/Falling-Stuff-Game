using UnityEngine;
using System.Collections.Generic;

public class TrashManager : MonoBehaviour
{
    public GameObject trashPrefab;
    public GameObject valuablePrefab;

    public int itemsToSpawn = 30;
    public float spawnInterval = 1.2f;
    public float xRange = 8f;
    public float spawnHeight = 6f;
    [Range(0f, 1f)] public float trashProbability = 0.7f;

    private int itemsSpawned = 0;
    private float timer = 0f;

    private List<Vector3> lastSpawnPositions = new List<Vector3>();
    private float minDistance = 1.5f;

    void Update()
    {
        if (itemsSpawned >= itemsToSpawn) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnItem();
            timer = 0f;
        }
    }

    void SpawnItem()
    {
        GameObject prefab = Random.value < trashProbability ? trashPrefab : valuablePrefab;
        Vector3 spawnPos;
        int attempt = 0;

        do
        {
            spawnPos = new Vector3(Random.Range(-xRange, xRange), spawnHeight, 0f);
            attempt++;
        } while (!IsFarEnough(spawnPos) && attempt < 10);

        lastSpawnPositions.Add(spawnPos);
        if (lastSpawnPositions.Count > 10)
            lastSpawnPositions.RemoveAt(0);

        Instantiate(prefab, spawnPos, Quaternion.identity);
        itemsSpawned++;
    }
    public void DecreaseSpawnInterval(float amount)
    {
        spawnInterval = Mathf.Max(0.5f, spawnInterval - amount);
        Debug.Log("Spawn interval decreased to: " + spawnInterval);
    }

    bool IsFarEnough(Vector3 pos)
    {
        foreach (Vector3 existing in lastSpawnPositions)
        {
            if (Vector3.Distance(existing, pos) < minDistance)
                return false;
        }
        return true;
    }
}
