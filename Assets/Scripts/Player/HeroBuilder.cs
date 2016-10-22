using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HeroBuilder
{
    public static Hero Build(int index, PlayerController player = null)
    {
        Hero hero = new Hero();
        switch (index)
        {
            case 0:
                hero.SetStats(new HeroStats(500, 100, 30, 5f));
                hero.AddAbility(new HAAttackMelee());
                hero.AddAbility(new HAFireball());
                hero.AddAbility(new HAWound());
                break;
        }

        hero.SetPlayer(player);
        return hero;
    }

}
