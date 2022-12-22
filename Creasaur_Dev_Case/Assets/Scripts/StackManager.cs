using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ModestTree;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> diamondList;

    [SerializeField] private float diamondAnimationTime;
    [SerializeField] private Ease diamondEaseType;

    [SerializeField] private GameObject diamondPrefabRed;
    [SerializeField] private GameObject diamondPrefabYellow;
    [SerializeField] private GameObject diamondPrefabBlue;
    [SerializeField] private GameObject diamondPrefabGreen;


    [SerializeField] private List<string> diamondColors;
    private int activeIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var diamonds in diamondList)
        {
            foreach (Transform child in diamonds.transform)
            {
                child.gameObject.transform.localScale = Vector3.zero;
            }
        }

        activeIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CollectDiamond("Red");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            CollectDiamond("Blue");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            CollectDiamond("Yellow");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            CollectDiamond("Green");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*List<int> temp = new List<int>();
            temp.Add(3);
            List<string> tempColor = new List<string>();
            tempColor.Add("red");
            MergeDiamonds(1,temp,tempColor);*/
            List<int> temp = new List<int>();
            List<string> temp1 = new List<string>();
            temp.Add(1);
            temp.Add(2);
            temp1.Add("Red");
            temp1.Add("Red");
            MergeDiamonds(0,temp,temp1);
            DiamondUpgradeAnimation(0,temp.Capacity);
        }
    }

    private void CollectDiamond(string color)
    {
        if (activeIndex < diamondList.Capacity)
        {
            foreach (Transform child in diamondList[activeIndex].transform)
            {
                if (child.gameObject.name == color)
                {
                    child.gameObject.transform.DOScale(new Vector3(2, 2, 2),diamondAnimationTime).SetEase(diamondEaseType);
                    diamondColors.Add(color);
                    activeIndex += 1;
                }
                else
                {
                    child.gameObject.transform.localScale = Vector3.zero;
                }
            }
           
        }
    }

    // If ı use other words than color names it will set diamond closed
    private void SetDiamond(string color,int index)
    {
        foreach (Transform child in diamondList[index].transform)
        {
            if (child.gameObject.name == color)
            {
                child.gameObject.transform.DOScale(new Vector3(2, 2, 2),diamondAnimationTime).SetEase(diamondEaseType);
            }
            else
            {
                child.gameObject.transform.localScale = Vector3.zero;
            }
        }
    }

    /*private void CalculateMergedList()
    {
        for (int i = 0; i < calculatorArray.Capacity; i++)
        {
            var temp = calculatorArray[i];
            var startPoint = i;
            if (calculatorArray[i+1] == )
            {
                
            }
        }
    }*/

    private void MergeDiamonds(int toMerge, List<int> mergedIndex, List<string>mergedColor)
    {
        mergedIndex.Sort();
        for (int i = 0; i < mergedIndex.Count; i++)
        {
            if (mergedColor[i] == "Red")
            {
                SetDiamond("clear",mergedIndex[i]); //close the real one
                var prefab = Instantiate(diamondPrefabRed, diamondList[mergedIndex[i]].transform);
                prefab.transform.localPosition = Vector3.zero;
                prefab.transform.DOMove(diamondList[toMerge].transform.position, 1f).OnComplete((() =>
                {
                    Destroy(prefab);
                    
                    
                }));
            }else if (mergedColor[i] == "Blue")
            {
                SetDiamond("clear",mergedIndex[i]); //close the real one
                var prefab = Instantiate(diamondPrefabBlue, diamondList[mergedIndex[i]].transform);
                prefab.transform.localPosition = Vector3.zero;
                prefab.transform.DOMove(diamondList[toMerge].transform.position, 1f).OnComplete((() =>
                {
                    Destroy(prefab);
                }));
            }else if (mergedColor[i] == "Green")
            {
                SetDiamond("clear",mergedIndex[i]); //close the real one
                var prefab = Instantiate(diamondPrefabGreen, diamondList[mergedIndex[i]].transform);
                prefab.transform.localPosition = Vector3.zero;
                prefab.transform.DOMove(diamondList[toMerge].transform.position, 1f).OnComplete((() =>
                {
                    Destroy(prefab);
                }));
            }
            else
            {
                SetDiamond("clear",mergedIndex[i]); //close the real one
                var prefab = Instantiate(diamondPrefabYellow, diamondList[mergedIndex[i]].transform);
                prefab.transform.localPosition = Vector3.zero;
                prefab.transform.DOMove(diamondList[toMerge].transform.position, 1f).OnComplete((() =>
                {
                    Destroy(prefab);
                }));
            }
        }

        FillEmptySlots(mergedIndex.Count,3/*mergedIndex[mergedIndex.Count]*/);
    }

    private void DiamondUpgradeAnimation(int index,int repeatCount)
    {
        diamondList[index].transform.DOScale(new Vector3(2.8f, 1.4f, 2.8f), 0.2f).SetEase(Ease.InOutSine).OnComplete((() =>
        {
            diamondList[index].transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.2f);
        })).SetLoops(repeatCount);
    }

    private void FillEmptySlots(int stepCount,int startIndex)
    {
        for (int i = startIndex-stepCount; i < startIndex+1; i++)
        {
            SetDiamond(diamondColors[i+stepCount],i);
        }

        for (int i = activeIndex-stepCount; i < activeIndex+1; i++)
        {
            SetDiamond("clear",i);
        }

        activeIndex -= stepCount;
    }
}
