using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameController : MonoBehaviour {
    
    private PlayerController player;
    private EnemyController enemy;
    private int terrainMask = 1 << 8;
    private int enemyMask = 1 << 9;
    private Ray ray;
    private RaycastHit hitInfo;
    private float selectTimeOK;
    private float selectDelayTime = 0.25f;

    private void Start()
    {
        player = PlayerController.Find();
        selectTimeOK = Time.time;
    }
	
	private void FixedUpdate () {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Mouse0) && HasMouseHitTerrain())
        {
            player.MoveTo(hitInfo.point);
        }
        else if (Input.GetKey(KeyCode.Mouse1) && Time.time > selectTimeOK)
        {
            if (HasMouseHitEnemy())
            {
                // the controller is on parent
                enemy = hitInfo.transform.parent.GetComponent<EnemyController>();
            }
            else
            {
                enemy = null;
            }
            player.ChaseAttack(enemy);
            selectTimeOK = Time.time + selectDelayTime;
        }
    }

    private bool HasMouseHitTerrain()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hitInfo, 100, terrainMask, QueryTriggerInteraction.Ignore);
    }

    private bool HasMouseHitEnemy()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.SphereCast(ray, 0.5f, out hitInfo, 100, enemyMask, QueryTriggerInteraction.Ignore);
    }
}
