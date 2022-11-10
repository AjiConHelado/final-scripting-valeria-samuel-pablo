using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;
    [SerializeField] [Range(0f, 1f)] private float probability;

    public void Spawn()
    {
        if (Random.value < probability)
        {
            GameObject obj = Instantiate(toSpawn, transform.position, Quaternion.identity);
        }
    }
}
