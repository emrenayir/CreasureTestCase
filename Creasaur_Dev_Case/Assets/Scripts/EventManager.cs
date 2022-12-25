using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public static event Action CollectRed;
    public static event Action CollectBlue;
    public static event Action CollectYellow;
    public static event Action CollectGreen;

    public static event Action GameStart;
    public static event Action GameEnd;
    public static event Action LoadNextLevel;
    
    
    public static event Action Door;
    
    public static event Action Trap;


    public bool collectRed;
    public bool collectBlue;
    public bool collectYellow;
    public bool collectGreen;

    public bool trap;
    public bool gameEnd;
    public bool loadNextLevel;
    public bool door;
    
    
    
    //yanlış işler dskladsa
    public bool oneTime;
    public GameObject environment;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Update()
    {
        if (collectRed)
        {
            CollectRed?.Invoke(); 
            collectRed = false;
        }
        if (collectBlue)
        {
            CollectBlue?.Invoke(); 
            collectBlue = false;
        }
        if (collectGreen)
        {
            CollectGreen?.Invoke(); 
            collectGreen = false;
        }if (collectYellow)
        {
            CollectYellow?.Invoke(); 
            collectYellow = false;
        }

        if (Input.GetMouseButton(0) && !oneTime)
        {
            GameStart?.Invoke();
            oneTime = true;
        }

        if (door)
        {
            Door?.Invoke();
            door = false;
        }

        if (trap)
        {
            Trap?.Invoke();
            trap = false;
        }

        if (gameEnd)
        {
            GameEnd?.Invoke();
            gameEnd = false;
        }

        if (loadNextLevel)
        {
            LoadNextLevel?.Invoke();
            loadNextLevel = false;
        }
    }
    
    
}
