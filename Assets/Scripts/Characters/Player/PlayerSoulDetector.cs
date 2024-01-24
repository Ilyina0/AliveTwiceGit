using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSoulDetector : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask SoulLayer;
    public Collider2D[] souls = new Collider2D[10];
    public bool isHavePossessableSoul;
    private ContactFilter2D contactFilter2D;
    private void FixedUpdate()
    {
        Array.Clear(souls,0,10);
        var size = Physics2D.OverlapCircle(transform.position, radius, contactFilter2D,souls);
        if (size > 0)
        {
            isHavePossessableSoul = true;
            // foreach (var item in flesh)
            // {
            //     if(item == null) continue;
            //     //TODO 将范围内可附身目标描边
            // }
        }
        else
        {
            isHavePossessableSoul = false;
        }
    }

    private void Awake()
    {
        contactFilter2D = new ContactFilter2D()
        {
            useLayerMask = true
        };
        contactFilter2D.SetLayerMask(SoulLayer);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
#endif
}
