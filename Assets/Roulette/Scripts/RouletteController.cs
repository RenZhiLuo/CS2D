using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class RouletteController : MonoBehaviour
{
    [SerializeField] private EventTrigger spinEvent;
    [SerializeField] private float triggerRadius;

    [SerializeField] private Transform focusTrans;

    private const int itemCount = 8;
    private float deltaAngle = 360 / itemCount;


    private int itemIndex;
    private void Start()
    {
        //OnDrag
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.Drag;
        entry2.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        spinEvent.triggers.Add(entry2);

        //OnEndDrag
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry3.eventID = EventTriggerType.EndDrag;
        entry3.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        spinEvent.triggers.Add(entry3);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - (Vector2)spinEvent.transform.position;

        if (direction.magnitude > triggerRadius)
        {
            float angle = Vector2.Angle(Vector2.up, direction);
            if (direction.x < 0) angle = 360 - angle;

            itemIndex = (int)(angle / deltaAngle);

            Vector3 focusDirection = new Vector3(0, 0, -((itemIndex + 1) * deltaAngle - deltaAngle / 2));
            focusTrans.rotation = Quaternion.Euler(focusDirection);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

}
