using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    private Tween tween;
    private void Start()
    {
        if (transform!= null)
        {
            tween = transform.DOLocalMove(transform.position + new Vector3(0, 3, 0), .5f).SetLoops(-1, LoopType.Yoyo);
        }
        
    }

    private void OnDestroy()
    {
        tween.Kill();
    }
}
