using UnityEngine;
using System.Collections;

public class HeroStats
{

    public int hp;
    public int mp;
    public int baseDamage;
    public float attackRange;
    public float attackCooldown;
    public float moveSpeed;

    public HeroStats(
        int hp, int mp, int baseDamage, float attackRange, 
        float attackCooldown, float moveSpeed) {

        this.hp = hp;
        this.mp = mp;
        this.baseDamage = baseDamage;
        this.attackRange = attackRange;
        this.attackCooldown = attackCooldown;
        this.moveSpeed = moveSpeed;
    }

}
