using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LightRotate : MonoBehaviour
{
    public float speed;
    public Vector3 rotation; 
    
    void Update()
    {
        transform.Rotate(rotation.x * speed * Time.deltaTime, rotation.y * speed * Time.deltaTime, rotation.z * speed * Time.deltaTime);
    }
}
