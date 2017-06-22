using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour {

	private LevelProgressionManager levelProgressionManager;

	void Start () {
		this.levelProgressionManager = GameObject.Find("Ball").GetComponent<LevelProgressionManager> ();
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("Throwable")) {
			this.gameObject.SetActive (false);
			this.levelProgressionManager.RegisterCollectibleCollected ();
		}
	}

}
