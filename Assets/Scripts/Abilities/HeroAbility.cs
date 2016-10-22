using System;
using System.Collections;
using UnityEngine;

public class HeroAbility
{
    protected PlayerController player;
    protected int index;
    protected AbilityButton button;

    protected string abilityName;
    protected float damageMultiplier;
    protected float cooldown;
    protected float timeWithCooldown = 0;
    protected float castTime;
    protected float range;
    protected bool needTarget = true;
    protected bool needCoroutine = false;

    public HeroAbility(string abilityName, float damageMultiplier, float castTime, float cooldown, float range)
    {
        this.abilityName = abilityName;
        this.damageMultiplier = damageMultiplier;
        this.castTime = castTime;
        this.cooldown = cooldown;
        this.range = range;
    }

    /* ------------- OVERRIDE ------------------- */
    public void Use(Vector3 point)
    {
        UseInit(point);
        InstantiateGameObjects(point);
    }

    public void Use(EnemyController enemy)
    {
        UseInit(enemy.transform.position);
        InstantiateGameObjects(enemy);
        if (damageMultiplier != 0) enemy.ApplyDamage(player.GetHero().GetStats().baseDamage * damageMultiplier);
    }

    protected void UseInit(Vector3 point)
    {
        timeWithCooldown = Time.time + cooldown;
        if (button) button.Use();
        player.transform.LookAt(point);
    }

    protected virtual void InstantiateGameObjects(Vector3 point) { }
    protected virtual void InstantiateGameObjects(EnemyController enemy) { }
    public virtual IEnumerator AbilityCoroutine(EnemyController enemy) { return null; }
    public virtual IEnumerator AbilityCoroutine(Vector3 point) { return null; }
    public virtual void ChangeAnimation(Animator animator) {
        animator.SetBool("Attacking", true);
    }
    /* ---------------------------------------- */

    public HeroAbility SetPlayerController(PlayerController player)
    {
        this.player = player;
        return this;
    }

    public HeroAbility SetUIButton(AbilityButton button)
    {
        this.button = button;
        return this;
    }

    public bool NeedTarget()
    {
        return needTarget;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }
    
    public int GetIndex()
    {
        return index;
    }

    public string GetAbilityName()
    {
        return abilityName;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public bool IsOnCooldown()
    {
        return Time.time < timeWithCooldown;
    }

    public float GetRange()
    {
        return range;
    }

    public bool NeedCoroutine()
    {
        return needCoroutine;
    }

    public float GetCastTime()
    {
        return castTime;
    }
}
