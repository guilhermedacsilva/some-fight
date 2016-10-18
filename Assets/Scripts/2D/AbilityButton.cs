using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class AbilityButton : MonoBehaviour {

    private static Color COLOR_RED = new Color(1, 137.0f / 255, 137.0f / 255);
    private RectTransform rectRed;
    private RawImage smallButtonImg;
    private float cooldown;
    private float timeOK = 0;

    public void Init(string name, string hotkey, float cooldown)
    {
        this.cooldown = cooldown;
        transform.Find("Big Button/Name").GetComponent<Text>().text = name;
        transform.Find("Small Button/Hotkey").GetComponent<Text>().text = hotkey;
        smallButtonImg = transform.Find("Small Button").GetComponent<RawImage>();
        rectRed = transform.Find("Big Button/Red").GetComponent<RectTransform>();
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
}
