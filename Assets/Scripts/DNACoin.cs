using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNACoin : MonoBehaviour
{
    public float power;

    private bool startThrow = true;
    private Rigidbody2D rb;
    private GameObject player;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (startThrow)
        {
            //var direction =  
            rb.AddForce((Vector2.up + Vector2.left) * Time.fixedDeltaTime * power);
            startThrow = false;
        }
    }
    void Update()
    {
        
    }
}
