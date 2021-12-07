using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ProJoystick : WoXing.Joystick
{

    private Image bgImg;
    private Color onColor;
    private Color offColor;
    protected override void Start()
    {
        base.Start();
        bgImg = background.GetComponent<Image>();
        offColor = bgImg.color;
        onColor = new Color(offColor.r, offColor.g, offColor.b, 255);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        bgImg.color = onColor;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        bgImg.color = offColor;
    }
}
