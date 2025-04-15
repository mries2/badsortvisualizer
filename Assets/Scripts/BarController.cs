using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BarController : MonoBehaviour
{
    public float barVal = 1;
    public float barWidth = 1;
    void Start()
    {
        transform.localScale = new Vector3(barWidth,barVal,barWidth);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(barWidth,barVal,barWidth);
    }

    public void SetSize(float size)
    {
        transform.localScale = new Vector3(barWidth,size,barWidth);
    }
}
