using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

	public List<GameObject> objectList;
	public List<GameObject> objectPrefabList;
	public int currentObject = 0;

	void Start () {
		foreach (Transform child in this.transform) {
			this.objectList.Add (child.gameObject);
		}
	}

	public void MenuLeft() {
		this.objectList [currentObject].SetActive (false);
		this.currentObject--;
		if (this.currentObject < 0) {
			this.currentObject = this.objectList.Count - 1;
		}
		this.objectList [currentObject].SetActive (true);
	}

	public void MenuRight() {
		this.objectList [currentObject].SetActive (false);
		this.currentObject++;
		if (this.currentObject > this.objectList.Count - 1) {
			this.currentObject = 0;
		}
		this.objectList [currentObject].SetActive (true);
	}

	public void SpawnCurrentObject() {
		Instantiate (this.objectPrefabList[this.currentObject], this.objectList[this.currentObject].transform.position, this.objectList[this.currentObject].transform.rotation);
	}
	
	void Update () {
		
	}

}
