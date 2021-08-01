using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScript : MonoBehaviour
{
    [SerializeField] public GameObject congratsCanvas;
    [SerializeField] public GameObject creditsCanvas;

    public void changeCanvas()
    {
        congratsCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }
}
