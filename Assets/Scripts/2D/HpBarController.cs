using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpBarController : MonoBehaviour {

    private Transform targetUnit;
    private Vector3 offset;
    private int total;
    private float maxWidth;

    private RectTransform redBar;

    private void Start()
    {
        redBar = (RectTransform) transform.Find("RedBar");
        maxWidth = redBar.rect.width;
        offset = new Vector3(0, 0.1f * Screen.height, 0);
    }

    public HpBarController Prepare(Transform targetUnit, int total)
    {
        this.total = total;
        this.targetUnit = targetUnit;
        return this;
    }

    public void SetHpCurrent(int current)
    {
        redBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth * current / total);
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(targetUnit.position) + offset;
    }

}
