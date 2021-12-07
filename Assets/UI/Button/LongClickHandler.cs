using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class LongClickHandler : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    [SerializeField] private Image fillImg;

    [SerializeField] private float triggerTime;
    [SerializeField] private UnityEvent handler;


    private Coroutine prog;
    public void OnPointerDown(PointerEventData eventData)
    {
        prog = StartCoroutine(StartLoad());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(prog);
        fillImg.fillAmount = 0;
    }

    private IEnumerator StartLoad()
    {
        float timer = 0;
        do
        {
            timer += Time.deltaTime;
            fillImg.fillAmount = timer / triggerTime;
            yield return new WaitForEndOfFrame();
        } while (timer < triggerTime);
        handler?.Invoke();
        fillImg.fillAmount = 0;
    }

}
