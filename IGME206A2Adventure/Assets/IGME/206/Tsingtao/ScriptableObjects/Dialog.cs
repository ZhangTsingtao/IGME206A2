using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "ScriptableObjects/Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    public List<string> names = new List<string>();
    public List<string> rawDialogs = new List<string>();
    public List<string> dialogs = new List<string>();
    private void OnEnable()
    {
        dialogs.Clear();
        if (names.Count == 0) return;
        for (int i = 0; i < rawDialogs.Count; i++) 
        {
            dialogs.Add(names[i % 2] + ": " + System.Environment.NewLine + rawDialogs[i]);
        }
    }
}