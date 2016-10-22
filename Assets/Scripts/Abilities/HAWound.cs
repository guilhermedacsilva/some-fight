using System;
using System.Collections;
using UnityEngine;

public class HAWound : HeroAbility
{
    public HAWound() : base("Wound", 1, 0.7f, 5, 1) {
        needCoroutine = true;
    }

    public override IEnumerator AbilityCoroutine(EnemyController target)
    {
        int count = 0;
        while (target != null && count < 3)
        {
            count++;
            target.ApplyDamage(player.GetHero().GetStats().baseDamage);
            yield return new WaitForSeconds(1);
        }
    }
}
