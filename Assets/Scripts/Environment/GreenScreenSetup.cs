using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenScreenSetup : MonoBehaviour
{
    public RandomColour objectOnScreen;
    public bool useOverwrite;
    public Color overwriteColour;
    void Start()
    {
        if(useOverwrite) { SetColour(overwriteColour); }
        else { SetColour(InvertColour(objectOnScreen.randomColour)); }
    }

    Color InvertColour(Color colToInvert)
    {
        Color invertedColour = new Color(1f - colToInvert.r, 1f - colToInvert.g, 1f - colToInvert.b, 1);
        return invertedColour;
    }

    void SetColour(Color col)
    {
        GetComponent<Renderer>().material.color = col;
    }
}
