using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAllMenuBuildings : MonoBehaviour
{
    private RoadScript roadScript;

    // Start is called before the first frame update
    void Awake()
    {
        roadScript = GetComponent<RoadScript>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DeactivateAll();
        }
    }

    public void DeactivateAll()
    {
        roadScript.roadBeingBuilt = false;
    }
}
