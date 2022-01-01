using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float radius = 3;
    [SerializeField] private float distance = 4;

    [SerializeField] private LayerMask layer;

    [SerializeField] private PlayerWeapon playerWeapon;

    private void Start()
    {
        InputSystem.instance.pickUpButton.onClick.AddListener(CheckPickUpByOverlap);
    }
    private void OnDestroy()
    {
        InputSystem.instance.pickUpButton.onClick.RemoveListener(CheckPickUpByOverlap);
    }
  
    private void CheckPickUpByOverlap()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        if (colls.Length > 0)
        {
            
            if(colls[0].CompareTag("Gun") && colls[0].TryGetComponent<Gun>(out Gun gun))
            {
                Debug.Log("Pick up gun");
                playerWeapon.PickUpWeapon(gun);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
