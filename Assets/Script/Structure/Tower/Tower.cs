
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Tower : Structure
{
    //================================ Variables

    private Elements tower_element;

    public float radius;

    public Transform target;

    [SerializeField] private GameObject wind_projectile_prefab;

    public float angle;

    public float  fire_rate = 1f;
    private float fire_countdown = 0.0f;

    public GameObject bulletprefeb;

    public GameObject projectile_spawn_point;

    //================================ Methods

    public Tower(Elements e)
    {
        tower_element = e;        
    }

    void awake()
    {
       
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.1f);
    }

    void Update()
    {
        if (target == null)
            return;
     
        Debug.DrawLine(projectile_spawn_point.transform.position, target.position, Color.green, 1.0f);

        if(fire_countdown <= 0.0f)
        {
            Shoot();
            fire_countdown = 1f / fire_rate;
        }

        fire_countdown -= Time.deltaTime;


    }

    private void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletprefeb, projectile_spawn_point.transform.position, GetProjectileRotation(target.position));
        bullet bullet = bulletGo.GetComponent<bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }

    }

    private Quaternion GetProjectileRotation(Vector3 position)
    {
        Vector3 direction = target.position - this.spawnPosition;
        float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
        return Quaternion.Euler(0, 0, angle);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if(distanceToEnnemy < shortestDistance)
            {
                shortestDistance = distanceToEnnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= radius)
        {
            target = nearestEnemy.transform;
            Vector3 targetDir = target.position - this.spawnPosition;

            angle = Vector3.Angle(this.spawnPosition, target.position);
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }




    //================================ Accessors
    public void SetAttackRadius(float radius)
    {
        this.radius = radius;
    }

    public void SetFireRate(float rate)
    {
        this.fire_rate = rate;
    }

    public void SetFireCountdown(float countdown)
    {
        this.fire_countdown = countdown;
    }


}
