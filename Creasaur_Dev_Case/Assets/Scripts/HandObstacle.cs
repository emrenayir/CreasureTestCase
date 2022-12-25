using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandObstacle : MonoBehaviour
{
    [SerializeField] private float rightEnd;
    [SerializeField] private float leftEnd;
    
    public float speed = 1.0f;
    private bool movingForward = true;

    private bool work;

    private void Start()
    {
        EventManager.GameStart += StartMove;
        EventManager.GameEnd += StopMove;
    }

    void Update()
    {
        if (work)
        {
            if (movingForward)
            {
                transform.position += Vector3.right * (Time.deltaTime * speed);
                if (transform.position.x >= rightEnd)
                {
                    movingForward = false;
                }
            }
            else
            {
                transform.position -= Vector3.right * (Time.deltaTime * speed);
                if (transform.position.x <= leftEnd)
                {
                    movingForward = true;
                }
            }
        }
       
    }

    private void StartMove()
    {
        work = true;
    }
    private void StopMove()
    {
        work = false;
    }
}
