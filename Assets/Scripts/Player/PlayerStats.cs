using UnityEngine;
using System.Collections;

public class PlayerStats {

    public int hp;
    public int mp;
    public int baseDamage;
    public float hitDistance;
    public float hitCooldown;
    public float moveSpeed;

    private PlayerStats(
        int hp, int mp, int baseDamage, float hitDistance, float hitCooldown, float moveSpeed) {

        this.hp = hp;
        this.mp = mp;
        this.baseDamage = baseDamage;
        this.hitDistance = hitDistance;
        this.hitCooldown = hitCooldown;
        this.moveSpeed = moveSpeed;
    }

    public static PlayerStats Create(int index)
    {
        PlayerStats stats = null;
        switch(index)
        {
            case 0:
                stats = new PlayerStats(500, 100, 30, 1f, 1f, 5f);
                break;
        }
        return stats;
    }

}
