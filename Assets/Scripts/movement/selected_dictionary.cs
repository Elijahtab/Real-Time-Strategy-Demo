using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selected_dictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedTable.ContainsKey(id)))
        {
            selectedTable.Add(id, go);
            go.AddComponent<selection_component>();
            foreach (KeyValuePair<int,GameObject> pair in selectedTable)
            {
                MovementScript movementScript = selectedTable[pair.Key].GetComponent<MovementScript>();
                if (movementScript != null)
                {
                    movementScript.isSelected = true; // Change the variable for each object
                }
            }
        }
    }

    public void deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<selection_component>());
        MovementScript movementScript = selectedTable[id].GetComponent<MovementScript>();
        if (movementScript != null)
        {
            movementScript.isSelected = false; // Change the variable for each object
        }
        selectedTable.Remove(id);
    }

    public void deselectAll()
    {
        foreach(KeyValuePair<int,GameObject> pair in selectedTable)
        {
            if(pair.Value != null)
            {
                Destroy(selectedTable[pair.Key].GetComponent<selection_component>());
                MovementScript movementScript = selectedTable[pair.Key].GetComponent<MovementScript>();
                if (movementScript != null)
                {
                    movementScript.isSelected = false; // Change the variable for each object
                }
            }
            
        }
        selectedTable.Clear();
        
    }
    public bool isKeyWithin(int id)
    {
        return selectedTable.ContainsKey(id);
    }
    public int numOfEntries()
    {
        return selectedTable.Count;
    }
}