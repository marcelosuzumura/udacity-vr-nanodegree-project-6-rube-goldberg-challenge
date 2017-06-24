using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudioManager : MonoBehaviour {

	public AudioClip audioClip;

	private AudioSource audioSource;

	void Start () {
		this.audioSource = this.GetComponent<AudioSource> ();
	}
	
	void OnCollisionEnter(Collision collision) {
		this.audioSource.PlayOneShot (this.audioClip);
	}

}
