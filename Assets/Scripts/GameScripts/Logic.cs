/// <summary>
/// Author: Fu
/// CreateDate: 14-12- 16  @!:%%
/// Function:  主控逻辑脚本
/// 		负责游戏中各个游戏对象的正常运转、功能的正常执行
/// 		以及遵循游戏规则判断输赢
/// </summary>
using UnityEngine;
using System.Collections;

public class Logic : MonoBehaviour {
	private bool isEightMode;
	private bool isJudgeOver;
	public bool resetPositionFlag;
	public GameObject cue;
	public GameObject line;
	public GameObject assistBall;
	public GameObject cueObject;
	public GameObject cueBall;
	public Vector3 cuePosition;		// 
	ArrayList ballNeedRemove;
	private float t = 0;
	private Result result;

	// Use this for initialization
	void Start () {
		result = GetComponent("Result") as Result;
		cuePosition = cueBall.transform.position;
		isEightMode = PlayerPrefs.GetInt("billiard") <9;
		isJudgeOver = PlayerPrefs.GetInt("billiard") ==9;
		resetPositionFlag = false;
		ballNeedRemove = new ArrayList ();

	}
	private void afterBallStopCallback () {
		if (CamControl.CURRENT_CAM ==1) {
			if (resetPositionFlag)  {
				setData();
				resetData();
			} else {
				t = Mathf.Min(t + Time.deltaTime / 2.0f, 1);
				cueObject.transform.position = Vector3.Lerp (cuePosition,cueBall.transform.position,t);
				if (t ==1) {
					setData();
					t = 0;
				}
			}
		} else {
			setData();
		}
	}
	void resetData() {
		GameLayer.TOTAL_ROTATION = 0;
		cueObject.transform.rotation = new Quaternion(1,0,0,13);
		(GetComponent("CamControl") as CamControl) .SetFreeCamera();
	}
	// Update is called once per frame
	void Update () {
		for (int i =0; i < GameLayer.BallGroup_TOTAL.Count; i ++) {
			GameObject tran = GameLayer.BallGroup_TOTAL[i] as GameObject;
			BallScript ballScript =tran.GetComponent("BallScript") as BallScript;
			if (ballScript.isAlowRemove) {
				ballNeedRemove.Add(tran);
			}
		}
		removeBalls();
		if (!GameLayer.TOTAL_FLAG && !GameLayer.IS_START_ACTION) {
			for (int i = 1; i < GameLayer.BallGroup_TOTAL.Count; i ++) {
				GameObject obj = (GameLayer.BallGroup_TOTAL[i] as GameObject) ;
				if (obj.rigidbody.velocity.sqrMagnitude > 0.01f) {
					return;
				}
			}
			if (resetPositionFlag) {
				afterBallStopCallback();
			}
			if (cueBall.rigidbody.velocity.sqrMagnitude < 0.01f) {
				afterBallStopCallback ();
			}
		}
	}
	
	private void setData() {
		if (resetPositionFlag) {
			resetPositionFlag =false;
			cueBall.transform.position = ConstOfGame.CUEBALL_POSITION;
			cueBall.transform.rigidbody.velocity = Vector3.zero;
			cueBall.transform.renderer.enabled =true;

		}
		if (!isJudgeOver) {
			int one_count = GameLayer.BallGroup_ONE_EIGHT.Count;
			int two_count = GameLayer.BallGroup_TWO_EIGHT.Count;
			if (one_count == two_count) {
				ConstOfGame.kitBallNum = 0;
				PowerBar.tipIndex = 0;

			} else if (one_count < two_count) {
				ConstOfGame.kitBallNum = 1;
				isJudgeOver = true;
				PowerBar.tipIndex = 1;
			} else {
				ConstOfGame.kitBallNum = 2;
				PowerBar.tipIndex = 2;
				isJudgeOver =true;
			}
		}
		cueObject .transform.position = cueBall.transform.position;
		cue.renderer.enabled = true;
		line.renderer.enabled = true;
		GameLayer.TOTAL_FLAG =true;
	}
	void removeBalls() {
		if (isEightMode) {
			if (GameLayer.BallGroup_ONE_EIGHT.Count == 0|| GameLayer.BallGroup_TWO_EIGHT.Count == 0 ) {
				PowerBar.tipIndex = 4;
			}
			for (int i = ballNeedRemove.Count -1;i >= 0; i -- ){
				GameObject tran = ballNeedRemove[i] as GameObject;
				BallScript ballScript = tran.GetComponent("BallScript") as BallScript ;
				if (ballScript.ballId == 0) {
					resetPositionFlag = true;
					ballScript.isAlowRemove = false;
					tran.transform.renderer.enabled = false;
					tran.rigidbody.velocity = Vector3.zero;
					tran.rigidbody.angularVelocity = Vector3.zero;

				} else if ( ballScript.ballId<8) {
					GameLayer.BallGroup_ONE_EIGHT.Remove(tran); 
					GameLayer.BallGroup_TOTAL.Remove(tran);
					DestroyImmediate(tran);
					GameLayer.BallInNum++;
					if (ConstOfGame.kitBallNum ==2) {
						result.goLoseScene();
					}
				} else if ( ballScript.ballId == 8 ) {
					GameLayer.BallGroup_TOTAL.Remove(tran);
					DestroyImmediate(tran);
					if (GameLayer.BallGroup_ONE_EIGHT.Count == 0 || GameLayer.BallGroup_TWO_EIGHT.Count == 0) {
						result.goVectorScene();
					} else {
						result.goLoseScene();
					}
				} else {
					GameLayer.BallGroup_TWO_EIGHT.Remove(tran);
					GameLayer.BallGroup_TOTAL.Remove(tran);
					DestroyImmediate(tran);
					GameLayer.BallInNum ++;
					if (ConstOfGame.kitBallNum ==1) {
						result.goLoseScene();
					}
				}
			}
			} else {
				if ( GameLayer.BallGroup_ONE_NINE.Count == 0) {
					PowerBar.tipIndex = 4;

				}
				for (int i = ballNeedRemove.Count -1; i >=0; i --) {
					GameObject tran = ballNeedRemove [i] as GameObject;
					BallScript ballScript = tran.GetComponent("BallScript") as BallScript;
					if (ballScript.ballId == 0) {
						resetPositionFlag = true;
						ballScript.isAlowRemove =false;
						tran.transform.renderer.enabled = false;
						tran.rigidbody.velocity = Vector3.zero;
						tran.rigidbody.angularVelocity = Vector3.zero;
					} else if (ballScript.ballId == 8) {
						GameLayer.BallGroup_ONE_NINE.Remove(tran);
						GameLayer.BallGroup_TOTAL.Remove(tran);
						DestroyImmediate(tran);
						if (GameLayer.BallGroup_ONE_NINE.Count == 0) {
							result.goVectorScene();
						} else {
							result.goLoseScene();
						}
					} else {
						PowerBar.tipIndex = 3;
						GameLayer.BallGroup_ONE_NINE.Remove(tran);
						GameLayer.BallGroup_TOTAL.Remove(tran);
						DestroyImmediate(tran);
						GameLayer.BallInNum ++;
					}
				}
			} 
			ballNeedRemove.Clear(); 		// 清空列表
	}
}
