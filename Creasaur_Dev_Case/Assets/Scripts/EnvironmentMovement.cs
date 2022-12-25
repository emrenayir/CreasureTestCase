using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private bool work;
    private void Start()
    {
        EventManager.GameStart += StartMoving;
        EventManager.Trap += RoadBack;
        EventManager.GameEnd += StopMoving;
    }

    private void FixedUpdate()
    {
        if (work)
        {
            this.transform.position -= new Vector3(0, 0, 1)* (Time.deltaTime * speed);
        }
        
    }

    private void StartMoving()
    {
        work = true;
    }
    private void StopMoving()
    {
        work = false;
    }
    private void RoadBack()
    {
        work = false; 
        transform.DOMove(transform.position + new Vector3(0, 0, 20), 0.5f).OnComplete((() =>
        {
            work = true;
        }));
    }
    
}
