using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnSeeker spawner;
    public GameObject player;
    public float lifespan;
    [SerializeField]
    public int score = 0;

    public int[] layers = new int[3] { 5, 3, 2 };//initializing network to the right size

    private NeuralNet network;

    [Range(0.0001f, 1f)] public float MutationChance = 0.01f;

    [Range(0f, 1f)] public float MutationStrength = 0.5f;

    [Range(0.1f, 10f)] public float Gamespeed = 1f;


    void Start()
    {
        spawner = FindObjectOfType<SpawnSeeker>();
        SetupNetwork();
        SpawnCollectible();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { SpawnCollectible(); }
        if (Input.GetKeyDown(KeyCode.T)) { AddScore(); }
    }


    public void AddScore()
    {
        score++;
        FindObjectOfType<UIUpdate>().UpdateScore(score);
        SpawnCollectible();
    }

    public void ResetScore()
    {
        score = 0;
        FindObjectOfType<UIUpdate>().UpdateScore(score);
    }

    void SpawnCollectible()
    {
        FindObjectOfType<SpawnPointController>().RandomPoint();
    }

    void NewSeeker()
    {
        Time.timeScale = Gamespeed;//sets gamespeed, which will increase to speed up training
        ResetScore();
        GameObject b = GameObject.FindGameObjectWithTag("Bot");
        if (b != null)
        {
            GameObject.Destroy(b);
            UpdateNetwork();
        }
        AIController a = spawner.SpawnNewSeeker().GetComponent<AIController>();
        a.network = network;
    }

    void UpdateNetwork()
    {
        network.Save("Assets/Scripts/Player/AI/NNModel.txt");
        network = network.copy(new NeuralNet(layers));
        network.Mutate((int)(1 / MutationChance), MutationStrength);
    }

    public void SetupNetwork()
    {
        network = new NeuralNet(layers);
        if (!System.IO.File.Exists("Assets/Scripts/Player/AI/NNModel.txt"))
            System.IO.File.WriteAllText("Assets/Scripts/Player/AI/NNModel.txt", "");
        network.Load("Assets/Scripts/Player/AI/NNModel.txt");//on start load the network save

        InvokeRepeating("NewSeeker", 0.1f, lifespan);//repeating function
    }
}
