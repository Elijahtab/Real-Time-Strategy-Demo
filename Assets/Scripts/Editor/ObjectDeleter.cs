using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectGenerator))]
public class ObjectDeleter : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectGenerator objectGenerator = (ObjectGenerator)target;

        // Add a button to the inspector
        if (GUILayout.Button("Click Me"))
        {
            // Call the method you want to execute when the button is clicked
            objectGenerator.DeleteAll();
        }
    }
}
