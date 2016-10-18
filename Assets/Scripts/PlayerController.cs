using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    //private Rigidbody rb;
    private Vector3 rbRotation;
    private Animator animator;
    private float doNothingTime = 0;
    private float hitTimeOK = 0;
    private float hitDelay = 1;
    private float hitDurationTime = 0.7f;
    private float hitDistanceEnemyEdge = 1f;
    private int hitDamage = 2;
    
    private EnemyController enemy;
    private float enemyDistance;
    private Vector3 destination;
    private Vector3 newPosition;
    private float moveSpeed = 5;

    private static GameObject fireballPrefab;

    private AbilityButton buttonWound;

    public static PlayerController Find()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        if (!fireballPrefab)
        {
            fireballPrefab = Resources.Load<GameObject>("Prefabs/Fireball");
        }
        
        //rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        destination = transform.position;
        newPosition = transform.position;

        buttonWound = GameObject.Find("Ability Button").GetComponent<AbilityButton>();
        buttonWound.Init("Wound", "Q", 5);
    }

    public void MoveTo(Vector3 point)
    {
        destination = point;
        destination.y = transform.position.y;
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
            destination = transform.position;
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

    public void CastFireball(Vector3 point)
    {
        transform.LookAt(point);

        Instantiate(fireballPrefab,
                    new Vector3(transform.position.x, 0.5f, transform.position.z),
                    transform.rotation);
    }

    public void CastWound(EnemyController target)
    {
        if (buttonWound.IsOnCooldown() || IsEnemyFar(target, hitDistanceEnemyEdge)) return;
        
        StartCoroutine(WoundCoroutine(target));

        buttonWound.Use();
        /*
        if (Time.time < woundTimeOK || IsEnemyFar(target, hitDistanceEnemyEdge)) return;
        
        GameObject.Find("Button").GetComponent<CanvasGroup>().alpha = 0.5f;

        woundTimeOK = Time.time + woundCooldown;
        
        */
    }

    private IEnumerator WoundCoroutine(EnemyController target)
    {
        int count = 0;
        while (target != null && count < 3)
        {
            count++;
            target.ApplyDamage(hitDamage);
            yield return new WaitForSeconds(1);
        }
    }

    private void Update()
    {
        FixRotationXZ();
        StioIfBlockedPath();

        if (Time.time < doNothingTime) return;
        
        animator.SetBool("Attacking", false);

        if (enemy)
        {
            transform.LookAt(enemy.transform);

            if (IsSelectedEnemyFar())
            {
                Chase();
            }
            else if (IsHitTimeOK())
            {
                Attack();
            }
        }
        else if (destination != transform.position)
        {
            GoToDestination();
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void StioIfBlockedPath()
    {
        if (transform.position != newPosition)
        {
            destination = transform.position;
            newPosition = transform.position;
        }
    }

    private void GoToDestination()
    {
        transform.LookAt(destination);
        DoMove(destination);
    }

    private bool IsSelectedEnemyFar()
    {
        return IsEnemyFar(enemy, hitDistanceEnemyEdge);
    }

    private bool IsEnemyFar(EnemyController enemy, float distance)
    {
        return Vector3.Distance(transform.position, enemy.transform.position) > distance + enemy.GetRadius();
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
        newPosition = Vector3.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);

        if (Physics.OverlapSphere(newPosition + new Vector3(0,0.5f,0), 0.4f).Length == 1)
        {
            transform.position = newPosition;
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void Attack()
    {
        animator.SetBool("Attacking", true);
        hitTimeOK = Time.time + hitDelay;
        doNothingTime = Time.time + hitDurationTime;
        enemy.ApplyDamage(hitDamage);
    }

    private void FixRotationXZ()
    {
        rbRotation = transform.rotation.eulerAngles;
        if (rbRotation.x != 0 || rbRotation.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, rbRotation.y, 0);
            //rb.MoveRotation(transform.rotation);
        }
    }
}
