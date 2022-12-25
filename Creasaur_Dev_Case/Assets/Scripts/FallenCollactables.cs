using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenCollactables : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.parent = EventManager.Instance.environment.transform;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    
}
