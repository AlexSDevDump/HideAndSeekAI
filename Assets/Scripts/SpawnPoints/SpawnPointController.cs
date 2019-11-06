using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    List<Transform> spawnPoints = new List<Transform>();
    public GameObject objToSpawn;

    void Start()
    {
        InitialiseList();
        RandomPoint();
    }

    void InitialiseList()
    {
        foreach (Transform t in transform)
        {
            spawnPoints.Add(t);
        }
    }

    void RandomPoint()
    {
        int rand = Random.Range(0, spawnPoints.Count - 1);
        SpawnPoint(rand, objToSpawn);
    }

    void SpawnPoint(int i, GameObject obj)
    {
        Instantiate(obj, spawnPoints[i]);
    }
}
