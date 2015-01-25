/// <summary>
/// Author: Fu
/// CreateDate: 2014 - 12-24 !$:@&
/// Function: 绘制并实时更新母球撞击力度的能量条
/// </summary>
using UnityEngine;
using System.Collections;

public class PowerBar : MonoBehaviour {
	public static int tipIndex;		// 
	public Texture2D[] tipTexture;		// 
	public Texture2D bg;		// 力量滑动条背景图片
	public Texture2D  bar;		// 力量块整张图片
	private int groupX = 0, groupY = 120, groupWidth = 100, groupHeight = 230, barX = 5, barY = 5,barW = 40, barH = 220;		// 背景图片矩形框参数力量块图片矩形参数
	private float texX = 0,texY = 0, texW = 1,texH = 1;		// 纹理矩形参数
	private static int totalBars = 22;		// 力量块的个数
	private int barWidth;		// 每个力量块的宽度
	private Rect groupRect;		// 群组矩形变量
	public static int restBars = 22;		// 
	private Matrix4x4 invertMatrix;		// 移动向量
	Vector3 movePosition;		// 移动向量
	Vector3 startPosition;		// 初始位置向量
	public Texture2D [] textures;		// 计时器相关的变量
	public bool isStartTime ;		// 初试时间
	private int totalTime;		// 总时间
	private int countTime;		// 计算时间
	public static int showTime = 720;		// 总倒数时间
	private int startTime;		// 初始化起始时间
	private int x = 300,y = 30, numWidth = 32,numHeight = 32,span = 6;
	private Result result;
	// Use this for initialization
	void Start () {
		result = GetComponent("Result") as Result ;
		if (PlayerPrefs.GetInt("billiard")==8) {
			tipIndex = 0;
		} else {
			tipIndex = 3;
		}
		startTime = (int) Time.time;
		countTime = 0;
		totalTime = 720;
		isStartTime = PlayerPrefs.GetInt("isTime") > 0;		// 是否为倒计时模式的标志位
		startPosition = Vector3.zero;
		movePosition = Vector3.zero;
		invertMatrix = ConstOfMenu.GetInvertMatrix();
		groupRect = new Rect(groupX ,groupY,groupWidth,groupHeight +100); 
		barWidth = barH / totalBars;

	}
	
	// Update is called once per frame
	void Update () {
		if (isStartTime) {
			countTime = (int) Time.time - startTime;
			showTime = totalTime - countTime;
			if (showTime <=0) {
				result.goLoseScene();		// 
			}
		}
		if ( GameLayer.TOTAL_FLAG) {
			if (Input.GetMouseButtonDown(0)) {
				CamControl.PRE_POSITION = Input.mousePosition;
				CamControl.TOUCH_FLAG = false;
				startPosition = invertMatrix.MultiplyPoint3x4(Input.mousePosition);
				movePosition = startPosition;
			}
			if (Input .GetMouseButton(0)) {
				movePosition = invertMatrix.MultiplyPoint3x4(Input.mousePosition);
			}
			if (Input .GetMouseButtonUp(0)) {
				CamControl.TOUCH_FLAG =false;
			}
		}
	}
	void DrawTime (int time) {
		int minute = time/60;
		int seconds = time%60;
		int num1 = minute / 10;
		int num2 = minute % 10;
		int num3 = seconds / 10; 
		int num4 = seconds % 10;
		GUI.BeginGroup(new Rect(x, y, 5*(numWidth + span), numHeight)) ;		// 绘制分钟纹理图
		GUI.DrawTexture (new Rect(0,0,numWidth,numHeight), textures[num1]) ;
		GUI.DrawTexture (new Rect((numWidth + span),0,numWidth,numHeight), textures[num2]); 
		GUI.DrawTexture (new Rect(2* (numWidth + span),0, numWidth,numHeight),textures[textures.Length - 1]);		// 绘制秒钟纹理图
		GUI.DrawTexture (new Rect(3* (numWidth + span),0, numWidth,numHeight),textures[num3]);		
		GUI.DrawTexture (new Rect(4* (numWidth + span),0, numWidth,numHeight),textures[num4]);		// 		
		GUI.EndGroup();
	}
	void OnGUI () {
		GUI.matrix = ConstOfMenu.GetMatrix() ;
		GUI.DrawTexture (new Rect (271,5,256,16),tipTexture[tipIndex]);		// 绘制纹理图
		if (isStartTime) {
			DrawTime (showTime);
		}
		GUI.BeginGroup (new Rect ( 0, 0, groupWidth,groupHeight),bg);
		GUI.DrawTextureWithTexCoords (new Rect (barX,barY + barWidth * (totalBars - restBars),barW,barWidth * restBars), bar,
		                              new Rect(texX, texY, texW, texH * restBars / totalBars));		// 
		GUI.EndGroup();
		if (new Rect ( barX + groupX, barY + groupY ,barW, barH).Contains ( new Vector2 ( startPosition.x, 480.0f - startPosition.y))) {
			CamControl.TOUCH_FLAG = false;
			restBars = Mathf.Clamp(totalBars - (int) (480.0f - movePosition.y - barY - groupY) / barWidth,1,22);
		} else {
			if (new Rect ( 0,420,800,60).Contains ( new Vector2 ( startPosition.x, 480.0f - startPosition.y))) {
				CamControl.TOUCH_FLAG = false;
			} else if (new Rect ( 730,10,30,30).Contains ( new Vector2 ( startPosition.x, 480.0f - startPosition.y))) {
				CamControl.TOUCH_FLAG = false;
			} else {
				CamControl.TOUCH_FLAG =true;
			}
		}
	}
}
