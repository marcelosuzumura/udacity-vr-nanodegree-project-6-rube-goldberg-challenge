using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFanBlades : MonoBehaviour {

	public float speed = 500f;

	void Update () {
		this.transform.Rotate (Vector3.forward, speed * Time.deltaTime);
	}

}
