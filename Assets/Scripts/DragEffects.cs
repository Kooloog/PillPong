using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragEffects : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform followMouse;
    private Vector3 startCoordinates;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        followMouse = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startCoordinates = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!PillCollision.isPlaying)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = .75f;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startCoordinates;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!PillCollision.isPlaying)
        {
            if (!(eventData.pointerDrag.name.ToString() == "Flag" && DropEffect.somethingHasFlag)) 
            {
                followMouse.anchoredPosition += eventData.delta;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }
}
