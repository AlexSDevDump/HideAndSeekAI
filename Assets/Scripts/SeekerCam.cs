using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerCam : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<CameraSwitch>().SetSeekerCamPos(transform);
    }
}
