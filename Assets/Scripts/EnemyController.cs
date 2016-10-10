using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {

    public Text textObject;
    private int hp;
    private static PlayerController player;
    private static GameObject selectionPrefab;
    private UnityEngine.Object selection;

    private GameObject blood;

	void Start () {
        hp = 100;
        blood = Resources.Load<GameObject>("Prefabs/Blood");
        if (!selectionPrefab)
        {
            selectionPrefab = Resources.Load<GameObject>("Prefabs/Selection");
            player = PlayerController.Find();
        }
    }
	
	void Update () {
        textObject.text = "Enemy HP: " + hp + "%";
	}
    
    public void ApplyDamage(int damage)
    {
        Instantiate(blood, 
                    transform.position, 
                    Quaternion.Euler(90, 0, 0),
                    transform);
        hp -= damage;
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
}
