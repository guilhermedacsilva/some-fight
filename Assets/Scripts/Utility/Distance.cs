using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Distance
{
    public static bool IsFar(Vector3 p1, Vector3 p2, float distance)
    {
        return Vector3.Distance(p1, p2) > distance;
    }

    public static bool IsEnemyFar(PlayerController player, EnemyController enemy, float distance)
    {
        return IsFar(
            player.transform.position,
            enemy.transform.position,
            distance + enemy.GetRadius());
    }

    public static bool IsPointFar(PlayerController player, Vector3 point, float distance)
    {
        return IsFar(player.transform.position, point, distance);
    }
}
