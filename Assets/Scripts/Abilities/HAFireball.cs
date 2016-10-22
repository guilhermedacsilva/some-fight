using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HAFireball : HeroAbility
{
    private static GameObject fireballPrefab;

    public HAFireball() : base("Fireball", 1, 100) {
        needTarget = false;
    }

    public new void InstantiateGameObjects()
    {
        if (!fireballPrefab)
        {
            fireballPrefab = Resources.Load<GameObject>("Prefabs/Fireball");
        }
        UE.Instantiate(fireballPrefab,
                    new Vector3(player.transform.position.x, 0.5f, player.transform.position.z),
                    player.transform.rotation);
    }
}
