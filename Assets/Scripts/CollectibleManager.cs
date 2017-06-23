using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour {

	private float speed = 200f;

	private LevelProgressionManager levelProgressionManager;

	void Start () {
		this.levelProgressionManager = GameObject.Find("Ball").GetComponent<LevelProgressionManager> ();
	}

	void Update() {
		this.transform.Rotate (Vector3.up, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("Throwable")) {
			this.gameObject.SetActive (false);
			this.levelProgressionManager.RegisterCollectibleCollected ();
		}
	}

}
