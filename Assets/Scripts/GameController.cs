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
    //private GameObject cubePrefab;

    private void Start()
    {
        player = PlayerController.Find();
        selectTimeOK = Time.time;
        //cubePrefab = Resources.Load<GameObject>("Prefabs/Cube");
    }
	
	private void FixedUpdate () {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Mouse0) && HasMouseHit(terrainMask))
        {
            player.MoveTo(hitInfo.point);
        }
        else if (Input.GetKey(KeyCode.Mouse1) && Time.time > selectTimeOK)
        {
            if (HasMouseHit(enemyMask))
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

    private bool HasMouseHit(int mask)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hitInfo, 100, mask, QueryTriggerInteraction.Ignore);
    }

    /*

    private void HandleTarget()
    {
        if (target == newPlayerTarget) return;

        if (newPlayerTarget == null)
        {
            DeselectTarget();
        }
        else // SelectTarget
        {
            enemy = newPlayerTarget.GetComponent<EnemyController>();
            selection = Instantiate(selectionPrefab,
                                    newPlayerTarget.transform.position + new Vector3(0, -0.49f, 0),
                                    Quaternion.Euler(90, 0, 0),
                                    newPlayerTarget.transform);
        }

        target = newPlayerTarget;
    }

    private void DeselectTarget()
    {
        if (selection != null)
        {
            Destroy(selection);
        }
        selection = null;
        target = null;
        newPlayerTarget = null;
    }

    private void HandleActions()
    {
        if (hasInputForMove)
        {
            //Instantiate(cubePrefab, moveDestination, Quaternion.identity);
            
            hasInputForMove = false;
        }
        else if (selection != null)
        {
            if (player.CanHit(enemy))
            {
                player.HitEnemy(enemy);
            }
            else
            {
                player.MoveTo(enemy.transform);
            }
        }
    }

    */
}
