using System.Collections;
using UnityEngine;
using Script.Character;
using Script.GameMaster.WaveSpawner;

public class Enemy : Character
{
    [SerializeField] int enemyAttackRange = 2;
    [SerializeField] int enemyAttackSpeed = 1;
    [SerializeField] int enemyDetectionRange = 10;
    [SerializeField] int damage = 10;
    [SerializeField] bool isInWave;

    private int test;
    Targets target = Targets.NONE;
    Vector2 targetPosition;
    Structure targetStructure;
    Player player;
    bool isDealingDamage;
    bool isPlayerDetected;
    bool isSlithering;

    AudioSource deathSource;
    AudioSource attackSource;
    AudioSource slitherSource;

    private WaveSpawner waveSpawner;

    void Start()
    {
        health = baseHealth;
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        deathSource = GameObject.Find("EnemyDeathSource").gameObject.GetComponent<AudioSource>();
        attackSource = GameObject.Find("EnemyAttackSource").gameObject.GetComponent<AudioSource>();
        slitherSource = GameObject.Find("EnemySlitherSource").gameObject.GetComponent<AudioSource>();
        
        if (!isInWave)
            target = Targets.PLAYER;
    }

    void Update()
    {
        if (isInWave)
        {
            if (target == Targets.STRUCTURE && targetStructure == null)
                GetNewTarget();

            if (!IsNearTarget())
            {
                Move();
            }
            else
            {
                Attack();
            }
        }
        else
        {
            IsPlayerNear();

            if (isPlayerDetected)
            {
                if (IsNearTarget())
                {
                    Attack();
                }
                else
                {
                    Move();
                }
                    
            }
        }
    }

    void Move()
    {
        if (isInWave) 
            GetNewTarget();
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector2 direction;

        direction.x = targetPosition.x - transform.position.x;
        direction.y = targetPosition.y - transform.position.y;
        
        if (!isSlithering)
            StartCoroutine(PlaySlitherSound());
            
        Mover.Move(direction.normalized);
    }

    bool IsNearTarget()
    {
        if (target == Targets.PLAYER)
            targetPosition = player.transform.position;
        
        if (target != Targets.NONE)
        {
            float distanceToTarget;

            distanceToTarget = DistanceCalculator(targetPosition);

            if (distanceToTarget <= enemyAttackRange)
            {
                return true;
            }
        }

        return false;
    }

    void IsPlayerNear()
    {
        if (DistanceCalculator(player.transform.position) <= enemyDetectionRange)
            isPlayerDetected = true;
        else
            isPlayerDetected = false;
    }

    void GetNewTarget()
    {
        float distanceToTarget;
        float distanceToCurrentTarget = float.MaxValue;
        Structure currentTarget;

        if(FindObjectsOfType<Structure>().Length > 0)
        {
            foreach (Structure structure in FindObjectsOfType<Structure>())
            {
                distanceToTarget = DistanceCalculator(structure.transform.position);

                if (distanceToTarget < distanceToCurrentTarget)
                {
                    distanceToCurrentTarget = distanceToTarget;
                    currentTarget = structure;
                    target = Targets.STRUCTURE;
                    targetPosition = currentTarget.transform.position;
                    targetStructure = currentTarget;
                }
            }
        }

        /*if (DistanceCalculator(player.transform.position) < distanceToCurrentTarget)
        {
            target = Targets.PLAYER;
            targetPosition = player.transform.position;
        }*/
    }

    void Attack()
    {
        if (!isDealingDamage)
            StartCoroutine(DealDamage());
    }


    protected override void Kill()
    {
        StartCoroutine(Die());
        //Play sound
        if (waveSpawner != null)
            waveSpawner.Notify();
        Destroy(gameObject);
    }

    float DistanceCalculator(Vector2 targetPosition)
    {
        Vector2 directionToTarget;
        float distanceToTarget;

        directionToTarget.x = (transform.position.x - targetPosition.x) * (transform.position.x - targetPosition.x);
        directionToTarget.y = (transform.position.y - targetPosition.y) * (transform.position.y - targetPosition.y);
        distanceToTarget = Mathf.Sqrt(directionToTarget.x + directionToTarget.y);

        return distanceToTarget;
    }

    IEnumerator DealDamage()
    {
        isDealingDamage = true;
        while ( target == Targets.STRUCTURE && targetStructure != null && IsNearTarget())
        {
            targetStructure.TakeDamage(damage, element);
            attackSource.Play();
            yield return new WaitForSeconds(enemyAttackSpeed);
        }

        while (target == Targets.PLAYER && player != null && IsNearTarget())
        {
            player.TakeDamage(damage, element);
            attackSource.Play();
            player.PlayAttackedSound();
            yield return new WaitForSeconds(enemyAttackSpeed);
        }

        isDealingDamage = false;
    }

    public void Subscribe(WaveSpawner spawner)
    {
        waveSpawner = spawner;
        isInWave = true;
        target = Targets.STRUCTURE;
    }

    IEnumerator PlaySlitherSound()
    {
        isSlithering = true;
        while (!IsNearTarget())
        {
            if (target == Targets.PLAYER && isPlayerDetected)
            {
                slitherSource.Play();
                yield return new WaitForSeconds(2);
            }
            else if (target == Targets.STRUCTURE)
            {
                slitherSource.Play();
                yield return new WaitForSeconds(2);
            }
        }
        isSlithering = false;
    }

    IEnumerator Die()
    {
        deathSource.Play();
        yield return new WaitForSeconds(0.3f);
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
