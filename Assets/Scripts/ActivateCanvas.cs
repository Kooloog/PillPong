using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCanvas : MonoBehaviour
{
    [SerializeField]
    public GameObject activate;

    // Start is called before the first frame update
    void Start()
    {
        activate.SetActive(true);
    }
}
