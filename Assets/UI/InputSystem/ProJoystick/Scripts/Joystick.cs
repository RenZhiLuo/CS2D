using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WoXing
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {

        public float Horizontal { get { return input.x; }  }
        public float Vertical { get { return input.y; }  }

        [SerializeField] protected RectTransform background;
        [SerializeField] protected RectTransform handle;

        [SerializeField] private float clampRadiusScale = 0.7f;

        private float radius;
        private Vector2 input = Vector2.zero;

        protected virtual void Start()
        {
            radius = background.sizeDelta.x / 2 * clampRadiusScale;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out position);
            position = Vector2.ClampMagnitude(position, radius);
  
            handle.anchoredPosition = position;
            input = position / radius;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            input = Vector2.zero;
            handle.anchoredPosition = input;
        }
    }
}

