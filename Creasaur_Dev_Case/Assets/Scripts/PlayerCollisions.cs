using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable Red"))
        {
            EventManager.Instance.collectRed = true;
            other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (other.gameObject.CompareTag("Collectable Blue"))
        {
            EventManager.Instance.collectBlue = true;
            other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (other.gameObject.CompareTag("Collectable Green"))
        {
            EventManager.Instance.collectGreen = true;
            other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (other.gameObject.CompareTag("Collectable Yellow"))
        {
            EventManager.Instance.collectYellow = true;
            other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("Door"))
        {
            EventManager.Instance.door = true;
            Destroy(other.gameObject.GetComponent<BoxCollider>());
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            EventManager.Instance.trap = true;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            EventManager.Instance.gameEnd = true;
            Invoke(nameof(LoadNextLevel),5f);
            Destroy(other.gameObject.GetComponent<BoxCollider>());
        }
    }

    private void LoadNextLevel()
    {
        EventManager.Instance.loadNextLevel = true;
    }
}
