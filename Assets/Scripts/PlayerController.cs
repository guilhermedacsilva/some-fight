using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody rb;
    private Vector3 rbRotation;
    private Animator animator;
    private float doNothingTime = 0;
    private float hitTimeOK = 0;
    private float hitDelay = 1;
    private float hitDurationTime = 0.7f;
    private float hitDistanceEnemyEdge = 1f;
    private int hitDamage = 20;
    
    private EnemyController enemy;
    private float enemyDistance;
    private Vector3 destination;
    private Vector3 newPosition;
    private float moveSpeed = .1f;

    public static PlayerController Find()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        destination = rb.position;
        newPosition = rb.position;
    }

    public void MoveTo(Vector3 point)
    {
        destination = point;
        destination.y = rb.position.y;
        if (enemy)
        {
            enemy.Select(false);
            enemy = null;
        }
    }

    public void ChaseAttack(EnemyController newEnemy)
    {
        if (newEnemy == null)
        {
            destination = rb.position;
            if (enemy)
            {
                enemy.Select(false);
            }
        }
        else if (enemy != newEnemy)
        {
            newEnemy.Select(true);
            if (enemy) enemy.Select(false);
        }
        enemy = newEnemy;
    }

    private void FixedUpdate()
    {
        if (Time.time < doNothingTime) return;

        animator.SetBool("Attacking", false);

        if (enemy)
        {
            if (IsEnemyFar())
            {
                Chase();
                animator.SetBool("Walking", true);
            }
            else if (IsHitTimeOK())
            {
                Attack();
                animator.SetBool("Attacking", true);
            }
        }
        else if (destination != rb.position)
        {
            GoToDestination();
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void GoToDestination()
    {
        transform.LookAt(destination);
        DoMove(destination);
    }

    private bool IsEnemyFar()
    {
        return Vector3.Distance(rb.position, enemy.transform.position) > hitDistanceEnemyEdge + enemy.GetRadius();
    }

    private bool IsHitTimeOK()
    {
        return Time.time >= hitTimeOK;
    }

    private void Chase()
    {
        DoMove(enemy.transform.position);
    }

    private void DoMove(Vector3 dest)
    {
        newPosition = Vector3.MoveTowards(rb.position, dest, moveSpeed);
        rb.position = newPosition;
    }

    private void Attack()
    {
        hitTimeOK = Time.time + hitDelay;
        doNothingTime = Time.time + hitDurationTime;
        enemy.ApplyDamage(hitDamage);
    }

    private void Update()
    {
        FixRotationXZ();

        if (Time.time < doNothingTime) return;

        if (enemy)
        {
            transform.LookAt(enemy.transform);
        }
        
        // verify if it is stucked on a wall
        if (rb.position != newPosition)
        {
            destination = rb.position;
            newPosition = rb.position;
        }
    }

    private void FixRotationXZ()
    {
        rbRotation = rb.rotation.eulerAngles;
        if (rbRotation.x != 0 || rbRotation.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, rbRotation.y, 0);
            rb.MoveRotation(transform.rotation);
        }
    }
}
