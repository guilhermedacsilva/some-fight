using UnityEngine;
using System.Collections;

public class V3 : MonoBehaviour {

	public static bool IsClose(Vector3 v1, Vector3 v2, float closeDistance)
    {
        return Vector3.SqrMagnitude(v1 - v2) <= closeDistance;
    }
}
