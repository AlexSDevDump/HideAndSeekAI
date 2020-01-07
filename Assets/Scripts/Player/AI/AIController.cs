using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Rigidbody rb;
    Collider col;
    PlayerController pc;

    public LayerMask badLayers;
    public LayerMask goodLayers;

    public NeuralNet network;

    public bool alive;
    private float movement = 0;
    private float rotation = 0;
    public float fitness;
    public float[] inp;


    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        pc = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        col = GetComponentInChildren<Collider>();
    }

    void FixedUpdate()
    {
        if(alive)
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
                if (Physics.Raycast(transform.position, sensors[i], out hit, 10f, badLayers))
                {
                    if (hit.collider != null && hit.collider != col)
                    {
                        inp[i] =  1 - (10 - hit.distance) / 10;
                    }
                    else
                        inp[i] = 1;
                }
                    Debug.DrawRay(transform.position, sensors[i] * 10, Color.red);
            }

            if(Physics.Raycast(transform.position, sensors[2], out hit, 20f, goodLayers))
            {
                Debug.Log("I SEE A CHERRY");
                if (hit.collider != null && hit.collider != col)
                {
                    if(inp[2] < (20 - hit.distance) / 20)
                    {
                        inp[2] = (20 - hit.distance) / 20;
                    }
                }
            }

            Debug.DrawRay(transform.position, sensors[2] * 20, Color.green);

            float[] output = network.FeedForward(inp);
            AIMovement(output);
        }
    }

    void AIMovement(float[] o)
    {
        movement = o[1];
        if (movement != 0f) { pc.RunForward(movement); }

        rotation = o[0];
        if (rotation != 0f) { pc.Rotation(rotation); }
    }

    public void UpdateFitness()
    {
        network.fitness = FindObjectOfType<GameManager>().score;//updates fitness of network for sorting
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.gameObject.layer == badLayers)
        {
            alive = false;
        }
    }
}
