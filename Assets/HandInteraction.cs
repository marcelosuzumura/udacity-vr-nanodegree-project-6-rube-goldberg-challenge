using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;
	public float throwForce = 1.5f;

	void Start () {
		this.trackedObj = this.GetComponent<SteamVR_TrackedObject> ();
	}
	
	void Update () {
		this.device = SteamVR_Controller.Input ((int)this.trackedObj.index);
	}

	void OnTriggerStay(Collider collider) {
		if (collider.gameObject.CompareTag("Throwable")) {
			if (this.device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				GrabObject (collider);
			} else if (this.device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
				ThrowObject (collider);
			}
		}
	}

	void GrabObject(Collider collider) {
		collider.transform.SetParent (this.gameObject.transform);
		collider.GetComponent<Rigidbody> ().isKinematic = true;
		this.device.TriggerHapticPulse (2000);
		Debug.Log ("You are touching down the trigger on an object");
	}

	void ThrowObject (Collider collider) {
		collider.transform.SetParent (null);
		Rigidbody rigidBody = collider.GetComponent<Rigidbody> ();
		rigidBody.isKinematic = false;
		rigidBody.velocity = this.device.velocity * throwForce;
		rigidBody.angularVelocity = this.device.angularVelocity;
		Debug.Log ("You have released the trigger");
	}

}
