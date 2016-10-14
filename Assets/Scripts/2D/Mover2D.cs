using UnityEngine;
using System.Collections;

public class Mover2D : MonoBehaviour {

    public Transform follow3d;
    public Vector2 speedPercentScreen;
    private Vector3 currentOffset = new Vector3();

    public void SetTarget(Transform target)
    {
        follow3d = target;
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
            transform.position = Camera.main.WorldToScreenPoint(follow3d.position) + currentOffset;
        }
        else
        {
            transform.position = currentOffset;
        }
	}
}
