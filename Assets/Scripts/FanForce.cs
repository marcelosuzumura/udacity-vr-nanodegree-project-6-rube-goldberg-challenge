using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanForce : MonoBehaviour {

	public float force = 10f;

	void OnTriggerStay(Collider other) {
		if (other.tag == "Throwable" && other.attachedRigidbody) {
//			Debug.Log ("fan: " + other.gameObject.name);
			other.attachedRigidbody.AddForce (this.gameObject.transform.forward * force);
		}
	}

}
