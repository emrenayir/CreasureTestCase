using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private Transform startCanvas;
    // Start is called before the first frame update
    void Start()
    {
        startCanvas = transform.GetChild(0);
        EventManager.LoadNextLevel += OpenStartCanvas;
        EventManager.GameStart += CloseStartCanvas;
    }

    private void OpenStartCanvas()
    {
        startCanvas.gameObject.SetActive(true);
    }
private void CloseStartCanvas()
    {
        startCanvas.gameObject.SetActive(false);
    }

    
}
