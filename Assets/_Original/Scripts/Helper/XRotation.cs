using UnityEngine;
using System.Collections;

public class XRotation : MonoBehaviour {

	public float rotationSpeed = 20;

	void Update () {
		gameObject.transform.Rotate (Vector3.right * rotationSpeed * Time.deltaTime,Space.Self);
	}
}
