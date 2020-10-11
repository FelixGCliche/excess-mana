using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Character;
using Script.GameMaster.WaveSpawner;
using UnityEngine;

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

    private WaveSpawner waveSpawner;

    void Start()
    {
        health = baseHealth;
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();

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
                Move();
            else
                Attack();
        }
        else
        {
            IsPlayerNear();

            if (isPlayerDetected)
            {
                if (IsNearTarget())
                    Attack();
                else
                    Move();
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
                return true;
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

        if (DistanceCalculator(player.transform.position) <= distanceToCurrentTarget)
        {
            target = Targets.PLAYER;
            targetPosition = player.transform.position;
        }
    }

    void Attack()
    {
        //Play Sound
        if (!isDealingDamage)
            StartCoroutine(DealDamage());
    }


    protected override void Kill()
    {
        //Play sound
        if (waveSpawner != null)
        {
            waveSpawner.Notify();
            Debug.Log(gameObject.name);
        }
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
        while (targetStructure != null && IsNearTarget() && target == Targets.STRUCTURE)
        {
            targetStructure.TakeDamage(damage, element);
            yield return new WaitForSeconds(enemyAttackSpeed);
        }

        while (player != null && IsNearTarget() && target == Targets.PLAYER)
        {
            player.TakeDamage(damage, element);
            yield return new WaitForSeconds(enemyAttackSpeed);
        }

        isDealingDamage = false;
    }

    public void Suscribe(WaveSpawner spawner)
    {
        waveSpawner = spawner;
    }
}
