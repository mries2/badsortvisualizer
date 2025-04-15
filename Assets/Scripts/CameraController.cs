using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float horizontal, vertical;
    public float movespeed = 0.02f;
    void Start()
    {
        horizontal = 0;
        vertical = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(horizontal * movespeed, vertical * movespeed, 0);
    }
}
