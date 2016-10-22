using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    
    public static int PLAYER_HERO_INDEX = 0;
    private static PlayerController playerController;

    private Hero hero;
    private HeroAbility tempAbility;

    private Vector3 rbRotation;
    private Animator animator;
    private float doNothingTime = 0;
    
    private EnemyController enemy;
    private float enemyDistance;
    private Vector3 destination;
    private Vector3 newPosition;

    private static GameObject fireballPrefab;

    private AbilityButton buttonWound;

    public static PlayerController FindOrCreate()
    {
        if (PLAYER_HERO_INDEX == -1)
        {
            Debug.Log("PLAYER_HERO is invalid");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            return null;
        }
        if (!playerController)
        {
            GameObject objFound = GameObject.FindGameObjectWithTag("Player");
            if (!objFound)
            {
                objFound = (GameObject) Instantiate(
                    Resources.Load<GameObject>("Prefabs/Heros/Hero" + PLAYER_HERO_INDEX),
                    Vector3.zero,
                    Quaternion.identity
                    );
            }
            playerController = objFound.GetComponent<PlayerController>();
        }
        return playerController;
    }

    private void Start()
    {
        hero = HeroBuilder.Build(PLAYER_HERO_INDEX, this);
        AbilityButton.CreateButtonsForHero(hero);

        animator = GetComponentInChildren<Animator>();
        destination = transform.position;
        newPosition = destination;
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

    public void Cast(int abilityIndex, Vector3 point, EnemyController target)
    {
        if (abilityIndex >= hero.CountAbilities()) return;

        tempAbility = hero.GetAbility(abilityIndex);
        
        if (tempAbility.IsOnCooldown()) return;
        if (tempAbility.NeedTarget())
        {
            if (!target || Distance.IsEnemyFar(this, target, tempAbility.GetRange())) return;

            tempAbility.Use(target);
            if (tempAbility.NeedCoroutine()) StartCoroutine(tempAbility.AbilityCoroutine(target));

        } else {
            if (Distance.IsPointFar(this, point, tempAbility.GetRange())) return;

            tempAbility.Use(point);
            if (tempAbility.NeedCoroutine()) StartCoroutine(tempAbility.AbilityCoroutine(point));
            
        }

        tempAbility.ChangeAnimation(animator);
        if (hero.GetAttackAbility().GetCastTime() > 0) doNothingTime = Time.time + hero.GetAttackAbility().GetCastTime();
    }

    private void Update()
    {
        FixRotationXZ();
        StopIfBlockedPath();

        if (Time.time < doNothingTime) return;
        
        animator.SetBool("Attacking", false);

        if (enemy)
        {
            transform.LookAt(enemy.transform);

            if (IsSelectedEnemyFar())
            {
                Chase();
            }
            else
            {
                Cast(0, enemy.transform.position, enemy);
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

    private void StopIfBlockedPath()
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
        return Distance.IsEnemyFar(this, enemy, hero.GetAttackAbility().GetRange());
    }

    private void Chase()
    {
        DoMove(enemy.transform.position);
    }

    private void DoMove(Vector3 dest)
    {
        newPosition = Vector3.MoveTowards(transform.position, dest, hero.GetStats().moveSpeed * Time.deltaTime);

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

    /*
    private void Attack()
    {
        animator.SetBool("Attacking", true);
        doNothingTime = Time.time + hero.GetAttackAbility().GetCastTime();
        enemy.ApplyDamage(hero.GetStats().baseDamage);
    }
    */

    private void FixRotationXZ()
    {
        rbRotation = transform.rotation.eulerAngles;
        if (rbRotation.x != 0 || rbRotation.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, rbRotation.y, 0);
            //rb.MoveRotation(transform.rotation);
        }
    }

    public Hero GetHero()
    {
        return hero;
    }
}
