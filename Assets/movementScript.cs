using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    selected_dictionary selectedDictionary;

    // Define the table at a broader scope
    Dictionary<int, GameObject> table;

    // Start is called before the first frame update
    void Start()
    {
        selectedDictionary = FindObjectOfType<selected_dictionary>();
    }

    void Update()
    {
        if (selectedDictionary != null)
        {
            // Now you can access selectedTable using selectedDictionary.selectedTable
            table = selectedDictionary.selectedTable;
        }

        // Check if table is not null before iterating
        if (table != null)
        {
            foreach (KeyValuePair<int, GameObject> pair in table)
            {
                
            }
        }
    }
}
