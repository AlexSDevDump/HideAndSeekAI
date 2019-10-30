using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Movement(Vector3 moveDir)
    {
        moveDir = moveDir.normalized * speed;
        rb.MovePosition(rb.position + moveDir * Time.deltaTime);
    }
}
