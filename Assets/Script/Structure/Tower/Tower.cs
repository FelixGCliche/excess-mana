
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Tower : Structure
{
    //================================ Variables

    private Elements tower_element;

    public float radius;

    public Transform target;


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
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
            return;
        else
            Debug.DrawLine(this.spawnPosition, target.position, Color.green, 2.5f);

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
        }else
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



}
