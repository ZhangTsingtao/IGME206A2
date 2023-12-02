using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogDisplay : MonoBehaviour
{
    public Dialog dialog;
    public int dialogEntry = 0;

    public (bool,string) NextDialogLine()
    {
        int index = dialogEntry;
 
        dialogEntry++;
           
        if(dialogEntry >= dialog.dialogs.Count)
        {
            Debug.Log("That's all dialogs");
            dialogEntry = dialog.dialogs.Count - 1;
            return (true, dialog.dialogs[dialogEntry]);
        }
        return (false, dialog.dialogs[index]);
    }    
}
