using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }
}
