using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {
    
    private static PlayerController player;
    private static GameObject selectionPrefab;
    private static GameObject bloodPrefab;
    private static GameObject hpBarPrefab;

    private UnityEngine.Object selection;
    private HpBarController hpBar;
    private int hp;
    private float radius;

    private void Start () {
        if (!player)
        {
            selectionPrefab = Resources.Load<GameObject>("Prefabs/Selection");
            bloodPrefab = Resources.Load<GameObject>("Prefabs/Blood");
            hpBarPrefab = Resources.Load<GameObject>("Prefabs/2D/HP Bar");
            player = PlayerController.Find();
        }

        hp = 100;
        GameObject hpObject = (GameObject) Instantiate(hpBarPrefab,
                                            CalculateHpBarPosition(),
                                            Quaternion.identity,
                                            GameObject.Find("Canvas").transform);
        hpBar = hpObject.GetComponent<HpBarController>();
        hpBar.SetHpTotal(hp);

        Collider col = GetComponentInChildren<Collider>();
        if (col.GetType() == typeof(SphereCollider))
        {
            radius = ((SphereCollider)col).radius;
        }
        else if (col.GetType() == typeof(BoxCollider))
        {
            radius = ((BoxCollider)col).bounds.extents.x;
        }
        else
        {
            radius = ((CapsuleCollider)col).radius;
        }
    }

    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            player.ChaseAttack(null);
            Destroy(gameObject);
            Destroy(hpBar.gameObject);
        }
    }
	
	private void Update () {
        hpBar.transform.position = CalculateHpBarPosition();
    }

    private Vector3 CalculateHpBarPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0, 30, 0);
    }
    
    public void ApplyDamage(int damage)
    {
        Instantiate(bloodPrefab, 
                    transform.position, 
                    Quaternion.Euler(90, 0, 0),
                    transform);
        hp -= damage;
        hpBar.SetHpCurrent(hp);
    }

    public void Select(bool select)
    {
        if (select && !selection)
        {
            selection = Instantiate(selectionPrefab,
                                    transform.position + new Vector3(0, -0.49f, 0),
                                    Quaternion.Euler(90, 0, 0),
                                    transform);
        }
        else if (!select && selection)
        {
            Destroy(selection);
            selection = null;
        }
    }

    public float GetRadius()
    {
        return radius;
    }
}
