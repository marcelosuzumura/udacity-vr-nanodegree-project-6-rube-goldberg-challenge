using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabThrowManager : MonoBehaviour {

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
		if (collider.gameObject.CompareTag("Throwable") || collider.gameObject.CompareTag("Structure")) {
//			Debug.Log ("You are touching a throwable or a structure");
			if (this.device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				GrabObject (collider);
			} else if (this.device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
				if (collider.gameObject.name == "Ball") {
					ThrowBall (collider);
				} else {
					PlaceObject (collider);
				}
			}
		}
	}

	void GrabObject(Collider collider) {
		collider.transform.SetParent (this.gameObject.transform);
		collider.GetComponent<Rigidbody> ().isKinematic = true;

		if (collider.gameObject.name == "Ball") {
			collider.gameObject.GetComponent<LevelProgressionManager>().ChangeBallMaterialIfCheating ();
		}

		this.device.TriggerHapticPulse (2000);
	}

	void ThrowBall (Collider collider) {
		collider.transform.SetParent (null);

		Rigidbody rigidBody = collider.GetComponent<Rigidbody> ();
		rigidBody.isKinematic = false;
		rigidBody.velocity = this.device.velocity * throwForce;
		rigidBody.angularVelocity = this.device.angularVelocity;

		collider.gameObject.GetComponent<LevelProgressionManager>().ChangeBallMaterialIfCheating ();
	}

	void PlaceObject (Collider collider) {
		collider.transform.SetParent (null);
	}

}
