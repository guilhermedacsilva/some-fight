using UnityEngine;
using System.Collections;

public class HeroStats
{

    public int hp;
    public int mp;
    public int baseDamage;
    public float moveSpeed;

    public HeroStats(
        int hp, int mp, 
        int baseDamage, float moveSpeed) {

        this.hp = hp;
        this.mp = mp;
        this.baseDamage = baseDamage;
        this.moveSpeed = moveSpeed;
    }

}
