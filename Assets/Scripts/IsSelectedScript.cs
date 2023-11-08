using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSelectedScript : MonoBehaviour
{
    public bool isSelected = false;
    private RightClickScript rightClickScript;

    void Awake()
    {
        // Initialize the class-level variables instead of creating new local variables.
        rightClickScript = gameObject.GetComponent<RightClickScript>();
    }

    public void select()
    {
        rightClickScript.isSelected = true;
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void deselect()
    {
        rightClickScript.isSelected = false;
        GetComponent<Renderer>().material.color = Color.white;
    }
}
