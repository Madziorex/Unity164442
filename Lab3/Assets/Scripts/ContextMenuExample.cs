using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ContextMenuExample : MonoBehaviour
{
    // Method that can be triggered from the context menu in the inspector
    [ContextMenu("Reset Values")]
    void ResetValues()
    {
        Debug.Log("Values have been reset.");
    }

    // Public field with a context menu item that triggers a method
    [ContextMenuItem("Log Value", "LogFieldValue")]
    public int valueWithContextMenuItem = 20;

    void LogFieldValue()
    {
        Debug.Log("Value With Context Menu Item: " + valueWithContextMenuItem);
    }
}

[InitializeOnLoad]
public class InitializeOnLoadExample
{
    static InitializeOnLoadExample()
    {
        Debug.Log("InitializeOnLoadExample has been initialized when Unity started.");
    }
}
