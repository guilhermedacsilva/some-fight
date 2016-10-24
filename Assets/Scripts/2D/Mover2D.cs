using UnityEngine;
using System.Collections;

public class Mover2D : MonoBehaviour {

    public Transform follow3d;
    public Vector2 speedPercentScreen;
    private Vector3 currentOffset = new Vector3();
    private Vector3 targetDeadPosition = Vector3.zero;

    public void SetTarget(Transform target)
    {
        follow3d = target;
        targetDeadPosition = target.position;
    }

    public void SetPosition(Vector3 position)
    {
        currentOffset = position;
    }

	private void Update () {
        currentOffset = currentOffset + 
                            new Vector3(speedPercentScreen.x * Screen.width * Time.deltaTime,
                                        speedPercentScreen.y * Screen.height * Time.deltaTime,
                                        0);

        if (follow3d)
        {
            if (follow3d.gameObject.activeInHierarchy)
            {
                targetDeadPosition = follow3d.position;
            }
            transform.position = Camera.main.WorldToScreenPoint(targetDeadPosition) + currentOffset;
        }
        else if (targetDeadPosition.Equals(Vector3.zero))
        {
            transform.position = currentOffset;
        }
        else
        {
            transform.position = Camera.main.WorldToScreenPoint(targetDeadPosition) + currentOffset;
        }
	}
}
