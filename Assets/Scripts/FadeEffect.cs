using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderer;
    public float decraseSpeed;

    void Update()
    {
        for(int i = 0; i< spriteRenderer.Length;i++) 
            spriteRenderer[i].color = new Color(spriteRenderer[i].color.r, spriteRenderer[i].color.g, spriteRenderer[i].color.b, spriteRenderer[i].color.a - (Time.deltaTime * decraseSpeed));
        if (spriteRenderer[0].color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
