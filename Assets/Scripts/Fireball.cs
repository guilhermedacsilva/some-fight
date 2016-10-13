using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        rb.MovePosition(rb.position + transform.forward * 0.25f);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().ApplyDamage(50);
            Destroy(gameObject);
        }
    }
}
