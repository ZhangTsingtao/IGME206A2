using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTheGame : MonoBehaviour
{
    public UIController controller;
    private void OnDisable()
    {
        controller.ShowWINUI();
    }
}
