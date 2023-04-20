using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;
using EZCameraShake;
using UnityEngine.Rendering.Universal;

public class GranadeExplosion : MonoBehaviour
{
    public float speed;
    public float damage;
    public float explosionDestroyRadius;
    public float explosionDamageRadius;
    public GameObject explosionEffectPrefab;
    public Timer explosionDestroyTimer;
    public Timer flyGranadeTimer;
    public float flyGranadePower;
    public LayerMask layer;
    public bool isPlayerRight;
    public bool isPlayerHaveThisGranade;
    public GameObject BombAreaEffectObject;
    
    private Rigidbody2D rb;
    private bool throwControl = true;
    private bool granadeTouchControl = false;
    private bool effectCrateControl = false;
    private bool isGranadeFly = false; 
    private GameObject granadeEffect;
    private Camera mainCam;
    private GameObject playerObject;

    public AudioSource audioSourceEffects;
    public AudioClip audioClipGrenedaEffects;
    public AudioClip audioClipRichochetEffect;


    void Start()
    {
        mainCam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (throwControl)
        {
            if(isPlayerRight) rb.AddForce((Vector2.right + Vector2.up) * Time.fixedDeltaTime * speed);
            else rb.AddForce((Vector2.left + Vector2.up) * Time.fixedDeltaTime * speed);
            throwControl = false;
        }
        
        granadeTouchControl = ExplosionArea(this.gameObject.transform.position, explosionDestroyRadius);
        if (granadeTouchControl)
        {
            if (!effectCrateControl)
            {
                granadeEffect = Instantiate(explosionEffectPrefab,transform.position,Quaternion.identity);
                ExplosionDamage(this.gameObject.transform.position, explosionDamageRadius);
                effectCrateControl = true;
            }
            
            explosionDestroyTimer.UpdateTimer();
            if (explosionDestroyTimer.Done())
            {
                Destroy(BombAreaEffectObject);
                Destroy(this.gameObject);
                Destroy(granadeEffect);
            }
        }

        if (isGranadeFly)
        {
            Vector3 movDir = this.gameObject.transform.position - playerObject.gameObject.transform.position;
            if(!isPlayerRight) this.gameObject.GetComponent<Rigidbody2D>().AddForce((movDir.normalized + Vector3.up + Vector3.right) * flyGranadePower * Time.fixedDeltaTime);
            else this.gameObject.GetComponent<Rigidbody2D>().AddForce((movDir.normalized + Vector3.up + Vector3.left) * flyGranadePower * Time.fixedDeltaTime);
            flyGranadeTimer.UpdateTimer();
            if (flyGranadeTimer.Done())
            {
                isGranadeFly = false;
                flyGranadeTimer.ResetTimer();
            }
        }
    }

    public bool ExplosionArea(Vector2 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player") && hitCollider.gameObject.GetComponent<PlayerAttack>().isDefencing)
            {
                if(!isPlayerHaveThisGranade) audioSourceEffects.PlayOneShot(audioClipRichochetEffect);
                playerObject = hitCollider.gameObject;
                isPlayerHaveThisGranade = true;
                isGranadeFly = true;
                return false;
            }
            else if (hitCollider.CompareTag("Player") || hitCollider.CompareTag("Wall")) 
            {
                BombAreaEffectObject.SetActive(true);
                BombAreaEffectObject.transform.parent = null;
                mainCam.GetComponent<CameraShaker>().ShakeOnce(0.25f, 3, 0.1f, 2);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<Light2D>().enabled = false;
                return true;
            }
        }
        return false;
    }

    public void ExplosionDamage(Vector2 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player") && !isPlayerHaveThisGranade)
            {
                if (hitCollider.gameObject.GetComponent<PlayerAttack>().isDefencing)
                {
                    return;
                }
                else
                {
                    audioSourceEffects.PlayOneShot(audioClipGrenedaEffects);
                    hitCollider.GetComponent<PlayerHealth>().DecraseHealth(damage);
                }
            }
            else if (hitCollider.CompareTag("Enemy") && isPlayerHaveThisGranade)
            {
                audioSourceEffects.PlayOneShot(audioClipGrenedaEffects);
                hitCollider.GetComponent<EnemyHealth>().DecraseHealth(damage);
            }
            else if (hitCollider.CompareTag("Wall"))
            {
                audioSourceEffects.PlayOneShot(audioClipGrenedaEffects);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionDamageRadius);
    }
}
