using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

	public GameObject pedestal;

	private Transform ballResetPosition;

	private LevelProgressionManager levelProgressionManager;

	void Start() {
		for (int i = 0; i < this.pedestal.transform.childCount; i++) {
			Transform child = this.pedestal.transform.GetChild (i);
			if (child.name == "BallResetPosition") {
				this.ballResetPosition = child.transform;
				break;
			}
		}

		this.levelProgressionManager = this.GetComponent<LevelProgressionManager> ();
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Ground")) {
			Rigidbody ballRigidBody = this.gameObject.GetComponent<Rigidbody> ();
			ballRigidBody.velocity = Vector3.zero;
			ballRigidBody.angularVelocity = Vector3.zero;

			this.gameObject.transform.position = this.ballResetPosition.position;

			this.levelProgressionManager.ResetLevelProgression ();
		}
	}

}
