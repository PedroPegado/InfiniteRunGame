using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public List<GameObject> m_Thorns = new List<GameObject>();
    public float spawnRate = 3;
    private float timer = 0;

    private void Start()
    {
        spawnThorn();
    }

    private void FixedUpdate()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnThorn();
            spawnRate = Random.Range(3, 4.5f);
            timer = 0;
        }
    }

    void spawnThorn()
    {
        int index = Random.Range(0, m_Thorns.Count);
        GameObject selectedThorn = m_Thorns[index];

        Instantiate(selectedThorn, transform.position, Quaternion.identity);
    }
}
