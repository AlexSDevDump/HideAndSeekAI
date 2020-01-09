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

    public int badDetDist, goodDetDist;

    public NeuralNet network;

    public bool alive;
    private float movement = 0;
    private float rotation = 0;
    public float fitness;
    private float barrierPen;
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
            transform.TransformDirection(Vector3.forward),
            transform.TransformDirection(Vector3.left),
            transform.TransformDirection((Vector3.left + Vector3.forward).normalized),
            transform.TransformDirection((Vector3.right + Vector3.forward).normalized),
            transform.TransformDirection(Vector3.right),
            };

            inp = new float[sensors.Length];

            if (Physics.Raycast(transform.position, sensors[0], out hit, badDetDist * 3, badLayers))
            {
                if (hit.collider != null && hit.collider != col)
                {
                    inp[0] = 1 - (hit.distance / badDetDist * 3);
                }
                else
                    inp[0] = 0;
            }
            Debug.DrawRay(transform.position, sensors[0] * badDetDist, Color.red);

            for (int i = 1; i < sensors.Length; i++)
            {
                if (Physics.Raycast(transform.position, sensors[i], out hit, badDetDist, badLayers))
                {
                    if (hit.collider != null && hit.collider != col)
                    {
                        inp[i] =  1 - (hit.distance / badDetDist);
                    }
                    else
                        inp[i] = 0;
                }
                    Debug.DrawRay(transform.position, sensors[i] * badDetDist, Color.red);
            }

            if(Physics.Raycast(transform.position, sensors[0], out hit, goodDetDist, goodLayers))
            {
                fitness += Time.deltaTime;
                if (hit.collider != null && hit.collider != col)
                {
                    inp[0] = (hit.distance / goodDetDist);
                }
            }
            Debug.DrawRay(transform.position, sensors[0] * goodDetDist, Color.green);

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
        float finalFitness = FindObjectOfType<GameManager>().score + fitness;
        if (!alive)
            finalFitness -= 1;
        if(finalFitness < 0) { finalFitness = 0; }
        network.fitness = finalFitness;//updates fitness of network
    }

    void KillAI()
    {
        alive = false;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Barrier"))
        {
            KillAI();
        }
    }
}
