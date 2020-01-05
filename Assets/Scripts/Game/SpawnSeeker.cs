using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeeker : MonoBehaviour
{
    public GameObject seeker;

    public GameObject SpawnNewSeeker()
    {
        GameObject a = Instantiate(seeker, this.transform.position, this.transform.rotation);
        return a;
    }
}
