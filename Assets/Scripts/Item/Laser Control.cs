using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float colorIntensity = 4.3f;
    private float beamColorEnhance;

    [SerializeField] private float maxLength = 100;
    [SerializeField] private float thickness = 9;
    [SerializeField] private float noiceScale = 3.14f;
    [SerializeField] private GameObject StartVFX;
    [SerializeField] private GameObject EndVFX;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _lineRenderer.material.color = color * colorIntensity;
        _lineRenderer.material.SetFloat("_LaserThickness",thickness);
        _lineRenderer.material.SetFloat("_LaserScale",noiceScale);

        ParticleSystem[] particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
        foreach (var p in particleSystems)
        {
            Renderer r = p.GetComponent<Renderer>();
            r.material.SetColor("_EmissionColor",color * (colorIntensity + beamColorEnhance));
        }
    }

    private void Start()
    {
        UpdateEndPosition();
    }

    private void Update()
    {
        UpdateEndPosition();
    }

    public void UpdatePosition(Vector2 startPosition, Vector2 direction)
    {
        direction = direction.normalized;
        transform.position = startPosition;
        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0,0,rotationZ * Mathf.Rad2Deg);
    }
    
    //更新激光照射点的位置
    private void UpdateEndPosition()
    {
        Vector2 startPosition = transform.position;
        float rotationZ = transform.rotation.eulerAngles.z;//degree
        rotationZ *= Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(rotationZ), Mathf.Sin(rotationZ));

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction.normalized);

        float length = maxLength;
        float laserEndRotation = 180;
        if (hit)
        {
            length = (hit.point - startPosition).magnitude;
            laserEndRotation = Vector2.Angle(direction, hit.normal);
        }
        
        _lineRenderer.SetPosition(1,new Vector2(length,0));

        Vector2 endPosition = startPosition + length * direction;
        StartVFX.transform.position = startPosition;
        EndVFX.transform.position = endPosition;
        EndVFX.transform.rotation = Quaternion.Euler(0, 0, laserEndRotation);
    }
}
