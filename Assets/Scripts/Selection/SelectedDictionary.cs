using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> SelectedTable = new Dictionary<int, GameObject>();
    
    public void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(SelectedTable.ContainsKey(id)))
        {
            SelectedTable.Add(id, go);
            foreach (KeyValuePair<int,GameObject> pair in SelectedTable)
            {
                IsSelectedScript isSelectedScript = SelectedTable[pair.Key].GetComponent<IsSelectedScript>();
                if (isSelectedScript != null)
                {
                    isSelectedScript.select(); // Change the variable for each object

                        
                }
            }
        }
    }

    public void deselect(int id)
    {
        IsSelectedScript isSelectedScript = SelectedTable[id].GetComponent<IsSelectedScript>();
        if (isSelectedScript != null)
        {
            isSelectedScript.deselect(); // Change the variable for each object
        }
        SelectedTable.Remove(id);
    }

    public void deselectAll()
    {
        foreach(KeyValuePair<int,GameObject> pair in SelectedTable)
        {
            if(pair.Value != null)
            {
                IsSelectedScript isSelectedScript = SelectedTable[pair.Key].GetComponent<IsSelectedScript>();
                if (isSelectedScript != null)
                {
                    isSelectedScript.deselect(); // Change the variable for each object
                }
            }
            
        }
        SelectedTable.Clear();
        
    }
    public bool isKeyWithin(int id)
    {
        return SelectedTable.ContainsKey(id);
    }
    public int numOfEntries()
    {
        return SelectedTable.Count;
    }
}