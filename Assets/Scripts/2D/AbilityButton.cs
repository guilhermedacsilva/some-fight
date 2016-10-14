using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class AbilityButton : MonoBehaviour {

    private RectTransform rectRed;
    private float cooldown;
    private float timeOK = 0;

    public void Init(string name, string hotkey, float cooldown)
    {
        this.cooldown = cooldown;
        transform.Find("Big Button/Name").GetComponent<Text>().text = name;
        transform.Find("Small Button/Hotkey").GetComponent<Text>().text = hotkey;
        rectRed = transform.Find("Big Button/White").GetComponent<RectTransform>();
    }

    public bool CanUse()
    {
        return Time.time > timeOK;
    }

    public void Use()
    {
        timeOK = Time.time + cooldown;
        StartCoroutine(UpdateCooldownAnimation());
    }

    private IEnumerator UpdateCooldownAnimation()
    {
        while (!CanUse())
        {
            rectRed.localScale = new Vector3(1, Mathf.Clamp((timeOK - Time.time) / cooldown, 0, 1));
            yield return new WaitForFixedUpdate();
        }
        rectRed.localScale = new Vector3(1, 0);
    }
}
