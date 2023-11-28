using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSelectedScript : MonoBehaviour
{

    void Awake()
    {
        // Initialize the class-level variables instead of creating new local variables.
    }

    public void select()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void deselect()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

}
