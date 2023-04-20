using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;
using UnityEngine.Rendering.Universal;

public class EnemyMovementGranade : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float moveDistance;
    public Animator animator;
    public Timer granadeThrowTimer;

    private float directionBody;
    private bool isAttacking = false;
    private bool startTimer = false;

    public Light2D spriteLight;
    void Update()
    {
        spriteLight.lightCookieSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        if (target.GetComponent<PlayerHealth>() && target.GetComponent<PlayerHealth>().isActiveAndEnabled)
        {
            directionBody = (target.transform.position - transform.position).x / Mathf.Abs((target.transform.position - transform.position).x);
            transform.localScale = new Vector3(directionBody * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance >= moveDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, -1), Time.deltaTime * speed);
                animator.SetFloat("Move", distance);
                startTimer = true;
            }
            else if(isAttacking && this.gameObject.transform.parent != null)
            {
                animator.SetTrigger("Attack");
                isAttacking = false;
            }

            if (startTimer)
            {
                granadeThrowTimer.UpdateTimer();
                if (granadeThrowTimer.Done())
                {
                    startTimer = false;
                    isAttacking = true;
                    granadeThrowTimer.ResetTimer();
                }
            }
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
        animator.Play("RadiationGuyIdleAnim");
    }
}
