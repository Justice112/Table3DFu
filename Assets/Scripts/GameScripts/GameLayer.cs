/// <summary>
/// Author: Fu
/// CreateDate: 14-12-19  @!: #(
/// Function: 游戏界面脚本，负责绘制游戏主界面，包括游戏主界面的多个按钮等，
/// </summary>
using UnityEngine;
using System.Collections;

public class GameLayer : MonoBehaviour {
	enum ButtonS{Go = 0, Far, Near, Left,Right,M,FirstV,FreeV,ThirdV}		// 声明按钮系列
	public static ArrayList BallGroup_ONE_EIGHT = new ArrayList();		// 八球模式下的2个辅助列表
	public static ArrayList BallGroup_TWO_EIGHT = new ArrayList();		// 八球模式下的2个辅助列表
	public static ArrayList BallGroup_ONE_NINE = new ArrayList();		// 九球模式下的1个辅助列表
	public static ArrayList BallGroup_TOTAL = new ArrayList();		// 所有球的列表
	public static int BallInNum = 0;		// 进球个数
	public static float TOTAL_ROTATION = 0.0f; 		// 绕X轴旋转的角度
	public static bool TOTAL_FLAG;			// 触控与按钮是否可用的总标志位
	public static bool IS_START_ACTION = false;		// 球杆是否运动的总标志位

	private bool isFirstView;		// 左下角显示按钮的标志位
	private bool isFirstActonOver;		// 第一次运动是否结束的标志位
	private bool isSecondeActionOver;		// 第二次运动是否结束的标志位
	private int tbtIndex;		// 控制右下角图片的变量
	Matrix4x4 guiMatrix;		// GUI的自适应矩阵
	public GUIStyle[] btnStyle;		// 按钮的STyle
	public GUIStyle  fbtnStyle;		// 按钮的GUIstyle
	Logic logic;		// 主逻辑类组件
	MiniMap miniMap;		// 小地图组件
	InitAllBalls initClass;		// 初始化桌球的组件
	public Texture2D [] Nums;	// 
	public AudioClip StartSound;		// 进球的音效

