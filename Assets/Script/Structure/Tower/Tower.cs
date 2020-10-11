
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

    bool neverDOne;

    public TextMesh text;
    public bool is_selected;

    public int required_small_runes = 5;
    public int required_medium_runes = 10;
    public int required_large_runes = 15;

    public int current_small_runes = 0;
    public int current_medium_runes = 0;
    public int current_large_runes = 0;

    public Script.Character.Player.PlayerInventory inventory;

    public GameObject player;
    public SpriteRenderer sr;
    //================================ Methods

    public bool CheckRuneQuantity(RuneSize rune_size)
    {
        switch(rune_size)
        {
            case RuneSize.SMALL:
                return (required_small_runes <= inventory.GetRuneQuantity(current_element, rune_size));

            case RuneSize.MEDIUM:
                return (required_medium_runes <= inventory.GetRuneQuantity(current_element, rune_size));

            case RuneSize.LARGE:
                return (required_large_runes <= inventory.GetRuneQuantity(current_element, rune_size));

            default:
                return false;
        }
    }

    public int RuneQuantityNumber(RuneSize rune_size)
    {
        switch (rune_size)
        {
            case RuneSize.SMALL:
                return (required_small_runes);

            case RuneSize.MEDIUM:
                return (required_medium_runes);

            case RuneSize.LARGE:
                return (required_large_runes);

            default:
                return 9999; // ?
        }
    }

    public void LevelCap()
    {
        if(current_level <= 4)
        {
            required_small_runes = 5;
            required_medium_runes = 0;
            required_large_runes = 0;
        }

        if (current_level > 4 && current_level <= 8)
        {
            required_small_runes = 10;
            required_medium_runes = 5;
            required_large_runes = 0;
        }

        if (current_level > 8 && current_level <= 12)
        {
            required_small_runes = 15;
            required_medium_runes = 10;
            required_large_runes = 5;
        }

        if (current_level > 12 && current_level <= 14)
        {
            required_small_runes = 15;
            required_medium_runes = 15;
            required_large_runes = 10;
        }

        if (current_level > 14 )
        {
            required_small_runes = 15;
            required_medium_runes = 15;
            required_large_runes = 15;
        }
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.1f);
        default_life = current_life;
        neverDOne = false;

        text.text = "Current Level : " + current_level.ToString();
        is_selected = false;

        inventory = new Script.Character.Player.PlayerInventory();

        sr = GetComponent<SpriteRenderer>();
    }



    private void OnMouseOver()
    {
      //  TakeDmage(10f, current_element);
        is_selected = true;

        Debug.Log(current_element + "Runes number : " + inventory.GetRuneQuantity(current_element, RuneSize.SMALL));
    }

    public IEnumerator DoRepair(float health)
    {
        for (; ; )
        {
            current_life += health;
            healthBar.UpdateProgressBar(current_life / 100);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Player player = collision.GetComponentInParent<Player>();
        if(player != null)
        {
            is_selected = true;
            sr.color = new Color(0,255,0,50);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();
        if (player != null)
        {
            is_selected = false;
            sr.color = new Color(255, 255, 255, 100);

        }
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
