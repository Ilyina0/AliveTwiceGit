using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed;
    public float waitTime;
    public Transform[] movePos;

    private int index;
    private bool isWaiting = false;

    private void Start()
    {
        index = 1;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, movePos[index].position) > 0.1f)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, movePos[index].position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if(!isWaiting)
                StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        index += 1;
        index %= movePos.Length;
        isWaiting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(typeof(PlayerController), out var player))
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(typeof(PlayerController), out var player))
        {
            player.transform.parent = null;
        }
    }
}
