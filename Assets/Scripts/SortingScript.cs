using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SortingScript : MonoBehaviour
{
    List<float> InitializeArray(List<float> array, int arraySize, float minVal, float maxVal)
    {
        array = new List<float>();
        for (int i = 0; i < arraySize; i++)
        {
            array.Add(UnityEngine.Random.Range(minVal, maxVal));
        }
        return array;
    }

    List<GameObject> DoQuickSort(List<GameObject> array)
    {
        //If length is too short return the list
        if (array.Count <= 1)
            return array;

        //create lists for left and right half of quicksort
        List<GameObject> leftArray = new List<GameObject>();
        List<GameObject> rightArray = new List<GameObject>();
        //random pivot value to determine left and right lists
        int pivotPos = UnityEngine.Random.Range(0, array.Count - 1);
        float pivotVal = array[pivotPos].GetComponent<BarController>().barVal;
        GameObject pivot = array[pivotPos];
        //if the current position's value is lower than the pivot put it in the left array, otherwise right array
        //unless it is the pivot position itself then it ignores
        for (int i = 0; i < array.Count; i++)
        {
            if (array[i].GetComponent<BarController>().barVal <= pivotVal && array[i].GetComponent<BarController>().barVal != 0 && i != pivotPos)
            {
                leftArray.Add(array[i]);
            }
            else if (array[i].GetComponent<BarController>().barVal != 0 && i != pivotPos)
            {
                rightArray.Add(array[i]);
            }
        }
        //recursively sort both halves of the array
        leftArray = DoQuickSort(leftArray);
        rightArray = DoQuickSort(rightArray);

        //build the array to return
        List<GameObject> returnArray = new List<GameObject>();
        for(int i = 0; i < leftArray.Count; i++)
            returnArray.Add(leftArray[i]);
        returnArray.Add(pivot);
        for(int i = 0; i < rightArray.Count; i++)
            returnArray.Add(rightArray[i]);

        return returnArray;
    }


    private List<float> mainList = new List<float>();
    public Transform WorldCenter;
    public GameObject barPrefab;
    public BarController barController;

    [Header("Array Fields")]
    [SerializeField] private int mainSize = 10;
    [SerializeField] private float maxVal = 100;
    private float minVal = 0.01f;

    [Header("Play Area Fields")]
    [SerializeField] private float barWidth = 1f;

    private List<GameObject> bars;
    void Start()
    {
        mainList = InitializeArray(mainList, mainSize, minVal, maxVal);
        bars = new List<GameObject>();
        for(int i = 0; i < mainSize; i++)
        {
            bars.Add(GameObject.Instantiate(barPrefab));
            bars[i].transform.position = new Vector2(i, 0);
            bars[i].GetComponent<BarController>().barVal = mainList[i];
        }
    }

    void Update()
    {
        for(int i = 0; i < bars.Count; i++)
        {
            bars[i].transform.position = new Vector2(i*barWidth, 0);
            bars[i].GetComponent<BarController>().barWidth = barWidth;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            bars = DoQuickSort(bars);
        }
        if(bars.Count != mainSize || Input.GetKeyDown(KeyCode.R))
        {
            for(int i = 0; i < bars.Count; i++)
            {
                Destroy(bars[i]);
            }
            mainList = InitializeArray(mainList, mainSize, minVal, maxVal);
            bars = new List<GameObject>();
            for(int i = 0; i < mainSize; i++)
            {
                bars.Add(GameObject.Instantiate(barPrefab));
                bars[i].transform.position = new Vector2(i, 0);
                bars[i].GetComponent<BarController>().barVal = mainList[i];
            }
        }

        if(mainSize > bars.Count)
        {
            bars.RemoveRange(mainSize, bars.Count - mainSize);
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            barWidth *= 1 - Input.GetAxis("Mouse ScrollWheel");
        }
    }
}
