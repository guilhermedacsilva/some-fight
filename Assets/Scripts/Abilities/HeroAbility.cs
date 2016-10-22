using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HeroAbility
{
    protected PlayerController player;
    protected int index;
    protected AbilityButton button;

    protected string abilityName;
    protected float cooldown;
    protected float timeWithCooldown = 0;
    protected bool needTarget;
    protected float range;

    public HeroAbility(string abilityName, float cooldown, float range)
    {
        this.abilityName = abilityName;
        this.cooldown = cooldown;
        this.range = range;
        needTarget = true;
    }

    /* ------------- OVERRIDE ------------------- */
    public void InstantiateGameObjects() { }
    
    public void Use(Vector3 point)
    {
        timeWithCooldown = Time.time + cooldown;
        if (button) button.Use();
        player.transform.LookAt(point);
        InstantiateGameObjects();
    }

    public void Use(EnemyController enemy)
    {
        Use(enemy.transform.position);
    }
    /* ---------------------------------------- */

    public HeroAbility SetPlayerController(PlayerController player)
    {
        this.player = player;
        return this;
    }

    public HeroAbility SetNeedTarget(bool needTarget)
    {
        this.needTarget = needTarget;
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
        return Time.time > timeWithCooldown;
    }

    public float GetRange()
    {
        return range;
    }
}
