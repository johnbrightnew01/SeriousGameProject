using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

    public static DontDestroy Instance;

	void Awake(){
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
