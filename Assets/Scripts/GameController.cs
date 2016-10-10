using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameController : MonoBehaviour {

    private const float delayBetweenClicks = 0.1f;
    private float timeClickOK;
    private PlayerController player;
    private EnemyController enemyController;
    private int terrainMask = 1 << 8;
    private int enemyMask = 1 << 9;
    private Ray ray;
    private RaycastHit hitInfo;

    private GameObject playerTarget;
    private GameObject newPlayerTarget;
    private GameObject selectionPrefab;

    private GameObject cubePrefab;

    private void Start()
    {
        timeClickOK = Time.time;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cubePrefab = Resources.Load<GameObject>("Prefabs/Cube");
        selectionPrefab = Resources.Load<GameObject>("Prefabs/Selection");
    }
	
	private void FixedUpdate () {
        HandleInput();
        HandleTarget();
        HandleActions();
    }

    private void HandleInput()
    {
        if (Time.time < timeClickOK) return;

        if (Input.GetKey(KeyCode.Mouse0) && IsMouseHit(terrainMask))
        {
            ActionMove();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (IsMouseHit(enemyMask))
            {
                newPlayerTarget = hitInfo.transform.parent.gameObject; // enemy parent
            }
            else
            {
                newPlayerTarget = null;
            }
        }
    }

    private void HandleTarget()
    {
        if (playerTarget == newPlayerTarget) return;

        if (newPlayerTarget == null)
        {
            // deselect
        }
        else
        {
            // todo: calc the position
            Instantiate(selectionPrefab, new Vector3(0, -0.5f, 0), Quaternion.identity, newPlayerTarget.transform);
        }

        playerTarget = newPlayerTarget;
    }

    private void HandleActions()
    {
        
    }

    private bool IsMouseHit(int mask)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hitInfo, 100, mask, QueryTriggerInteraction.Ignore);
    }

    private void ActionMove()
    {
        Instantiate(cubePrefab, hitInfo.point, Quaternion.identity);
        player.MoveTo(hitInfo.point);
        timeClickOK = Time.time + delayBetweenClicks;
    }

    private void ActionHit()
    {
        enemyController = hitInfo.transform.parent.GetComponent<EnemyController>();
        enemyController.ApplyHitByPlayer(player);
        player.HitEnemy();
        timeClickOK = Time.time + delayBetweenClicks;
    }
}
