/// <summary>
/// Author: Fu
/// CreateDate: 2014 - 12-25
/// Function: 该脚本挂载在“Ball”和“CueBall” 游戏对象下， 主要负责控制桌球碰撞的声音以及桌球碰撞效果
/// </summary>
using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	public bool isAlowRemove = false ;		// 是否删除的标志位
	public int ballId = 0;		// 	桌球ID
	public AudioClip BallHit;		// 桌球互相碰撞发出的声音
	public bool setData = true;		// 桌球数据标志位


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rigidbody.velocity = this .transform.rigidbody.velocity * 0.988f;
		this.transform.rigidbody.angularVelocity = this.transform.rigidbody.angularVelocity * 0.988f;
		if (this.transform.rigidbody.velocity.sqrMagnitude < 0.01f) {
			this.transform.rigidbody.velocity = Vector3.zero;
		}
		if (Mathf.Abs(this.transform.position.z) > 12.3f || Mathf.Abs(this.transform.position.z) > 6.1f) {
			if (setData) {
				collider.material.bounciness = 0.2f;
				this.transform.rigidbody.velocity = this.transform.rigidbody.velocity / 4;
				setData = false;
			}
		} else {
			if ( Mathf.Abs (this .transform.rigidbody.velocity.y) >= 3f) {
				this.transform.rigidbody.velocity = Vector3.zero;
			}
			collider.material.bounciness = 1;
			setData = true;
		}
	}
	void OnCollisionEnter (Collision collision) {
		if ( PlayerPrefs.GetInt("offEffect") == 0) {
			if (collision.gameObject.tag =="Balls") {
				float speedOfMySelf = gameObject.rigidbody.velocity.magnitude;
				float speedOfAnother = collision.rigidbody.velocity.magnitude;
				if (speedOfMySelf >speedOfAnother ) {
					audio.volume = speedOfMySelf / ConstOfGame.MAX_SPEED;
					audio.PlayOneShot(BallHit);
				}
			}
		}
	}

}
