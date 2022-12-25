using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelPrefabs;
    private int levelIndex;
    private GameObject currentLevel;

    private void Awake()
    {
        levelIndex = PlayerPrefs.GetInt("level");
        currentLevel = Instantiate(levelPrefabs[levelIndex]);
        currentLevel.transform.SetParent(this.gameObject.transform);
        currentLevel.transform.localPosition = Vector3.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void Start()
    {
        EventManager.LoadNextLevel += LevelFinished;
    }

    private void LevelFinished()
    {
        Destroy(currentLevel);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject,1f);
        }
        if (levelPrefabs.Count-1>levelIndex)
        {
            levelIndex += 1;
        }
        else
        {
            levelIndex = 0;
        }
        PlayerPrefs.SetInt("level",levelIndex);
        LoadLevel();
    }

    private void LoadLevel()
    {
        currentLevel = Instantiate(levelPrefabs[levelIndex]);
        currentLevel.transform.SetParent(this.gameObject.transform);
        currentLevel.transform.localPosition = Vector3.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        EventManager.Instance.oneTime = false;
    }
}
