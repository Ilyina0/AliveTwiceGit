using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{

   [SerializeField] private float radius;
   [SerializeField] private LayerMask groundLayer;
   private readonly Collider2D[] _colliders = new Collider2D[1];
   private ContactFilter2D _contactFilter2D;

   public bool IsGround =>
       //NonAlloc 指在运行中不会分配内存，则不会触垃圾回收机制，优化性能
       Physics2D.OverlapCircle(transform.position, radius, _contactFilter2D, _colliders) != 0;

   public void Awake()
   {
       _contactFilter2D.useLayerMask = true;
       _contactFilter2D.SetLayerMask(groundLayer);

   }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
#endif
}
