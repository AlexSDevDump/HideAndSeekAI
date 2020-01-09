using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnSeeker spawner;
    //public GameObject player;
    public float lifespan;
    private float currentLifespan;
    public int botCount;
    private bool running;
    [SerializeField]
    public int score = 0;

    public int[] layers = new int[3] { 5, 3, 2 };//initializing network to the right size

    private List<NeuralNet> networks;
    private List<AIController> bots;

    [Range(0.0001f, 1f)] public float MutationChance = 0.01f;

    [Range(0f, 1f)] public float MutationStrength = 0.5f;

    [Range(0.1f, 10f)] public float Gamespeed = 1f;


    void Start()
    {
        currentLifespan = lifespan;
        ResetScore();
        spawner = FindObjectOfType<SpawnSeeker>();
        SetupNetwork();
        SpawnCollectible();
        NewSeeker();
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { SpawnCollectible(); }
        if (Input.GetKeyDown(KeyCode.T)) { AddScore(); }

        if(running)
        {
            currentLifespan -= Time.deltaTime;
            if(currentLifespan <= 0)
            {
                currentLifespan = lifespan;
                NewSeeker();
            }
        }
    }


    public void AddScore()
    {
        currentLifespan = lifespan;
        score += 10;
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
        
        if (bots != null)
        {
            for(int i = 0; i < bots.Count; i++)
            {
                GameObject.Destroy(bots[i].gameObject);;
                ResetScore();
            }

            UpdateNetwork();
        }

        bots = new List<AIController>();
        for(int i = 0; i < botCount; i++)
        {
            AIController bot = spawner.SpawnNewSeeker().GetComponent<AIController>();
            bot.network = networks[i];
            bots.Add(bot);
        }
    }

    void UpdateNetwork()
    {
        for (int i = 0; i < botCount; i++)
        {
            bots[i].UpdateFitness();//gets bots to set their corrosponding networks fitness
        }
        networks.Sort();
        networks[botCount - 1].Save("Assets/Scripts/Player/AI/NNModel.txt");
        for (int i = 0; i < botCount / 2; i++)
        {
            networks[i] = networks[i + botCount / 2].copy(new NeuralNet(layers));
            networks[i].Mutate((int)(1 / MutationChance), MutationStrength);
        }
    }

    public void SetupNetwork()
    {
        networks = new List<NeuralNet>();
        for (int i = 0; i < botCount; i++)
        {
            NeuralNet net = new NeuralNet(layers);
            if (!System.IO.File.Exists("Assets/Scripts/Player/AI/NNModel.txt"))
                System.IO.File.WriteAllText("Assets/Scripts/Player/AI/NNModel.txt", "");
            net.Load("Assets/Scripts/Player/AI/NNModel.txt");//on start load the network save
            networks.Add(net);
        }
    }
}
