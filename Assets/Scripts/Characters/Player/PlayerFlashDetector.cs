using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFlashDetector : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask FleshLayer;
    public Collider2D[] flesh = new Collider2D[10];
    public bool isHavePossessableFlesh;
    private ContactFilter2D contactFilter2D;

    private void Awake()
    {
        contactFilter2D = new ContactFilter2D
        {
            useLayerMask = true
        };
        contactFilter2D.SetLayerMask(FleshLayer);
    }

    private void FixedUpdate()
    {
        Array.Clear(flesh,0,10);
        var size = Physics2D.OverlapCircle(transform.position, radius,contactFilter2D, flesh);
        if (size > 0)
        {
            isHavePossessableFlesh = true;
            // foreach (var item in flesh)
            // {
            //     if(item == null) continue;
            //     //TODO 将范围内可附身目标描边
            // }
        }
        else
        {
            isHavePossessableFlesh = false;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
#endif
}
