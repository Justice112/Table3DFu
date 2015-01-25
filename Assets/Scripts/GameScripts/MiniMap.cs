/// <summary>
/// Author: Fu
/// CreateDate: 2014-12-24 !!:!)
/// Function: 绘制游戏界面的小地图，是游戏界面的俯视图
/// </summary>

using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
	public static bool isMiniMap = true;		// 
	private Texture2D [] textures;		// 桌球图片
	private Texture2D miniTable;		// mini球台图片
	private Texture2D cue;		// 球杆的图片
	private float scale;		// 缩放比
	private Vector2 privotPoint;		// 旋转点
	Matrix4x4 guiInvert;		// 获取gui的逆矩阵
	// Use this for initialization
	void Start () {
		guiInvert = ConstOfMenu.GetMatrix ();
		scale = ConstOfGame .miniMapScale;
		InitMiniTexture(PlayerPrefs.GetInt ("billiard"));		// 初始化桌球图片
		miniTable = Resources.Load("minitable") as Texture2D;
		cue = Resources.Load("cueMini") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void drawMiniMap() {
		if (MiniMap.isMiniMap) {
			GUI.DrawTexture(new Rect (0,0,283.0f/scale,153.0f/scale),miniTable );
			for (int i = 0;i < GameLayer.BallGroup_TOTAL.Count; i++) {
				GameObject tran = GameLayer.BallGroup_TOTAL[i] as GameObject;
				BallScript ballScript = tran.GetComponent("BallScript") as BallScript;
				Vector3 ballPosition = tran.transform.position;
				int ballId = ballScript.ballId;
				GUI.DrawTexture (new Rect(ballPosition.z *5 + 70,ballPosition.x * 5 + 35f,5,5), textures[ballId]);

			}
			if ((GameObject.Find("Cue") as GameObject).renderer.enabled)  {		// 如果球杆可见
				Vector3 cuePosition = (GameObject.Find("CueObeject") as GameObject) .transform.position;
				Vector3 cueBallPosition = (GameObject.Find("CueBall") as GameObject) .transform .position;
				privotPoint = new Vector2(cueBallPosition.z * 5+72.5f,cueBallPosition.x *5 +37f);
				Vector3 m =guiInvert.MultiplyPoint3x4(new Vector3 (privotPoint.x,privotPoint.y,0));
				GUIUtility.RotateAroundPivot(GameLayer.TOTAL_ROTATION,new Vector2(m.x,m.y));
				GUI.DrawTexture (new Rect(cuePosition.z * 5+45,cuePosition.x * 5+ 37f,20,2),cue);
			}
		}
	}
	void InitMiniTexture (int billiard)  {
		bool init = (billiard - 8) > 0;
		if (!init) {
			textures = new Texture2D[16];
			for (int i = 0; i < 16 ;i ++ ) {
				textures[i] = Resources.Load("minimap" +i) as Texture2D;
			}
		}else {
			textures =new Texture2D[10];
			for (int i = 0; i< 10;i++) {
				textures [i] = Resources.Load("minimap" + i) as Texture2D;
			}
		}
	}
}
