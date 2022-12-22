using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private DynamicJoystick dynamicJoystick;
    [SerializeField] private float movementSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dynamicJoystick.Horizontal != 0)
        {
            Move();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        this.gameObject.transform.position += new Vector3(dynamicJoystick.Horizontal,0,0) * (movementSpeed * Time.deltaTime);
    }
}
