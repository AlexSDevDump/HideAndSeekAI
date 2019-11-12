using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour {

    public Text scoreCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateScore(int score)
    {
        scoreCount.text = "CURRENT SCORE: " + score.ToString();
    }
}
