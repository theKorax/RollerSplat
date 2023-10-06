using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPiece : MonoBehaviour
{
   public bool isColoured = false;
    public void ChangeColour(Color colour)
    {
        GetComponent<MeshRenderer>().material.color = colour;
        isColoured = true;
        GameManager.singleton.CheckComplete();
    }
}
