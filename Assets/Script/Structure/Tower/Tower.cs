
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Tower : Structure
{
    //================================ Variables
    public float radius;

    public Transform target;

    public int max_level;

    public float  fire_rate = 1f;
    private float fire_countdown = 0.0f;

    public GameObject projectile_spawn_point;

    public TowerAttacks tower_attacks;

    public int id;
    public float default_life;

    [SerializeField] private KeyCode interactKey = KeyCode.Mouse1;
    bool interackKeyDown;
    bool neverDOne;

    public TextMesh text;
    public bool is_selected;

    //================================ Methods


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.1f);
        default_life = current_life;
        interackKeyDown = false;
        neverDOne = false;

        text.text = "Current Level : " + current_level.ToString();
        is_selected = false;

    }

    private void OnMouseOver()
    {

      //  TakeDmage(10f, current_element);
        is_selected = true;
        /*if(Input.GetMouseButtonDown(2))
        {
            current_life = 45;
        }

        if (Input.GetMouseButton(1))
        {
            neverDOne = true;
        }
            if (current_life != default_life)
            {
                if(neverDOne)
                {
                    //repair
                    StartCoroutine(DoRepair(10));
                    if (current_life >= default_life)
                    {
                        StopAllCoroutines();
                        current_life = default_life;
                        neverDOne = false;
                    }
                }

            }
            else
            {
                //upgrade
                Upgrade();
            }
        
        Debug.Log(current_life);*/

    }

    public IEnumerator DoRepair(float health)
    {
        for (; ; )
        {
            current_life += health;
            healthBar.AdjustHealthBar(current_life / 100);
            yield return new WaitForSeconds(1.0f);

            if (current_life >= default_life)
                yield break;
        }
    }

    private void OnMouseExit()
    {
        is_selected = false;
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

    public void AddExp()
    {

    }

    public override void SetCurrentLevel(int level)
    {
        this.current_level = level;
        text.text = "Current Level : " + current_level.ToString();
    }

    private void Shoot()
    {
        switch(current_element)
        {
            case Elements.WIND:
                tower_attacks.WindAttack(projectile_spawn_point.transform, target);
                break;

            case Elements.FIRE:
                tower_attacks.FireAttack(projectile_spawn_point.transform, target);
                break;

            case Elements.WATER:
                tower_attacks.WaterAttack(projectile_spawn_point.transform, target);
                break;

            case Elements.EARTH:
                tower_attacks.EarthAttack(projectile_spawn_point.transform, target);
                break;
        }
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
            Vector3 targetDir = target.position - this.spawnPosition;
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

    public void SetId(int id)
    {
        this.id = id;
    }
}
