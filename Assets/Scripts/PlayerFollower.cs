using UnityEngine;
using System.Collections;

public class PlayerFollower : MonoBehaviour {

    private Transform target;    
    private const float offsetZ = -4;

    private void Start()
    {
        target = PlayerController.Find().transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, 6, target.position.z + offsetZ);
    }

    /*
     * public float smoothTime = 0.3f;
     * private Vector3 velocity = Vector3.zero;
     * 
         * Smooth
        Vector3 destination = new Vector3(
            target.position.x,
            6,
            target.position.z + offsetZ);

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);*/

}
