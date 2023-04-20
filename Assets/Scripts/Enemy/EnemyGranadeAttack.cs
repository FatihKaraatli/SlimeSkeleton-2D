using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGranadeAttack : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject granadePrefab;
    public GameObject player;

    public float distanceSpeedConst;

    private float baseDistance = 29.95f;
    private float basespeed = 3100f;
    private float baseFormula = 0;
    private void Awake()
    {
        baseFormula = Mathf.Sqrt(baseDistance) * basespeed;
    }

    public void ThrowGranade()
    {
        GameObject granade = Instantiate(granadePrefab, spawnPoint.transform.position, Quaternion.identity);
        distanceSpeedConst = baseFormula / Mathf.Sqrt(Mathf.Abs(player.transform.position.x - this.gameObject.transform.position.x));
        float time = Mathf.Abs(player.transform.position.x - this.gameObject.transform.position.x) * distanceSpeedConst;
        granade.GetComponent<GranadeExplosion>().speed = time;
        granade.GetComponent<GranadeExplosion>().isPlayerRight = (player.transform.position.x - this.gameObject.transform.position.x) > 0 ? true : false;
    }    
}
