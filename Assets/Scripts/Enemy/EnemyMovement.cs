using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyMovement : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float stopDistance;
    public float moveDistance;
    public Animator animator;
    private float direction;
    bool isEnemyAttacking = false;
    bool isEnemyHurting = false;

    public Light2D spriteLight;

    void Update()
    {
        spriteLight.lightCookieSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        if (target.GetComponent<PlayerHealth>() && target.GetComponent<PlayerHealth>().isActiveAndEnabled)
        {
            direction = (target.transform.position - transform.position).x / Mathf.Abs((target.transform.position - transform.position).x);
            transform.localScale = new Vector3(direction * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance >= moveDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, -1), Time.deltaTime * speed);
                animator.SetFloat("Move", distance);
            }
            if (distance <= stopDistance && !isEnemyAttacking && !isEnemyHurting && this.gameObject.transform.parent != null)
            {
                animator.SetTrigger("Attack");
                isEnemyAttacking = true;
            }












            /*float distance = Vector3.Distance(transform.position , target.transform.position);
        
            if (distance <= stopDistance && !isEnemyAttacking && !isEnemyHurting && this.gameObject.transform.parent != null)
            {
                animator.SetTrigger("Attack");
                isEnemyAttacking = true;
            }
            else if (distance <= moveDistance && distance >= stopDistance)
            {

                isEnemyAttacking = false;
                animator.SetFloat("Move", distance);
                transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.transform.position.x,transform.position.y,-1), Time.deltaTime * speed);
            }
            else if(distance > moveDistance)
            {

                isEnemyAttacking = false;
                animator.SetFloat("Move", distance);
            }
            direction = (target.transform.position - transform.position).x / Mathf.Abs((target.transform.position - transform.position).x);
            transform.localScale = new Vector3(direction * Mathf.Abs(transform.localScale.x),transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);*/
        }
        else
        {
            animator.SetFloat("Move", 500);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,stopDistance);
        Gizmos.DrawWireSphere(transform.position,moveDistance);
    }

    public void EnemyAttackStart()
    {
       /* animator.SetBool("Attack", false);
        isEnemyAttacking = true;
        isEnemyAttacking = false;
        Debug.Log("attack start");*/
    }
    public void EnemyAttackFinish()
    {   
        isEnemyAttacking = false;
        /*isEnemyHurting = false;
        Debug.Log("attack finish");*/
    }
    
    public void EnemyHurtStart()
    {
        Debug.Log("Hurt Start");
        isEnemyHurting = true;
        isEnemyAttacking = false;
        animator.SetBool("Hurt", false);
        //animator.SetBool("Attack", false);
    }
    public void EnemyHurtFinish()
    {
        Debug.Log("Hurt Finish");
        isEnemyHurting = false;
        isEnemyAttacking = false;
    }
}
