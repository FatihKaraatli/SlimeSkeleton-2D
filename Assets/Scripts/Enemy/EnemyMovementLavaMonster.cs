using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using CountDownTimer;

public class EnemyMovementLavaMonster : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float moveDistance;
    public Animator animator;
    public Timer lavaAttackTimer;
    public EnemyLavaMonsterAttack enemyLavaMonsterAttack;

    private float directionBody;

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
            else if (distance < moveDistance)
            {
                lavaAttackTimer.UpdateTimer();
                if (lavaAttackTimer.Done())
                {
                    animator.SetTrigger("Attack");
                    enemyLavaMonsterAttack.LavaMonsterLavaAttacking();

                    lavaAttackTimer.ResetTimer();
                }
            }
            animator.SetFloat("Move", distance);
            directionBody = ((target.transform.position - transform.position).x / Mathf.Abs((target.transform.position - transform.position).x)) * -1;
            transform.localScale = new Vector3(directionBody * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, moveDistance);
    }
}
