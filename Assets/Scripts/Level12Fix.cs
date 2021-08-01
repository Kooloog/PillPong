using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level12Fix : MonoBehaviour
{
    public GameObject pillToCheck;
    Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    // Start is called before the first frame update
    public void checkStatus()
    {
        if (pillToCheck.GetComponent<DropEffect>().currentEffect == "ZeroG")
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + .2f);
        }
    }

    public void backToOriginalPosition()
    {
        this.transform.position = originalPosition;
    }
}
