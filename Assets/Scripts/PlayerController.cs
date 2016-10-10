using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Vector3 destination;
    private Vector3 newPosition;
    private Rigidbody rb;
    private float moveSpeed = .1f;
    private float hitTimeOK = 0;
    private float hitDelay = 1;

	private void Start () {
        rb = GetComponent<Rigidbody>();
        destination = rb.position;
        newPosition = rb.position;
    }
	
	private void FixedUpdate () {
        if (rb.position != destination)
        {
            ApplyMove();
        }
    }

    private void ApplyMove()
    {
        newPosition = Vector3.MoveTowards(rb.position, destination, moveSpeed);
        rb.position = newPosition;
    }

    private void Update()
    {
        // verify if it is stucked on a wall
        if (rb.position != newPosition)
        {
            destination = rb.position;
            newPosition = rb.position;
        }
    }

    public void MoveTo(Vector3 point)
    {
        destination = point;
        destination.y = rb.position.y;
        transform.LookAt(destination);
    }

    public bool CanHit()
    {
        return Time.time >= hitTimeOK;
    }

    public void HitEnemy()
    {
        hitTimeOK = Time.time + hitDelay;
    }
}
