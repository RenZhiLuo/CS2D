using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoldButton : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public bool IsHoldDown { get { return isHoldDown; } }
    private bool isHoldDown;
    public void OnPointerDown(PointerEventData eventData)
    {
        isHoldDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHoldDown = false;
    }

}
