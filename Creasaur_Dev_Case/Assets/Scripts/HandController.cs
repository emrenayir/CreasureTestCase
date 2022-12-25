using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private DynamicJoystick dynamicJoystick;
    [SerializeField] private float movementSpeed;

    [SerializeField] private float leftEdge;
    [SerializeField] private float rightEdge;

    private Animator animator;
    private bool work;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.GameStart += CanMove;
        EventManager.GameEnd += CantMove;
        EventManager.LoadNextLevel += ResetHand;
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        if (work)
        {
            Move();
        }
    }

    private void Move()
    {
        if (transform.position.x>leftEdge && transform.position.x< rightEdge )
        {
            transform.position += new Vector3(dynamicJoystick.Horizontal,0,0) * (movementSpeed * Time.deltaTime);
        }

        if (transform.position.x < leftEdge && dynamicJoystick.Horizontal>0 )
        {
            transform.position += new Vector3(dynamicJoystick.Horizontal,0,0) * (movementSpeed * Time.deltaTime);
        } 
        if (transform.position.x > rightEdge && dynamicJoystick.Horizontal<0 )
        {
            transform.position += new Vector3(dynamicJoystick.Horizontal,0,0) * (movementSpeed * Time.deltaTime);
        }
    }

    private void CanMove()
    {
        work = true;
    }
    private void CantMove()
    {
        transform.DOMove(Vector3.zero + new Vector3(0,7,0), 0.5f);
        animator.SetBool("finish",true);
        work = false;
    }

    private void ResetHand()
    {
        animator.SetBool("finish",false);
        transform.position = Vector3.zero;
    }
}
