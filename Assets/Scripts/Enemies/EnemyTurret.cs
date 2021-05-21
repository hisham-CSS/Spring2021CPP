using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyTurret : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;

    public float projectileForce;

    public float projectileFireRate;

    float timeSinceLastFire = 0.0f;
    public int health;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
        if (projectileForce <= 0)
        {
            projectileForce = 7.0f;
        }

        if (projectileFireRate <= 0)
        {
            projectileFireRate = 2.0f;
        }

        if (health <= 0)
        {
            health = 5;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //HINT 1 FOR LAB: CHECK SOMETHING PRIOR TO FIRING TO DETERMING WHICH DIRECTION TO FIRE - CAN ALSO INCLUDE DISTANCE
        if (Time.time >= timeSinceLastFire + projectileFireRate)
        {
            anim.SetBool("Fire", true);
            timeSinceLastFire = Time.time;
        }
    }

    public void Fire()
    {
        //HINT 2 FOR LAB: IF YOU KNOW THE DIRECTION - YOU CAN ADD LOGIC HERE TO FIRE IN THAT DIRECTION
        Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        temp.speed = projectileForce;
    }

    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health--;
            Destroy(collision.gameObject);
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
