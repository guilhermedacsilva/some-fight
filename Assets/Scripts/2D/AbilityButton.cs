using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class AbilityButton : MonoBehaviour {

    private static GameObject buttonPrefab;
    private static Color COLOR_RED = new Color(1, 137.0f / 255, 137.0f / 255);

    private RectTransform rectRed;
    private RawImage smallButtonImg;
    private float cooldown;
    private float timeOK = 0;

    public AbilityButton Init(HeroAbility heroAbility)
    {
        cooldown = heroAbility.GetCooldown();
        transform.Find("Big Button/Name").GetComponent<Text>().text = 
            heroAbility.GetAbilityName();
        transform.Find("Small Button/Hotkey").GetComponent<Text>().text = 
            Hotkey.ConvertIndexToHotkey(heroAbility.GetIndex());
        smallButtonImg = transform.Find("Small Button").GetComponent<RawImage>();
        rectRed = transform.Find("Big Button/Red").GetComponent<RectTransform>();
        return this;
    }

    public bool IsOnCooldown()
    {
        return Time.time <= timeOK;
    }

    public void Use()
    {
        timeOK = Time.time + cooldown;
        StartCoroutine(UpdateCooldownAnimation());
    }

    private IEnumerator UpdateCooldownAnimation()
    {
        smallButtonImg.color = COLOR_RED;
        while (IsOnCooldown())
        {
            rectRed.localScale = new Vector3(1, Mathf.Clamp((timeOK - Time.time) / -cooldown, -1, 0));
            
            yield return new WaitForFixedUpdate();
        }
        smallButtonImg.color = Color.white;
        rectRed.localScale = new Vector3(1, 0);
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
