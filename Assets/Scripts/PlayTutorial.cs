using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayTutorial : MonoBehaviour {

	void Start () {
		int firstDelaySeconds = 3;
		int textShownForSeconds = 15;
		int i = 0;
		foreach (Transform tutorialTextTransform in this.transform) {
			TextMeshPro text = tutorialTextTransform.GetComponent<TextMeshPro> ();

			StartCoroutine (ShowTutorialText (text, firstDelaySeconds + textShownForSeconds * i + 1, textShownForSeconds));

			i++;
		}
	}

	IEnumerator ShowTutorialText(TextMeshPro text, int showAfterSeconds, int textShownForSeconds) {
		yield return new WaitForSeconds (showAfterSeconds);

		text.gameObject.SetActive (true);

		yield return new WaitForSeconds (textShownForSeconds);

		text.gameObject.SetActive (false);
	}
	
}
