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
            foreach (KeyValuePair<int,GameObject> pair in selectedTable)
            {
                IsSelectedScript isSelectedScript = selectedTable[pair.Key].GetComponent<IsSelectedScript>();
                if (isSelectedScript != null)
                {
                    isSelectedScript.select(); // Change the variable for each object
                }
            }
        }
    }

    public void deselect(int id)
    {
        IsSelectedScript isSelectedScript = selectedTable[id].GetComponent<IsSelectedScript>();
        if (isSelectedScript != null)
        {
            isSelectedScript.deselect(); // Change the variable for each object
        }
        selectedTable.Remove(id);
    }

    public void deselectAll()
    {
        foreach(KeyValuePair<int,GameObject> pair in selectedTable)
        {
            if(pair.Value != null)
            {
                IsSelectedScript isSelectedScript = selectedTable[pair.Key].GetComponent<IsSelectedScript>();
                if (isSelectedScript != null)
                {
                    isSelectedScript.deselect(); // Change the variable for each object
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