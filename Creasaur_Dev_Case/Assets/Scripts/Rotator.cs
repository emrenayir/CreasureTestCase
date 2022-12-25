using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 rotateVector;
    

    private bool work;


    private void Start()
    {
        EventManager.GameStart += StartMove;
        EventManager.GameEnd += StopMove;
    }

    private void Update()
    {
        if (work)
        {
            transform.Rotate(rotateVector * (speed * Time.deltaTime));
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
