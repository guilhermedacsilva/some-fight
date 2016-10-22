using System;
using System.Collections;
using UnityEngine;

public class HAFireball : HeroAbility
{
    private static GameObject fireballPrefab;

    public HAFireball() : base("Fireball", 0, 0, 1, 100) {
        needTarget = false;
    }

    protected override void InstantiateGameObjects(Vector3 point)
    {
        if (!fireballPrefab)
        {
            fireballPrefab = Resources.Load<GameObject>("Prefabs/Fireball");
        }
        GameObject obj = (GameObject) UE.Instantiate(fireballPrefab,
                                new Vector3(player.transform.position.x, 0.5f, player.transform.position.z),
                                player.transform.rotation);

        obj.GetComponent<Fireball>().SetDamage(player.GetHero().GetStats().baseDamage * 2);
    }
}
