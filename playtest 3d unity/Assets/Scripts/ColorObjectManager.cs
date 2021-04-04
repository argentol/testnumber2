using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObjectManager : MonoBehaviour
{

    public Material pipeColor1_;
    public Material pipeColor2_;
    public Material pipeColor3_;

    public Material buttonColor1_;
    public Material buttonColor2_;
    public Material buttonColor3_;

    public void RandomizeColors()
    {
        pipeColor1_.color = new Color(Random.value, Random.value, Random.value, 1);
        pipeColor2_.color = new Color(Random.value, Random.value, Random.value, 1);
        pipeColor3_.color = new Color(Random.value, Random.value, Random.value, 1);

        buttonColor1_.color = pipeColor1_.color;
        buttonColor2_.color = pipeColor2_.color;
        buttonColor3_.color = pipeColor3_.color;
    }
}
