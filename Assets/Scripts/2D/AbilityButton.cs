﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class AbilityButton : MonoBehaviour {

    private Animator animator;
    private static GameObject buttonPrefab;
    private float cooldown;

    public AbilityButton Init(HeroAbility heroAbility)
    {
        cooldown = heroAbility.GetCooldown();
        transform.Find("Big Button/Name").GetComponent<Text>().text = 
            heroAbility.GetAbilityName();
        transform.Find("Small Button/Hotkey").GetComponent<Text>().text = 
            Hotkey.ConvertIndexToHotkey(heroAbility.GetIndex());
        animator.speed = 1 / cooldown;
        return this;
    }

    public void Use()
    {
        animator.SetTrigger("cooldown");
    }

    public static void CreateButtonsForHero(Hero hero)
    {
        if (!buttonPrefab)
        {
            buttonPrefab = Resources.Load<GameObject>("Prefabs/2D/Ability Button");
        }
        
        HeroAbility ability;
        for (int index = 0; index < hero.CountAbilities(); index++)
        {
            ability = hero.GetAbility(index);

            GameObject obj = (GameObject)Instantiate(
                                        buttonPrefab,
                                        Vector3.zero,
                                        Quaternion.identity,
                                        GameObject.Find("Canvas").transform);


            obj.GetComponent<RectTransform>().anchoredPosition = 
                                                    new Vector3(-100 + 70 * index,0,0);
            obj.name = "Button " + ability.GetAbilityName();
            AbilityButton button = obj.GetComponent<AbilityButton>().Init(ability);

            ability.SetUIButton(button);
        }
    }
}