	// Use this for initialization
	void Start () {
		isFirstView = true;
		isFirstActonOver = false;
		isSecondeActionOver = false;
		GameLayer.BallGroup_TOTAL.Add(GameObject.Find("cueBall"));
		initClass = GetComponent("InitAllBalls") as InitAllBalls ;
		miniMap = GetComponent("MiniMap") as MiniMap;
		initClass.initAllBalls(PlayerPrefs.GetInt("billiard"));
		logic = GetComponent("Logic") as Logic;
		if (PlayerPrefs.GetInt("offMusic") != 0) {
			audio.Pause();
		}
		guiMatrix = ConstOfMenu.GetMatrix();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("MenuScene");
		}
	}

	/// <summary>
	/// 绘制按钮方法
	/// </summary>
	public void DrawButtons () {
		if (GUI.Button(new Rect(0,ConstOfGame.btnPositionY,ConstOfGame.btnSize,ConstOfGame.btnSize),"",btnStyle[(int)ButtonS.Go])) {
			if (GameLayer .TOTAL_FLAG) {
				GameLayer .TOTAL_FLAG = false;
				isFirstActonOver = false;
				isSecondeActionOver = false;
				GameLayer.IS_START_ACTION = true;		// 设置移动的标志位
				logic.cuePosition = logic.cueBall.transform.position ;	// 
			}
		}
		if ( GUI.RepeatButton ( new Rect(100,ConstOfGame.btnPositionY,ConstOfGame.btnSize,ConstOfGame.btnPositionY),"",btnStyle[(int) ButtonS.Far])) {
			if (GameLayer.TOTAL_FLAG) {
				(GetComponent("CamControl") as CamControl ).MovieCamera(-5);		// 
			}
		}
		if ( GUI.RepeatButton ( new Rect(200,ConstOfGame.btnPositionY,ConstOfGame.btnSize,ConstOfGame.btnPositionY),"",btnStyle[(int) ButtonS.Near])) {
			if (GameLayer.TOTAL_FLAG) {
				(GetComponent("CamControl") as CamControl ).MovieCamera(5);		// 
			}
		}
		if ( GUI.RepeatButton ( new Rect(300,ConstOfGame.btnPositionY,ConstOfGame.btnSize,ConstOfGame.btnPositionY),"",btnStyle[(int) ButtonS.Left])) {
			if (GameLayer.TOTAL_FLAG) {
				GameLayer.TOTAL_ROTATION -= ConstOfGame.rotationStep; 		// 
				logic.cueObject.transform.RotateAround(logic.cueBall.transform.position , Vector3.up,-ConstOfGame.rotationStep);
			}
		}
		if (GUI.RepeatButton (new Rect (460,ConstOfGame.btnPositionY,ConstOfGame.btnSize,ConstOfGame.btnSize),"",btnStyle[(int ) ButtonS.Right])) {
			if (GameLayer.TOTAL_FLAG) {
				GameLayer.TOTAL_ROTATION += ConstOfGame.rotationStep ;
				logic.cueObject.transform.RotateAround(logic.cueBall.transform.position,Vector3.up,ConstOfGame.rotationStep);
			}
		}
		if (GUI.Button (new Rect (550,ConstOfGame.btnPositionY,ConstOfGame.btnSize,ConstOfGame.btnSize),"",btnStyle[(int ) ButtonS.M])) {
			if (GameLayer.TOTAL_FLAG) {
				MiniMap.isMiniMap = !MiniMap.isMiniMap;
			}
		}
		if (GUI.Button (new Rect (650,ConstOfGame.btnPositionY,ConstOfGame.btnSize,ConstOfGame.btnSize),"",fbtnStyle)) {
			if (GameLayer.TOTAL_FLAG) {
				logic.assistBall.SetActive(!logic.assistBall.activeSelf);		// 调用辅助线
				logic.line.SetActive(!logic.line.activeSelf);
			}
		}
		if (isFirstView) {
			if (GUI.Button (new Rect (740,ConstOfGame.btnPositionY,ConstOfGame.btnSize,ConstOfGame.btnSize),"",btnStyle[(int ) ButtonS.FirstV])) {
				if (GameLayer.TOTAL_FLAG) {
					(GetComponent("CamControl")as CamControl) .ChangeCam(2);
					isFirstView = !isFirstView;
				}
			}
		} else {
			if (GUI.Button (new Rect (740,ConstOfGame.btnPositionY,ConstOfGame.btnSize,ConstOfGame.btnSize),"",btnStyle[(int ) ButtonS.FreeV])) {
				if (GameLayer.TOTAL_FLAG) {
					(GetComponent("CamControl")as CamControl) .ChangeCam(1);
					isFirstView = !isFirstView;
				}
			}
		}
		if (GUI.Button (new Rect (730,10,30,30),"",btnStyle[(int ) ButtonS.ThirdV])) {
			if (GameLayer.TOTAL_FLAG) {
				(GetComponent("CamControl")as CamControl) .ChangeCam(0);
			}
		}

	}
	void OnGUI () {
		GUI.matrix = guiMatrix;
		GUI.DrawTexture (new Rect (770,10,30,30),Nums[GameLayer.BallInNum]);
		DrawButtons ();
		miniMap.drawMiniMap();
		if (GameLayer.IS_START_ACTION) {
			cueRunAction() ;
		}
	}
	/// <summary>
	/// 重置数据
	/// </summary>
	public static void RestAllStaticData () {
		BallGroup_ONE_EIGHT.Clear();
		BallGroup_TWO_EIGHT.Clear();
		BallGroup_ONE_NINE.Clear();
		BallGroup_TOTAL.Clear();
		BallInNum = 0;
		TOTAL_ROTATION = 0.0f;
		TOTAL_FLAG = true;
		IS_START_ACTION = false; 
		CamControl.CURRENT_CAM = 1;
		CamControl.PRE_POSITION = Vector3.zero ;
		CamControl.TOUCH_FLAG = true;
		PowerBar.showTime = 720;
		PowerBar.restBars = 22;
	}
	void cueRunAction() {
		if (!isFirstActonOver) {
			logic.cue.transform.Translate (new Vector3(0,0,Time.deltaTime)) ;
			if ( logic.cue.transform.localPosition.z <= -2) {
				isFirstActonOver = true ;

			}
		}else if (!isSecondeActionOver && isFirstActonOver) {
			logic.cue.transform.Translate (new Vector3(0,0,-2*Time.deltaTime)) ;
			if ( logic.cue.transform.localPosition.z >= -0.45f) {
				isSecondeActionOver = true ;
				
			}
		} else {
			if (PlayerPrefs.GetInt("offEffect") == 0) {
				audio.PlayOneShot(StartSound);
			}
			logic.cue.transform.localPosition = new Vector3(0,0,-1) ;
			logic.cue.renderer.enabled = false;
			logic.cue.renderer.enabled =false;
			logic.assistBall.transform.position = new Vector3(100,0.98f,100);
			logic.line.renderer.enabled = false;
			logic.cueBall.rigidbody.velocity = new Vector3((PowerBar.restBars -1)/22.0f*ConstOfGame.MAX_SPEED* Mathf.Sin(GameLayer.TOTAL_ROTATION / 180.0f * Mathf.PI),0,
			                                               (PowerBar.restBars -1) / 22.0f * ConstOfGame.MAX_SPEED * Mathf.Cos (GameLayer.TOTAL_ROTATION / 180.0f * Mathf.PI));
			GameLayer.IS_START_ACTION = false;

		}
	}

}
