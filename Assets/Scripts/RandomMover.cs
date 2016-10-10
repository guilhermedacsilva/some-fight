using UnityEngine;
using System.Collections;

public class RandomMover : MonoBehaviour {
	
	void FixedUpdate () {
        transform.Rotate(0, -0.5f, 0);
        transform.position += transform.forward * 0.05f;
	}
}
