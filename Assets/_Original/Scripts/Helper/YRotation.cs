using UnityEngine;
using System.Collections;

public class YRotation : MonoBehaviour {

	public float rotationSpeed = 20;
	public bool isLocal = true;

	void Update () {
        if (isLocal)
        {
			gameObject.transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime,Space.Self);
        }
        else
        {
            gameObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }

	}
}
