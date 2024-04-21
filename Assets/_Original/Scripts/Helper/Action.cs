/*
 * Created by Mahmud
 * Bitmascot
 * */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Actions : MonoBehaviour {
    public static IEnumerator MoveWithFixedTime(GameObject gameObject, Vector3 position, float duration) {
        Vector3 startPosition = gameObject.transform.localPosition;
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.fixedDeltaTime / duration;
			gameObject.transform.localPosition = Vector3.Lerp(startPosition, position, t);
            yield return null;
        }
    }

    public static IEnumerator Move(GameObject gameObject, Vector3 position, float duration) {
        Vector3 startPosition = gameObject.transform.localPosition;
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime / duration;
            gameObject.transform.localPosition = Vector3.Lerp(startPosition, position, t);
            yield return null;
        }
    }

    public static IEnumerator Move(Transform trans, Vector3 position, float duration) {
        Vector3 startPosition = trans.localPosition;
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime / duration;
            trans.localPosition = Vector3.Lerp(startPosition, position, t);
            yield return null;
        }
    }

    //Fade
    public static IEnumerator FadeTo(GameObject obj, float aValue, float aTime) {
		Color c = obj.GetComponent<SpriteRenderer> ().color;
        float alpha = obj.GetComponent<SpriteRenderer>().color.a;       
		float t = 0.0f;
		while (t < 1.0f) {
			t += Time.deltaTime / aTime;
			Color newColor = new Color(c.r, c.g, c.b, Mathf.Lerp(alpha, aValue, t));
			obj.GetComponent<SpriteRenderer>().color = newColor;
			yield return null;
		}
    }

    public static IEnumerator FadeTo(MeshRenderer mr, float aValue, float aTime) {
        Color startColor = mr.material.color;
        Color targetColor = startColor;
        targetColor.a = aValue;

        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime / aTime;
            mr.material.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
    }

    public static IEnumerator FadeTo(SpriteRenderer sr, float aValue, float aTime) {
		Color startColor = sr.color;
		Color targetColor = startColor;
		targetColor.a = aValue;

		float t = 0.0f;
		while (t < 1.0f) {
			t += Time.deltaTime / aTime;
			sr.color = Color.Lerp(startColor,targetColor,t);
			yield return null;
		}
	}
    public static IEnumerator FadeTo(ParticleSystem obj, float aValue, float aTime) {
		float t = 0;

		Color startColor = obj.startColor;
		Color targetColor = startColor;

		targetColor.a = aValue;

		while (t < 1.0f) {
			t += Time.deltaTime / aTime;
			obj.startColor = Color.Lerp (startColor,targetColor,t);
			yield return null;
		}
    }
	public static IEnumerator FadeTo(Text text,float aValue, float duration){
		float t = 0;

		Color startColor = text.color;
		Color targetColor = startColor;

		targetColor.a = aValue;

		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			text.color = Color.Lerp (startColor,targetColor,t);
			yield return null;
		}
	}

    public static IEnumerator FadeTo(TextMesh text, float aValue, float duration) {
        float t = 0;

        Color startColor = text.color;
        Color targetColor = startColor;

        targetColor.a = aValue;

        while (t < 1.0f) {
            t += Time.deltaTime / duration;
            text.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
    }

    public static IEnumerator FadeTo(Image image,float aValue, float duration){
		
		float t = 0;

		Color startColor = image.color;
		Color targetColor = startColor;

		targetColor.a = aValue;

		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			image.color = Color.Lerp (startColor,targetColor,t);
			yield return null;
		}

	}


    public static IEnumerator ZoomTo(GameObject gameObject, Vector3 scale, float duration) {
		Vector3 startScale = gameObject.transform.localScale;
		float t = 0.0f;
		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			gameObject.transform.localScale = Vector3.Lerp(startScale, scale, t);
			yield return null;
		}
	}

	public static IEnumerator ChangeColor(Image g,Color targetColor,float duration){
		float t = 0.0f;
		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			g.color = Color.Lerp (g.color , targetColor, t);
			yield return null;
		}
	}

	public static IEnumerator ChangeColor(GameObject obj, Color targetColor, float aTime) {
		Color c = obj.GetComponent<SpriteRenderer> ().color;
		float alpha = obj.GetComponent<SpriteRenderer>().color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {			 
			obj.GetComponent<SpriteRenderer>().color = Color.Lerp(obj.GetComponent<SpriteRenderer>().color,targetColor,t);
			yield return null;
		}
	}

	//Rotation
    public static void RotateAgainstTarget(GameObject obj, GameObject target) {
        var newRotation = Quaternion.LookRotation(obj.transform.position + target.transform.position, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, newRotation, Time.deltaTime * 8);
    }

	public static IEnumerator RotateTo(GameObject obj,Vector3 target,float duration){
		Quaternion targetRotation = Quaternion.Euler(target);
		float t = 0.0f;
		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			obj.transform.localRotation = Quaternion.Slerp(obj.transform.localRotation,targetRotation,t);
			yield return null;
		}
	}

	public static IEnumerator RotateTo(GameObject obj,Quaternion target,float duration){
		Quaternion startRotation = obj.transform.rotation;
		Quaternion targetRotation = target;
		float t = 0.0f;
		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			obj.transform.rotation = Quaternion.Slerp(startRotation,targetRotation,t);
			yield return null;
		}
	}

	public static IEnumerator RotateBy(GameObject obj,Vector3 target,float duration){

	//	obj.transform.Rotate(target * Time.deltaTime);
		Quaternion startRotation = obj.transform.rotation;
		Quaternion targetRotation = Quaternion.Euler(new Vector3(startRotation.x + target.x,startRotation.y+target.y,startRotation.z+target.z));
		float t = 0.0f;
		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			obj.transform.rotation = Quaternion.Slerp(startRotation,targetRotation,t);

			yield return null;
		}
	}

	public static IEnumerator RotateAlways(GameObject g,Vector3 rotateDirection,float speed){



		while (true) {
			g.transform.Rotate(rotateDirection,speed * Time.deltaTime);
			yield return true;
		}
	}

	//Shake


	public static IEnumerator Jump(Transform t,Vector3 startPosition,Vector3 endPosition,float jumpDuration,float jumpHeight){
		float dt = 0;
		while (dt < 1.0f) {
			dt += Time.deltaTime / jumpDuration;
			Vector3 height = Vector3.up * Mathf.Sin (Mathf.PI * dt);
			height.y = (height.y < 0) ? 0.0f : height.y;
			t.localPosition = (Vector3.Lerp (startPosition, endPosition, dt) + height * jumpHeight);
			yield return null;
		}
	}

    public static IEnumerator JumpTo(Transform t, Vector3 startPosition, Vector3 endPosition, float jumpDuration, float jumpHeight) {
        float dt = 0;
        while (dt < 1.0f) {
            dt += Time.deltaTime / jumpDuration;
            Vector3 height = Vector3.up * Mathf.Sin(Mathf.PI * dt);
            height.y = (height.y < 0) ? 0.0f : height.y;
            t.localPosition = (Vector3.Lerp(startPosition, endPosition, dt) + height * jumpHeight);
            yield return null;
        }
    }

}
