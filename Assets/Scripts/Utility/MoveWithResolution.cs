using UnityEngine;
using System.Collections;

public class MoveWithResolution : MonoBehaviour {

    public Vector2 speedPercentScreen;

	void Update () {
        transform.Translate(
            speedPercentScreen.x * Screen.width * Time.deltaTime,
            speedPercentScreen.y * Screen.height * Time.deltaTime, 
            0);
	}
}
