using UnityEngine;
using System.Collections;

public class ZRotation : MonoBehaviour {

	public float rotationSpeed = 20;

    public bool isSelf;

	void Update () {
		gameObject.transform.Rotate (Vector3.forward * rotationSpeed * Time.deltaTime,(isSelf) ? Space.Self : Space.World);
	}
}
