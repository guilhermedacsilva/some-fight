using UnityEngine;
using System.Collections;

public class RandomMover : MonoBehaviour {
	
	void Update () {
        transform.Rotate(0, -0.7f * 50 * Time.deltaTime, 0);
        transform.position += transform.forward * 0.05f * 50 * Time.deltaTime;
    }
}
