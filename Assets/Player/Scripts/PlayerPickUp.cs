using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float radius = 3;
    [SerializeField] private float distance = 4;

    [SerializeField] private LayerMask layer;

    [SerializeField] private UnityEvent<Collider2D> PickUpHandler;

    private void Start()
    {
        InputSystem.instance.pickUpButton.onClick.AddListener(CheckPickUpByOverlap);
    }
    private void OnDestroy()
    {
        InputSystem.instance.pickUpButton.onClick.RemoveListener(CheckPickUpByOverlap);
    }
    private void CheckPickUpByRaycast()
    {
        if (Physics.SphereCast(Camera.main.transform.position, radius, Camera.main.transform.forward, out RaycastHit hit, distance, layer))
        {
            Debug.Log("CheckPickUpByRaycast");
        }
    }
    private void CheckPickUpByOverlap()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        if (colls.Length > 0)
        {
            Debug.Log("CheckPickUpByOverlap");
            PickUpHandler?.Invoke(colls[0]);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
