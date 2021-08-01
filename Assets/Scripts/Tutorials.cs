using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorials : MonoBehaviour, IPointerClickHandler
{
    public GameObject menusToActivate;
    public GameObject tutorialButton;
    public GameObject[] listOfTutorials;

    public static bool hasTutorials;

    int currentTutorialPage;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Tutorials") != null)
        {
            enableTutorials();
            hasTutorials = true;
        }
        else
        {
            hasTutorials = false;
            menusToActivate.SetActive(true);
        }
    }

    public void enableTutorials()
    {
        tutorialButton.SetActive(false);
        currentTutorialPage = 0;
        menusToActivate.SetActive(false);
        listOfTutorials[currentTutorialPage].SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("*click*");
        listOfTutorials[currentTutorialPage].SetActive(false);
        currentTutorialPage++;

        if(currentTutorialPage >= listOfTutorials.Length)
        {
            menusToActivate.SetActive(true);
            tutorialButton.SetActive(true);
        }
        else
        {
            listOfTutorials[currentTutorialPage].SetActive(true);
        }
    }
}
