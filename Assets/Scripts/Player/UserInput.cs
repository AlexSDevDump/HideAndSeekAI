using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private float movement;
    private float rotation;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Vertical");
        if (movement != 0f) { pc.RunForward(movement); }

        rotation = Input.GetAxisRaw("Horizontal");
        if (rotation != 0f) { pc.Rotation(rotation); }
    }
}
