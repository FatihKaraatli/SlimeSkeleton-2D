using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class EnemyGunAttack : MonoBehaviour
{
    public GameObject bullet;
    public GameObject enemyHimself;
    public Transform spawnPoint;
    public float attackPower;
    public Timer timeBetweenTwoShoots;

    public float gunAxis;

    private GameObject turquosieShoulder;
    private GameObject player;
    private Animator animator;

    public AudioSource audioSourceEffects;
    public AudioClip audioClipEffects;

    public void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        turquosieShoulder = this.gameObject.transform.parent.gameObject;
    }

    public void Update()
    {
        if (!enemyHimself.GetComponent<EnemyHealth>().isActiveAndEnabled)
        {
            animator.SetTrigger("Death");
            this.gameObject.GetComponent<EnemyGunAttack>().enabled = false;
        }
        if (!player.GetComponent<PlayerHealth>().isActiveAndEnabled)
        {
            this.gameObject.GetComponent<EnemyGunAttack>().enabled = false;
        }
        else
        {
            float playerToEnemyDistance = Mathf.Abs(enemyHimself.gameObject.transform.position.x - player.transform.position.x);
            float enemyMoveDistance = enemyHimself.GetComponent<EnemyMovementDistance>().moveDistance;

            timeBetweenTwoShoots.UpdateTimer();
            if (timeBetweenTwoShoots.Done() && enemyMoveDistance > playerToEnemyDistance && this.gameObject.transform.parent != null)
            {
                animator.SetTrigger("Shoot");
                timeBetweenTwoShoots.ResetTimer();
            }
        }


        Vector3 difference = turquosieShoulder.transform.position - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y + gunAxis, difference.x) * Mathf.Rad2Deg;
        turquosieShoulder.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    public void GabrielEnemyAttacking()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);

        GameObject turquosieBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity);

        BulletMovement tmp = turquosieBullet.GetComponent<BulletMovement>();
        tmp.movementDir = (int)(Mathf.Abs(turquosieShoulder.transform.localScale.x) / turquosieShoulder.transform.localScale.x) * -1;
        tmp.transform.localScale = new Vector2(((int)(Mathf.Abs(turquosieShoulder.transform.localScale.x) / turquosieShoulder.transform.localScale.x)) * -1 * 
                                                        tmp.transform.localScale.x,
                                                        tmp.transform.localScale.y);
        tmp.damage = attackPower;
    }
}
