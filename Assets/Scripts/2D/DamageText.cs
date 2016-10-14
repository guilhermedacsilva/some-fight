using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DamageText : MonoBehaviour {

    private static GameObject damagePrefab;
    private static Transform canvasTransform;

    private static GameObject GetPrefab()
    {
        if (!damagePrefab)
        {
            damagePrefab = Resources.Load<GameObject>("Prefabs/2D/Damage Text");
        }
        return damagePrefab;
    }

    private static Transform GetCanvas()
    {
        if (!canvasTransform)
        {
            canvasTransform = GameObject.Find("Canvas").transform;
        }
        return canvasTransform;
    }

    internal static void Create(Transform target, int damage)
    {
        GameObject gameObject = (GameObject)Instantiate(GetPrefab(),
                                                    Camera.main.WorldToScreenPoint(target.position) + new Vector3(0, 1f, 0),
                                                    Quaternion.identity,
                                                    GetCanvas());

        gameObject.GetComponent<Text>().text = damage.ToString();
        gameObject.transform.GetChild(0).GetComponent<Text>().text = damage.ToString();
        gameObject.GetComponent<Mover2D>().SetTarget(target);
    }

}
