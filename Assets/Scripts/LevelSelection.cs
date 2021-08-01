using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    int thisLevel;
    bool selectable;

    void Start()
    {
        thisLevel = int.Parse(GetComponent<Image>().sprite.name);
        Debug.Log(PlayerPrefs.GetInt("Levels"));

        if (PlayerPrefs.GetInt("Levels") >= thisLevel)
        {
            selectable = true;
        }
        else
        {
            selectable = false;
            GetComponent<Image>().color = Color.gray;
        }
    }

    public void GoToThisLevel()
    {
        if(selectable)
        {
            SceneManager.LoadScene(thisLevel);
        }
    }
}
