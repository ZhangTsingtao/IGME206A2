using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogDisplay : MonoBehaviour
{
    public TMP_Text dialogText;
    public Dialog playerDialog;
    public int dialogEntry = 0;

    private void Start()
    {
        dialogText.text = playerDialog.dialogs[0];
    }
    public void NextDialogLine()
    {
        dialogEntry++;
        if (dialogEntry >= playerDialog.dialogs.Count)
        {
            Debug.Log("That's all dialogs");
            dialogEntry = playerDialog.dialogs.Count - 1;
        }
        dialogText.text = playerDialog.dialogs[dialogEntry];
    }
    public void LastDialogLine()
    {
        dialogEntry--;
        if (dialogEntry <= 0)
        {
            Debug.Log("That's initial line");
            dialogEntry = 0;
        }
        dialogText.text = playerDialog.dialogs[dialogEntry]; 
    }
    
}
