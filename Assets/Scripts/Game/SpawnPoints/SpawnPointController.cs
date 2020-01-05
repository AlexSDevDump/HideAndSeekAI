using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    List<Transform> spawnPoints = new List<Transform>();
    public GameObject objToSpawn;
    private int lastSpawn;

    void Start()
    {
        InitialiseList();
    }

    void InitialiseList()
    {
        foreach (Transform t in transform)
        {
            spawnPoints.Add(t);
        }
    }

    public void RandomPoint()
    {
        int rand = Random.Range(0, spawnPoints.Count - 1);
        while(rand == lastSpawn) { rand = Random.Range(0, spawnPoints.Count - 1); }
        SpawnPoint(rand, objToSpawn);
        lastSpawn = rand;
    }

    void SpawnPoint(int i, GameObject obj)
    {
        Instantiate(obj, spawnPoints[i]);
    }
}
