using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameController : MonoBehaviour {

    private const float delayBetweenClicks = 0.1f;
    private float timeClickOK;
    private PlayerController playerController;
    private EnemyController enemyController;
    private int terrainMask = 1 << 8;
    private int enemyMask = 1 << 9;
    private Ray ray;
    private RaycastHit hitInfo;

    private GameObject cube;

    private void Start()
    {
        timeClickOK = Time.time;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        cube = Resources.Load<GameObject>("Prefabs/Cube");
    }
	
	private void FixedUpdate () {
        if (Time.time < timeClickOK) return;

        if (Input.GetKey(KeyCode.Mouse0) && IsMouseHit(terrainMask))
        {
            ActionMove();
        }
        if (Input.GetKey(KeyCode.Mouse1) && IsMouseHit(enemyMask))
        {
            ActionHit();
        }
    }

    private bool IsMouseHit(int mask)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hitInfo, 100, mask, QueryTriggerInteraction.Ignore);
    }

    private void ActionMove()
    {
        Instantiate(cube, hitInfo.point, Quaternion.identity);
        playerController.moveTo(hitInfo.point);
        timeClickOK = Time.time + delayBetweenClicks;
    }

    private void ActionHit()
    {
        enemyController = hitInfo.transform.parent.GetComponent<EnemyController>();
        enemyController.ApplyHitByPlayer(playerController);
        timeClickOK = Time.time + delayBetweenClicks;
    }
}
