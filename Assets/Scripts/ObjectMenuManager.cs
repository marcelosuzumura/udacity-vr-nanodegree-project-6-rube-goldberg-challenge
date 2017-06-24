using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour {

	public List<GameObject> objectList;
	public List<GameObject> objectPrefabList;
	public int[] objectPrefabCountList;
	public int currentObject = 0;

	private List<string> objectOriginalTextList = new List<string>();
	private List<Text> objectTextList = new List<Text>();

	void Start () {
		int i = 0;
		foreach (Transform child in this.transform) {
			this.objectList.Add (child.gameObject);

			Text objectText = child.GetComponentInChildren<Text> ();
			this.objectOriginalTextList.Add (objectText.text);
			this.objectTextList.Add (objectText);
			this.updateCurrentObjectCountText (i);
			i++;
		}
	}

	void updateCurrentObjectCountText(int currentObject) {
		this.objectTextList[currentObject].text = this.objectOriginalTextList[currentObject] + " (" + this.objectPrefabCountList[currentObject] + " remaining)";
	}

	public void MenuEnable() {
		this.objectList [this.currentObject].SetActive (true);
	}

	public void MenuDisable() {
		this.objectList [this.currentObject].SetActive (false);
	}

	public void MenuLeft() {
		this.objectList [this.currentObject].SetActive (false);
		this.currentObject--;
		if (this.currentObject < 0) {
			this.currentObject = this.objectList.Count - 1;
		}
		this.objectList [this.currentObject].SetActive (true);
	}

	public void MenuRight() {
		this.objectList [this.currentObject].SetActive (false);
		this.currentObject++;
		if (this.currentObject > this.objectList.Count - 1) {
			this.currentObject = 0;
		}
		this.objectList [this.currentObject].SetActive (true);
	}

	public void SpawnCurrentObject() {
		if (this.objectPrefabCountList [this.currentObject] > 0) {
			this.objectPrefabCountList [this.currentObject]--;

			this.updateCurrentObjectCountText (this.currentObject);

			Instantiate (this.objectPrefabList[this.currentObject], this.objectList[this.currentObject].transform.position, this.objectList[this.currentObject].transform.rotation);
		}
	}
	
}
