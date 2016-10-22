using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    private Rigidbody rb;
    private int damage;

    private void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void FixedUpdate () {
        rb.MovePosition(rb.position + transform.forward * 0.25f);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().ApplyDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
