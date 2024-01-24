using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] private Vector3 RotateSpeed;

    private void Update()
    {
        transform.Rotate(RotateSpeed * Time.deltaTime);
    }
}
