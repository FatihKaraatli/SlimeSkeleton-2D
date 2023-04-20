using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class EnemyMovementDistance : MonoBehaviour
{
    public GameObject target;
    public GameObject gunArm;
    public float speed;
    public float moveDistance;
    public Animator animator;
    private float directionBody;
    private float directionArm;

    public Light2D spriteLight;

    void Update()
    {
        spriteLight.lightCookieSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        if (target.GetComponent<PlayerHealth>() && target.GetComponent<PlayerHealth>().isActiveAndEnabled)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            
            if (distance >= moveDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, -1), Time.deltaTime * speed);
            }
            animator.SetFloat("Move", distance);
            directionBody = (target.transform.position - transform.position).x / Mathf.Abs((target.transform.position - transform.position).x);
            transform.localScale = new Vector3(directionBody * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            directionArm = (target.transform.position - gunArm.transform.position).x / Mathf.Abs((target.transform.position - gunArm.transform.position).x);
            gunArm.transform.localScale = new Vector3(-directionArm, -directionArm, gunArm.transform.localScale.z);
        }
        /*else
        {
            animator.SetFloat("Move", 500);
        }*/

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, moveDistance);
    }
}
