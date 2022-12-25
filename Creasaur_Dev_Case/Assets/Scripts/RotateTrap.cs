using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateTrap : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Ease _ease;
        
    private bool repeat = true;

    private bool work;

    private void Start()
    {
        EventManager.GameStart += StartMove;
        EventManager.GameEnd += StopMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (repeat && work)
        {
            transform.DOLocalRotate(rotation, speed,RotateMode.LocalAxisAdd).SetEase(_ease).OnComplete((() =>
            {
                repeat = true;
            }));
            repeat = false;
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
