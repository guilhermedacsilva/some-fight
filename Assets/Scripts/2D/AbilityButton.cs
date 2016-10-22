using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class AbilityButton : MonoBehaviour {

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Init(string name, string hotkey, float cooldown)
    {
        animator.speed = 1 / cooldown;
        transform.Find("Big Button/Name").GetComponent<Text>().text = name;
        transform.Find("Small Button/Hotkey").GetComponent<Text>().text = hotkey;
    }

    public bool IsOnCooldown()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Base.cooldown");
    }

    public void Use()
    {
        animator.SetTrigger("cooldown");
    }
}
