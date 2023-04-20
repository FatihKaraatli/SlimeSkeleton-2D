using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingMovement : MonoBehaviour
{
    public float rotSpeed;
    public float rotAmountDivide;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(0,0,Mathf.Sin(Time.time * rotSpeed) / rotAmountDivide);
        transform.localRotation = Quaternion.Euler(0, 0, vec.z);
    }
}
