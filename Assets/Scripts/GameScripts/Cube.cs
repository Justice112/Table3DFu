/// <summary>
/// Author: Fu
/// CreateDate: 2014 - 1 25 !@ :$%
/// Function: 进球检测脚本，为桌球进洞的回调方法，同时检测桌球进洞后播放音效；
/// </summary>

using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public AudioClip BallinEffect;		// 进球的音效

	void OnCollisionEnter ( Collision other) {
		if (other.gameObject.tag == "Balls") {
			if (PlayerPrefs.GetInt ("OffEffect") == 0) {
				audio.PlayOneShot(BallinEffect);
			}
			(other.gameObject.GetComponent("BallScript") as BallScript).isAlowRemove = true ;
		}
	}

}
