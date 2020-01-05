using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Rigidbody rb;
    Collider col;


    public float movement;
    public float fitness;
    public float[] inp;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        col = GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        Vector3[] sensors = new Vector3[]
        {
            transform.TransformDirection(Vector3.left),
            transform.TransformDirection((Vector3.left + Vector3.forward).normalized),
            transform.TransformDirection(Vector3.forward),
            transform.TransformDirection((Vector3.right + Vector3.forward).normalized),
            transform.TransformDirection(Vector3.right),
        };

        inp = new float[sensors.Length];

        for (int i = 0; i < sensors.Length; i++)
        {
            if (Physics.Raycast(transform.position, sensors[i], out hit))
            {
                if (hit.collider != null && hit.collider != col)
                {
                    inp[i] = hit.distance;
                }
            }

            Debug.DrawRay(transform.position, sensors[i] * 10, Color.red);
        }
    }
}
