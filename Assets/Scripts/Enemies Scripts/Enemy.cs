using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterMovement 
{
    public bool HasPlayerTargetTEST;

    private Transform playerTarget;
    private Vector3 playerLastTrackedPosition;

    private Vector3 startingPosition;

    private Vector3 enemyMovemetnMotion;

    private bool dealthDamageToPlayer;

    [SerializeField]
    private float damageCoolDownTreshold = 1f;
    private float damageCoolDownTimer;

    [SerializeField]
    private float damageAmount = 10f;

    [SerializeField]
    private float chaseSpeed = 0.8f;

    private float lastFollowTime;
    private float turningTimeDelay = 1f;

    [SerializeField]
    private float turningDelayRate = 1f;

    private Vector3 myScale;

    private CharacterHealth enemyHealth;

    private EnemyBacthHandler enemyBatch;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        playerLastTrackedPosition = playerTarget.position;

        startingPosition = transform.position;

        lastFollowTime = Time.time;
        turningTimeDelay = ((float)1f - (float)xSpeed);
        turningTimeDelay += 1f * turningDelayRate;

        enemyHealth = GetComponent<CharacterHealth>();

        enemyBatch = GetComponentInParent<EnemyBacthHandler>();
    }

    private void OnDisable()
    {
        if(!enemyHealth.IsAlive())
        enemyBatch.RemoveEnemy(this);
    }
    private void Update()
    {
        if(!playerTarget)
            return;

        if(!enemyHealth.IsAlive())
            return;

        HandleFacingDirection();
        HandleChasingPlayer();
    }

    void HandleChasingPlayer()
    {   
        // test if HasPlayerTarget
        if (HasPlayerTarget)
        {
            if (!dealthDamageToPlayer)
            {
               ChasePlayer();
            }
            else
            {
                if (Time.time < damageCoolDownTimer)
                {
                    enemyMovemetnMotion = startingPosition - transform.position;
                }
                else
                {
                    dealthDamageToPlayer = false;
                }
            }
        }
        else
        {
            enemyMovemetnMotion = startingPosition - transform.position;

            if(Vector3.Distance(transform.position, startingPosition) < 0.1f)
            {
                enemyMovemetnMotion = Vector3.zero;
            }
        }

        HandleMovement(enemyMovemetnMotion.x, enemyMovemetnMotion.y);
    }

    void ChasePlayer()
    {
        if(Time.time - lastFollowTime > turningTimeDelay)
        {
            playerLastTrackedPosition = playerTarget.position;
            lastFollowTime = Time.time;
        }

        if (Vector3.Distance(transform.position, playerLastTrackedPosition) > 0.016f)
        {
            enemyMovemetnMotion = (playerLastTrackedPosition - transform.position).normalized * chaseSpeed;
        }

        else
            enemyMovemetnMotion = Vector3.zero;
    }

    void HandleFacingDirection()
    {
        myScale = transform.localScale;

        if (HasPlayerTarget)
        {
            // chasing the player 
            // face the direction where the player is 
            if (playerTarget.position.x > transform.rotation.x)
                myScale.x = Mathf.Abs(myScale.x);
            else if(playerTarget.position.x < transform.rotation.x)
                myScale.x = -Mathf.Abs(myScale.x);
        }
        else
        {
            // going back to initial position
            // face the direction where the intial position is 
            if (startingPosition.x > transform.rotation.x)
                myScale.x = Mathf.Abs(myScale.x);
            else if (startingPosition.x < transform.rotation.x)
                myScale.x = -Mathf.Abs(myScale.x);
        }


        transform.localScale = myScale;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.PLAYER_TAG))
        {
            damageCoolDownTimer = Time.time + damageCoolDownTreshold;

            dealthDamageToPlayer = true;

            collision.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
        }
    }




} //class
