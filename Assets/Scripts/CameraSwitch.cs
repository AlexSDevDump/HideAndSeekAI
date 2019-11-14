using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCam;
    public List<Transform> camPos = new List<Transform>();
    public int currentCamID;
    // Start is called before the first frame update
    void Start()
    {
        DefaultCamSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) { MoveCamera(true); }
        if(Input.GetKeyDown(KeyCode.Q)) { MoveCamera(false); }
        if(Input.GetKeyDown(KeyCode.R)) { ResetTransform(); }
    }

    void DefaultCamSetting()
    {
        mainCam.transform.SetParent(camPos[0]);
        ResetTransform();
    }

    void MoveCamera(bool right)
    {
        if(right)
        {
            currentCamID++;
            if(currentCamID > camPos.Count - 1) { currentCamID = 0; }
            mainCam.transform.SetParent(camPos[currentCamID]);
        }

        else
        {
            currentCamID--;
            if (currentCamID < 0) { currentCamID = camPos.Count - 1; }
            mainCam.transform.SetParent(camPos[currentCamID]);
        }

        ResetTransform();
    }

    void ResetTransform()
    {
        Transform t = mainCam.transform;
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
    }
}
