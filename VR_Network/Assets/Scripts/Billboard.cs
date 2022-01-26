using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform canvas;

    void Start()
    {
        
    }

    void Update()
    {
        canvas.forward = Camera.main.transform.forward;    
    }
}
