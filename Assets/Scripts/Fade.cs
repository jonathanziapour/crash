using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	public float fadeInTime;
	public float fadeOutTime;
	public float waitToFadeOutTime;

	void Start(){
		StartCoroutine (FadeIn(fadeInTime));
		StartCoroutine (WaitToFadeOut (waitToFadeOutTime));
	}

	IEnumerator FadeOut(float fadeTime) {
		for (float f = 0.05f; f >= 0; f -= Time.deltaTime / fadeTime) {
			Color c = this.GetComponent<Renderer> ().material.color;
			c.a = f;
			this.GetComponent<Renderer> ().material.color = c;
			yield return null;
		}

		Destroy (this.transform.parent.gameObject);
	}

	IEnumerator FadeIn(float fadeTime) {
		for (float f = 0f; f <= 0.05f; f += Time.deltaTime/fadeTime) {
			Color c = this.GetComponent<Renderer>().material.color;
			c.a = f;
			this.GetComponent<Renderer>().material.color = c;
			yield return null;
		}
	}

	IEnumerator WaitToFadeOut(float waitTime){
		yield return new WaitForSeconds(waitTime);
		StartCoroutine (FadeOut (fadeOutTime));
	}
}
