using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogDisplay : MonoBehaviour
{
    public TMP_Text dialogText;
    public Dialog dialog;
    public int dialogEntry = 0;

    private void Start()
    {
        dialogText.text = "";
    }
    public bool NextDialogLine()
    {
        dialogText.text = dialog.dialogs[dialogEntry];
        dialogEntry++;
           
        if(dialogEntry >= dialog.dialogs.Count)
        {
            Debug.Log("That's all dialogs");
            dialogEntry = dialog.dialogs.Count - 1;
            return true;
        }
        return false;
    }
    public void LastDialogLine()
    {
        dialogText.text = dialog.dialogs[dialogEntry];
        dialogEntry--;
        if (dialogEntry <= 0)
        {
            Debug.Log("That's initial line");
            dialogEntry = 0;
        }
        
    }
    
}
