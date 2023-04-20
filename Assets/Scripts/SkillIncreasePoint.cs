using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIncreasePoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.GetComponent<SkillTree>().enabled = true;
            this.gameObject.GetComponent<SkillTree>().PinkPointEntered(GameObject.FindGameObjectWithTag("Player"));
        }
    }
}
