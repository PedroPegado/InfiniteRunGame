using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public List<SpawnableObject> spawnables = new List<SpawnableObject>();
    private float timer = 0f;
    private float currentSpawnRate;
    private SpawnableObject nextObjectToSpawn;
    public LayerMask spawnCheckMask;
    private void Start()
    {
        PickNextSpawn();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnRate)
        {
            SpawnObject(nextObjectToSpawn);
            PickNextSpawn();
            timer = 0f;
        }
    }

    void PickNextSpawn()
    {
        int index = Random.Range(0, spawnables.Count);
        nextObjectToSpawn = spawnables[index];
    }

    void SpawnObject(SpawnableObject spawnable)
    {
        Vector3 spawnPos = transform.position + spawnable.spawnOffset;

        Collider2D hit = Physics2D.OverlapCircle(spawnPos, 0.5f, spawnCheckMask);

        if (hit == null)
        {
            Instantiate(spawnable.prefab, spawnPos, Quaternion.identity);
        }

        currentSpawnRate = Random.Range(
            nextObjectToSpawn.spawnRateRange.x,
            nextObjectToSpawn.spawnRateRange.y
        );
    }


    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject prefab;
        public Vector3 spawnOffset;

        public Vector2 spawnRateRange;
    }
}
