
using DG.Tweening;
using Harmony;
using Script.Character.Player;
using Script.Projectile;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Tower : Structure
{
    [SerializeField] private float radius;

    [SerializeField] private float damage = 10;

    [SerializeField] private Transform target;

    [SerializeField] private float fireRate = 1f;
    private float fire_countdown = 0.0f;

    [SerializeField] private GameObject projectileSpawnPoint;

    [SerializeField] private Projectile projectile;

    [SerializeField] private float levelUpFireRateBonus = 0.1f;

    [SerializeField] private float levelUpMaxHealthBonus = 20.0f;

    [SerializeField] private float levelUpDamageBonus = 1.0f;

    public TextMesh text;

    private PlayerInventory playerInventory;

    private int currentLevel = 0;

    void Start()
    {

        text.text = currentLevel.ToString();

        playerInventory = Finder.PlayerInventory;
    }

    void Update()
    {
        if (target == null || Vector2.Distance(transform.position, target.transform.position) > radius)
        {
            UpdateTarget();
            return;
        }

        if(fire_countdown <= 0.0f)
        {
            Shoot();
            fire_countdown = 1f / fireRate;
        }

        fire_countdown -= Time.deltaTime;
    }

    public void SetCurrentLevel(int level)
    {
        this.currentLevel = level;
        text.text = currentLevel.ToString();
    }

    private void Shoot()
    {
        Projectile newProjectile = Instantiate(projectile, projectileSpawnPoint.transform.position, GetProjectileRotation(target, projectileSpawnPoint.transform));
        newProjectile.SetDamage(damage);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");
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
        }
        else
        {
            target = null;
        }
    }

    private Quaternion GetProjectileRotation(Transform target, Transform projectileSpawnPoint)
    {
        Vector3 dir = target.position - projectileSpawnPoint.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    private void Upgrade()
    {
        if (currentHealth == initialHealth)
        {
            if (playerInventory.CanPay((currentLevel ^ 2) + currentLevel * 5, currentElement))
            {
                currentLevel++;
                fireRate += levelUpFireRateBonus;
                currentHealth = initialHealth += levelUpMaxHealthBonus;
                damage += levelUpDamageBonus;
            }
        }
        else 
        {
            float healthToRegain = initialHealth - currentHealth;
            int tickToRegain = (healthToRegain / 5).RoundToInt();
            for (int i = 0; i < tickToRegain; i++)
            {
                if (playerInventory.CanPay(1, currentElement))
                {
                    Repair(5);
                }
                else 
                {
                    break;
                }
            }
        }
    }
}
