using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCheatManager : MonoBehaviour {

	private bool cheating = false;

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("AntiCheatDevice")) {
			Debug.Log ("anti cheat: can complete level");

			this.cheating = false;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.CompareTag("AntiCheatDevice")) {
			Debug.Log ("anti cheat: CANNOT complete level");

			this.cheating = true;
		}
	}

	public bool isCheating() {
		return this.cheating;
	}

}
