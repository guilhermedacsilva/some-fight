using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Hero
{
    // Order: Mouse, Q, W, E, R
    private List<HeroAbility> abilities = new List<HeroAbility>();
    private HeroStats stats;

    public void SetStats(HeroStats stats)
    {
        this.stats = stats;
    }

    public void AddAbility(HeroAbility ability)
    {
        ability.SetIndex(abilities.Count);
        abilities.Add(ability);
    }

    public HeroStats GetStats()
    {
        return stats;
    }

    public int CountAbilities()
    {
        return abilities.Count;
    }

    public void SetPlayer(PlayerController player)
    {
        foreach (HeroAbility ability in abilities)
        {
            ability.SetPlayerController(player);
        }
    }

    public HeroAbility GetAbility(int index)
    {
        return abilities[index];
    }


}