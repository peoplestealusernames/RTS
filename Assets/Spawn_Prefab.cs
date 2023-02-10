using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Prefab : MonoBehaviour
{
    public GameObject prefab;
    public float range = 1900.0f;

    private void Start()
    {
        for (int i = 0; i < 600; i++)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * range;
            randomPosition.y = 0;
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            GameObject instance = Instantiate(prefab, randomPosition, randomRotation);
            instance.transform.parent = transform;
        }
    }
}