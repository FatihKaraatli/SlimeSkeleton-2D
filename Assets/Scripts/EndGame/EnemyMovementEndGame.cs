using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyMovementEndGame : MonoBehaviour
{
    public GameObject player;
    public Light2D spriteLight;

    public float speed;
    public float stopDistance;

    private GameObject enemy;
    private Animator enemyAnimator;

    public void Start()
    {
        enemy = this.gameObject;
        enemyAnimator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        spriteLight.lightCookieSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, -1), Time.deltaTime * speed);
            enemyAnimator.SetBool("Move", true);
        }
        else
        {
            enemyAnimator.SetBool("Move", false);
        }
    }
}
