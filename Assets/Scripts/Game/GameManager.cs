using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float maxGameCountdown;
    [SerializeField]
    private float currentGameCountdown;
    [SerializeField]
    private int score = 0;

    void Start()
    {
        ResetTime();
        NewSeeker();
        SpawnCollectible();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { SpawnCollectible(); }
        if (Input.GetKeyDown(KeyCode.T)) { ResetTime(); }
        if (Input.GetKeyDown(KeyCode.E)) { AddScore(); }

        UpdateCountdown();
    }

    void UpdateCountdown()
    {
        if (currentGameCountdown > 0) { currentGameCountdown -= Time.deltaTime; }
        else { OutOfTime(); }
    }

    void OutOfTime()
    {
        player.SetActive(false);
    }

    void ResetTime()
    {
        currentGameCountdown = maxGameCountdown;
    }

    public void AddScore()
    {
        score++;
        FindObjectOfType<UIUpdate>().UpdateScore(score);
        ResetTime();
        SpawnCollectible();
    }

    void SpawnCollectible()
    {
        FindObjectOfType<SpawnPointController>().RandomPoint();
    }

    void NewSeeker()
    {
        player = FindObjectOfType<SpawnSeeker>().SpawnNewSeeker();
    }
}
