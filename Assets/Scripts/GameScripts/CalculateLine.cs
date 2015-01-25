/// <summary>
/// Author: Fu
/// CreateDate: 2014-12-25 !#: #*
/// Function: 计算辅助线脚本
/// </summary>
using UnityEngine;
using System.Collections;

public class CalculateLine : MonoBehaviour {
	public GameObject line;
	public GameObject cueBall;
	public GameObject allScript;		//
	InitAllBalls initAllBalls;
	CamControl camCotrol;
	public ParticleSystem particle ;
	public Color c = Color.green;
	private int mode;
	private float alpha = 1;

	// Use this for initialization
	void Start () {
		mode = PlayerPrefs .GetInt("billiard");
		initAllBalls = allScript.GetComponent("InitAllBalls") as InitAllBalls;
		camCotrol = allScript.GetComponent("CamControl")as CamControl;
	}
	
	// Update is called once per frame
	void Update () {
		if ( GameLayer.TOTAL_FLAG) {
			GameObject tableBall_N = calculateBall();
			calculateUtill(tableBall_N);
			ParticalBlint(tableBall_N);
		}
	}
	Vector3 HitPoint() {
		Vector3 point = Vector3.zero;
		RaycastHit hit;
		if ( Physics.Raycast ( cueBall.transform.position,line.transform.forward,out hit ,100)) {
			if ( hit.transform.tag == "table") {
				point =hit.point;
			}
		}
		return point;
	}
	void RedColor() {
		c = Color.Lerp (Color.red,Color.red/2,Mathf.PingPong(Time.time,1));		// 球进行红色闪烁
	}
	void GreenColor () {
		c = Color.Lerp( Color.green,Color.green / 2, Mathf.PingPong (Time.time,1));
	}
	
	void FullColor () {
		c = Color.Lerp( Color.yellow,Color.blue / 2, Mathf.PingPong (Time.time,1));
	}

	GameObject calculateBall() {
		GameObject tableBall_N = null;
		if (Mathf.Abs (GameLayer.TOTAL_ROTATION) == 90) {
			return null;
		}
		Vector2 position0 = new Vector2 (cueBall.transform.position.z,-cueBall.transform.position.x);
		Vector2 forceVector = new Vector2 (Mathf.Cos(-GameLayer.TOTAL_ROTATION / 180.0f *Mathf.PI), Mathf.Sin ( -GameLayer.TOTAL_ROTATION / 180.0f * Mathf.PI));
		float k = forceVector.y/forceVector.x;
		for (int i = 1; i < GameLayer.BallGroup_TOTAL.Count ; i ++) {
			GameObject tableBall_M = GameLayer.BallGroup_TOTAL[i] as GameObject;
			BallScript ballScript = tableBall_M.GetComponent("BallScript") as BallScript ;
			Vector2 position_M = new Vector2 (tableBall_M.transform.position.z,-tableBall_M.transform.position.x);
			Vector2 vectorM_0 = new Vector2( position_M.x - position0.x, position_M.y - position0.y);
			float length = Mathf.Abs(position_M.y - k* position_M.x- position0.y + position0.x * k) / Mathf.Sqrt(1 + k * k);
			if ( length <= 1 && Vector2.Angle(vectorM_0, forceVector) < Mathf.Acos(1/2) * Mathf.Rad2Deg) {
				if (tableBall_N) {
					Vector2 position_A = new Vector2(tableBall_N.transform.position.z, -tableBall_N.transform.position.x);
					Vector2 position_B = position_M;
					float length1 = Vector2.SqrMagnitude (new Vector2(position_A.x - position0.x,position_A.y - position0.y));
					float length2 = Vector2.SqrMagnitude (new Vector2(position_B.x - position0.x,position_B.y - position0.y));
					if (length1 > length2 ) {
						tableBall_N =tableBall_M;
					}
				} else {
					tableBall_N = tableBall_M;
				}

			}
		}
		return tableBall_N;
	}

