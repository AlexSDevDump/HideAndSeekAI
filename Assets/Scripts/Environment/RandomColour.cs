using UnityEngine;

public class RandomColour : MonoBehaviour
{
    public Color randomColour;


    // Start is called before the first frame update
    void Start()
    {
        randomColour = GenerateColour();
        SetColour(randomColour);
    }
    
    Color GenerateColour()
    {
        Color colourBuffer = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        return colourBuffer;
    }

    void SetColour(Color colour)
    {
        GetComponent<Renderer>().material.color = colour;
    }
}
