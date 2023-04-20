using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class EnemyAttack : MonoBehaviour
{
    public int enemyAttackPower;
    public Timer flyTime;
    public LayerMask layer;
    public float flyPower;
    public EnemyMovement enemyMovement;

    private GameObject playerObject;
    private bool isPunched = false;
    private Vector3 a;

    public void FixedUpdate()
    {
        if (isPunched)
        {
            Vector3 movDir = this.gameObject.transform.position - playerObject.gameObject.transform.position;
            this.gameObject.GetComponent<Rigidbody2D>().AddForce((movDir.normalized + Vector3.up + (a = movDir.x > 0 ? Vector3.right : Vector3.left)) * flyPower * Time.fixedDeltaTime);
            flyTime.UpdateTimer();
            if (flyTime.Done())
            {
                isPunched = false;
                flyTime.ResetTimer();
            }
        }
    }

    public void BernardEnemyAttacking()
    {
        GameObject player = ExplosionDamage(transform.position,5);
        if (player != null)
        {
            if (player.GetComponent<PlayerHealth>() != null)
            {
                if (player.GetComponent<PlayerAttack>().isDefencing)
                {
                    playerObject = player;
                    isPunched = true;
                }
                else
                {
                    if (player.GetComponent<PlayerHealth>().health <= enemyAttackPower)
                    {
                        player.GetComponent<PlayerHealth>().DecraseHealth(enemyAttackPower);
                        return;
                    }
                    player.GetComponent<PlayerHealth>().DecraseHealth(enemyAttackPower);
                }
            }
        }
    }
    public GameObject ExplosionDamage(Vector2 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                GameObject target = hitCollider.gameObject;
                return target;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