	void calculateUtill ( GameObject tableBall_N) {
		Vector2 forceVector = new Vector2(Mathf.Cos( -GameLayer.TOTAL_ROTATION / 180.0f * Mathf.PI), Mathf.Sin(-GameLayer.TOTAL_ROTATION / 180.0f * Mathf.PI));
		if (tableBall_N) {
			BallScript ballScript = tableBall_N.GetComponent("BallScript") as BallScript;
			transform.LookAt(camCotrol.Cameras[CamControl.CURRENT_CAM].transform.position);
			float k = forceVector.y / forceVector.x;
			Vector2 position_N = new Vector2 (tableBall_N.transform.position.z, -tableBall_N.transform.position.x);
			Vector2 position0 = new Vector2 (cueBall.transform.position.z, -cueBall.transform.position.x);
			Vector2 vector0_N = new Vector2 (position_N.x - position0.x ,position_N.y - position0.y);
			float length1 = Mathf.Abs (position_N.y - k * position_N.x - position0.y + position0.x *k) / Mathf.Sqrt(1 + k * k);		// 计算球与直线的距离
			float length2 = Vector2.SqrMagnitude(vector0_N);
			float length3 = Mathf.Sqrt(1 - length1 *length1);
			float length4 = Mathf.Sqrt(length2 - length1 * length1) - length3;
			Vector2 point1 = forceVector * length4;
			Vector2 point2 = position0 + point1;
			transform.position = new Vector3(-point2.y,0.98f,point2.x);
			Vector2 point3 = forceVector *length4;
			Vector2 point4 = position0 + point3 / 2;
			line.transform.position = new Vector3(-point4.y,0.98f,point4.x);
			line.transform.localScale = new Vector3 (0.005f,1,(length4 - 1f) / 10.0f);
			line.renderer.material.mainTextureOffset = new Vector2 (0,Time.time * 0.03f); 
			line.renderer.material.mainTextureScale = new Vector2 (1, (length4 - 1) / 12);
		} else {
			Vector3 hitPoint3 = HitPoint();
			Vector2 hitPoint = new Vector2 ( hitPoint3.z, -hitPoint3.x);
			Vector2 position0 =new Vector2 (cueBall.transform.position.z , -cueBall.transform.position.x); 
			Vector2 vector0_N = new Vector2 (hitPoint.x - position0.x, hitPoint.y - position0.y);
			Vector2 point1 = (position0 + hitPoint) / 2 + 0.5f * forceVector;
			transform.position = new Vector3 (100,0.98f, 100);
			float length1 = Vector2 .Distance(Vector2.zero,vector0_N);
			line.transform.position =new Vector3(-point1.y,0.98f,point1.x);
			line.transform.localScale = new Vector3 (0.005f,1,length1 / 10.0f);
			line.renderer.material.mainTextureOffset = new Vector2 (0,Time.time* 0.03f);
			line.renderer.material.mainTextureScale = new Vector2 (1,length1 / 12);
		}


	}
	/// <summary>
	/// 控制桌球闪烁的颜色
	/// </summary>
	void ParticalBlint(GameObject tableBall_N) {
		BallScript ballScript = tableBall_N.GetComponent("BallScript") as BallScript;
		transform.renderer.material.mainTexture = initAllBalls.textures[ballScript.ballId];
		int num = ConstOfGame.kitBallNum;
		if (mode < 9) {
			if (num == 0) {
				if (ballScript.ballId == 8) {
					RedColor();
				} else {
					GreenColor();
				}
			} else if (num == 1) {
				if (ballScript.ballId >8) {
					RedColor();
				} else if (ballScript.ballId == 8) {
					int one_count = GameLayer.BallGroup_ONE_EIGHT.Count;
					int two_count = GameLayer.BallGroup_TWO_EIGHT.Count;
					if (one_count==0|| two_count == 0) {
						FullColor();
					} else {
						RedColor();
					}

				}  else {
					GreenColor();
				}
			} else {
				if (ballScript.ballId > 8) {
					GreenColor();
				} else if (ballScript.ballId == 8) {
					int one_count = GameLayer.BallGroup_ONE_EIGHT.Count;
					int two_count = GameLayer.BallGroup_TWO_EIGHT.Count;
					if (one_count==0|| two_count == 0) {
						FullColor();
					} else {
						RedColor();
					}
				} else {
					RedColor();
				}
			}
		} else {
			if (ballScript.ballId == 8) {
				int one_nine = GameLayer.BallGroup_ONE_NINE.Count;
				if (one_nine == 0) {
					FullColor();
				} else {
					RedColor ();
				}
			} else {
				GreenColor();
			}
		}
		alpha = Mathf.Lerp (0.5f,1,Mathf.PingPong(Time.time,1));
		particle.startColor =c;

	}
}
