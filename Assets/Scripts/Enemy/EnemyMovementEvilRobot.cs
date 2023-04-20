using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;
using UnityEngine.Rendering.Universal;

public class EnemyMovementEvilRobot : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float moveDistance;
    public Animator animator;
    public Timer laserAttackTimer;
    public EnemyEvilRobotLaserAttack enemyEvilRobotLaserAttack1;
    public EnemyEvilRobotLaserAttack enemyEvilRobotLaserAttack2;
    
    private float directionBody;
    private bool isAttacking = false;
    private bool startTimer = false;

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
                isAttacking = true;
            }
            else if (distance < moveDistance)
            {
                laserAttackTimer.UpdateTimer();
                if (laserAttackTimer.Done())
                {
                    enemyEvilRobotLaserAttack1.EvilRobotLaserAttacking();
                    enemyEvilRobotLaserAttack2.EvilRobotLaserAttacking();

                    laserAttackTimer.ResetTimer();
                }
            }
            animator.SetFloat("Move", distance);
            directionBody = (target.transform.position - transform.position).x / Mathf.Abs((target.transform.position - transform.position).x);
            transform.localScale = new Vector3(directionBody * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, moveDistance);
    }

    public void StopAttack()
    {
        startTimer = true;
    }
}
