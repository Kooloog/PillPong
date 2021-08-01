using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropEffect : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public string currentEffect;
    public AudioSource placeEffectSound;
    public AudioSource removeEffectSound;
    public GameObject canPlaceFlag;

    public static bool somethingHasFlag;

    GameObject placeSprite;

    void Start()
    {
        somethingHasFlag = false;
        currentEffect = null;
        placeSprite = this.transform.GetChild(0).gameObject;

        //Volume of sound effects
        placeEffectSound.volume = PlayerPrefs.GetFloat("Sounds");
        removeEffectSound.volume = PlayerPrefs.GetFloat("Sounds");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && currentEffect != eventData.pointerDrag.name.ToString())
        {
            if (currentEffect == "Flag" && somethingHasFlag)
            {
                somethingHasFlag = false;
                canPlaceFlag.SetActive(false);
            }

            this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            placeEffectSound.Play();

            Debug.Log(eventData.pointerDrag.name);
            GameObject effect = GameObject.Find(eventData.pointerDrag.name);

            placeSprite.GetComponent<SpriteRenderer>().sprite = effect.transform.GetComponent<Image>().sprite;
            placeSprite.transform.localScale = new Vector3(3f, 3f, 3f);

            currentEffect = eventData.pointerDrag.name.ToString();
            if(currentEffect == "Flag")
            {
                somethingHasFlag = true;
                canPlaceFlag.SetActive(true);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (placeSprite.transform.localScale == new Vector3(3f, 3f, 3f) && !PillCollision.isPlaying)
        {
            if(currentEffect == "Flag" && somethingHasFlag)
            {
                somethingHasFlag = false;
                canPlaceFlag.SetActive(false);
            }
            
            currentEffect = null;
            removeEffectSound.Play();
            placeSprite.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
}
