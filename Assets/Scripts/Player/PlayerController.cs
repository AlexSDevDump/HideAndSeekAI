using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void RunForward(float movement)
    {
        movement *= speed;
        Vector3 finalMovement = transform.forward * movement;
        rb.MovePosition(rb.position + finalMovement * Time.deltaTime);
    }

    public void Rotation(float rotation)
    {
        float finalRotation = 360f / rotation * rotSpeed;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, finalRotation, 0) * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
