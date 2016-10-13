using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {
    
    private static PlayerController player;
    private static GameObject selectionPrefab;
    private static GameObject bloodPrefab;
    private static GameObject hpBarPrefab;

    private Rigidbody rb;
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

        rb = GetComponent<Rigidbody>();

        hp = 100;
        GameObject hpObject = (GameObject) Instantiate(hpBarPrefab,
                                            transform.position,
                                            Quaternion.identity,
                                            GameObject.Find("Canvas").transform);

        hpBar = hpObject.GetComponent<HpBarController>().Prepare(transform, hp);

        CalculateRadius();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            player.ChaseAttack(null);
            Destroy(gameObject);
            Destroy(hpBar.gameObject);
        }
    }
    
    public void ApplyDamage(int damage)
    {
        hp -= damage;
        hpBar.SetHpCurrent(hp);
        Invoke("CreateBlood", 0.3f);
    }

    private void CreateBlood()
    {
        if (hp > 0)
        {
            Instantiate(bloodPrefab,
                        transform.position + new Vector3(0, 0.5f, 0),
                        Quaternion.Euler(90, 0, 0),
                        transform);
        }
    }

    public void Select(bool select)
    {
        if (select && !selection)
        {
            selection = Instantiate(selectionPrefab,
                                    transform.position,
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

    private void CalculateRadius()
    {
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
}
