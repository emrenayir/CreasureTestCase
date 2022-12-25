using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ModestTree;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class StackManager : MonoBehaviour
    {
        //Stack object list
        [SerializeField] private List<GameObject> diamondList;

    
        //Merge animation properties
        [SerializeField] private float diamondAnimationTime;
        [SerializeField] private Ease diamondEaseType;
    
        //Special prefabs for effects
        [SerializeField] private GameObject diamondPrefabRed;
        [SerializeField] private GameObject diamondPrefabYellow;
        [SerializeField] private GameObject diamondPrefabBlue;
        [SerializeField] private GameObject diamondPrefabGreen;


        //record stack color properties
        [SerializeField] private List<string> diamondColors;
        [SerializeField] private GameObject hand;

        
        //Point to last element in stack
        public int lastIndex;


        private bool gateStart;
        // Start is called before the first frame update
        void Start()
        {
            EventManager.CollectRed += CollectDiamondRed;
            EventManager.CollectBlue += CollectDiamondBlue;
            EventManager.CollectYellow += CollectDiamondYellow;
            EventManager.CollectGreen += CollectDiamondGreen;
            EventManager.Door += MergeDiamondsInList;
            EventManager.Trap += TakeHit;
            EventManager.LoadNextLevel += ResetStack;
            EventManager.LoadNextLevel += FullResetStack;
            ResetStack();
        }

        private void FullResetStack()
        {
            diamondColors.Clear();
        }
        
        private void CollectDiamondRed()
        {
            CollectDiamond("Red");
        }
        private void CollectDiamondBlue()
        {
            CollectDiamond("Blue");
        }
        private void CollectDiamondYellow()
        {
            CollectDiamond("Yellow");
        }
        private void CollectDiamondGreen()
        {
            CollectDiamond("Green");
        }
        
        private void TakeHit()
        {
            List<string> newList = new List<string>();

            for (int i = 0; i < diamondColors.Count/2; i++)
            {
                newList.Add(diamondColors[i]);
            }
            for (int i = diamondColors.Count/2; i < diamondColors.Count ; i++)
            {
                var obj = DiamondInstantiate(diamondColors[i]);
                obj.transform.localPosition = diamondList[i].transform.position;
                obj.transform.localScale = new Vector3(2, 2, 2);
                obj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1,1),2f,20f),ForceMode.Impulse);
            }
            

            diamondColors = newList;
            ApplyList();
        }

        private GameObject DiamondInstantiate(string color)
        {
            if (color == "Red")
            {
                var obj = Instantiate(diamondPrefabRed);
                return obj;
            }else if (color == "Blue")
            {
                var obj = Instantiate(diamondPrefabBlue);
                return obj;
            }else if (color == "Green")
            {
                var obj = Instantiate(diamondPrefabGreen);
                return obj;
            }
            else
            {
                var obj = Instantiate(diamondPrefabYellow);
                return obj;
            }
        }
        private void MergeDiamondsInList()
        {
            if (diamondColors.Count != 0)
            {
                List<string> newList = new List<string>();

                newList.Add(diamondColors[0]);
                for (int i = 1; i < diamondColors.Count; i++)
                {
                    if (diamondColors[i] != diamondColors[i-1])
                    {
                        newList.Add(diamondColors[i]);
                    }
                }

                diamondColors = newList;
                ApplyList();
            }
        }

        private void ApplyList()
        {
            ResetStack();
            for (int i = 0; i < diamondColors.Count; i++)
            {
                SetDiamondColor(diamondColors[i],i);
            }

            lastIndex = diamondColors.Count;
        }


        // Set inactive all diamonds in the stack and sets active index to 0 
        private void ResetStack()
        {
            foreach (var diamonds in diamondList)
            {
                foreach (Transform child in diamonds.transform)
                {
                    child.gameObject.transform.localScale = Vector3.zero;
                }
            }
            lastIndex = 0;
        }
        
        //Active a diamond in the last index with a color property
        private void CollectDiamond(string color)
        {
            if (lastIndex < diamondList.Capacity)
            {
                foreach (Transform child in diamondList[lastIndex].transform)
                {
                    if (child.gameObject.name == color)
                    {
                        child.gameObject.transform.DOScale(new Vector3(2, 2, 2),diamondAnimationTime).SetEase(diamondEaseType);
                        //update color list 
                        diamondColors.Add(color);
                        //update last index
                        lastIndex += 1;
                    }
                    else
                    {
                        child.gameObject.transform.localScale = Vector3.zero;
                    }
                }
           
            }
        }

        private void SetDiamondColor(string color,int index)
        {
            foreach (Transform child in diamondList[index].transform)
            {
                if (child.gameObject.name == color)
                {
                    child.gameObject.transform.localScale = new Vector3(2,2,2);
                    //update color list
                    diamondColors[index] = color;
                }
                else
                {
                    child.gameObject.transform.localScale = Vector3.zero;
                }
            }
        }

        private void RemoveDiamond(int index)
        {
            foreach (Transform child in diamondList[index].transform)
            {
                child.transform.localScale = Vector3.zero;
            }
                
        }

        /*private void CalculateMergedList()
        {
            int repeatCount = 0;
            string color;
            List<int> mergedIndex = new List<int>();
            for (int i = 0; i < diamondColors.Count; i++)
            {
                color = diamondColors[i];
                
                for (int j = i+1; j < diamondColors.Count; j++)
                {
                    if (color == diamondColors[j])
                    {
                        mergedIndex.Add(j);
                        if (j == diamondColors.Count-1)
                        {
                            
                             MergeDiamonds(i,mergedIndex,color);
                             i += mergedIndex.Count;
                             mergedIndex.Clear();
                             return;
                        }
                    }
                    else
                    {
                        if (mergedIndex.Count != 0)
                        {
                            //merged index has a value and we are in a diffrent color index
                            MergeDiamonds(i, mergedIndex, color);
                            i += mergedIndex.Count;
                            mergedIndex.Clear();
                            return;
                        }

                        break;
                    }
                }
            }
        }*/
        
        /*private void MergeDiamonds(int toMerge, List<int> mergedIndex, string color)
        {
            mergedIndex.Sort();
            
            for (int i = 0; i < mergedIndex.Count; i++)
            {
                if (color == "Red")
                {
                    RemoveDiamond(mergedIndex[i]);//close the real one
                    var prefab = Instantiate(diamondPrefabRed, diamondList[mergedIndex[i]].transform);
                    prefab.transform.localPosition = Vector3.zero;
                    prefab.transform.DOMove(diamondList[toMerge].transform.position, 1f).OnComplete(() =>
                    {
                        Destroy(prefab);
                    });
                }else if (color == "Blue")
                {
                    RemoveDiamond(mergedIndex[i]); //close the real one
                    var prefab = Instantiate(diamondPrefabBlue, diamondList[mergedIndex[i]].transform);
                    prefab.transform.localPosition = Vector3.zero;
                    prefab.transform.DOMove(diamondList[toMerge].transform.position, 1f).OnComplete(() =>
                    {
                        Destroy(prefab);
                    });
                }else if (color == "Green")
                {
                    RemoveDiamond(mergedIndex[i]); //close the real one
                    var prefab = Instantiate(diamondPrefabGreen, diamondList[mergedIndex[i]].transform);
                    prefab.transform.localPosition = Vector3.zero;
                    prefab.transform.DOMove(diamondList[toMerge].transform.position, 1f).OnComplete(() =>
                    {
                        Destroy(prefab);
                    });
                }
                else
                {
                    RemoveDiamond(mergedIndex[i]); //close the real one
                    var prefab = Instantiate(diamondPrefabYellow, diamondList[mergedIndex[i]].transform);
                    prefab.transform.localPosition = Vector3.zero;
                    prefab.transform.DOMove(diamondList[toMerge].transform.position, 1f).OnComplete(() =>
                    {
                        Destroy(prefab);
                    });
                }
            }

            FillEmptySlots(mergedIndex.Count,mergedIndex.Last()+1);
            Debug.Log("** " + mergedIndex.Count + "  " + mergedIndex.Last()+1);
        }*/

        private void DiamondUpgradeAnimation(int index,int repeatCount)
        {
            diamondList[index].transform.DOScale(new Vector3(2.8f, 1.4f, 2.8f), 0.2f).SetEase(Ease.InOutSine).OnComplete((() =>
            {
                diamondList[index].transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.2f);
            })).SetLoops(repeatCount);
        }

        private void FillEmptySlots(int stepCount,int startIndex)
        {
            
            if (startIndex < diamondColors.Count)
            {
                for (int i = startIndex-stepCount; i < startIndex+1; i++)
                {
                    SetDiamondColor(diamondColors[i+stepCount],i);
                }
            }
            

            for (int i = lastIndex-stepCount; i < lastIndex+1; i++)
            {
                SetDiamondColor("clear",i);
            }

            var x = diamondColors.Count;
            for (int i = diamondColors.Count-1; i > x - stepCount-1; i--)
            {
                //Debug.Log(i);
                diamondColors.RemoveAt(i);
            }
            lastIndex -= stepCount;
        }

        private void OnDisable()
        {
            EventManager.CollectRed -= CollectDiamondRed;
            EventManager.CollectBlue -= CollectDiamondBlue;
            EventManager.CollectYellow -= CollectDiamondYellow;
            EventManager.CollectGreen -= CollectDiamondGreen;
        }
    }
}
