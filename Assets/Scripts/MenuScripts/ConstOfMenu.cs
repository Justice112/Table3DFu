/// <summary>
/// Author: Fu
/// CreateDate: 14-12-11 @#:@):))
/// Function: 初始化游戏开发的所需的各类常量
/// </summary>

using UnityEngine;
using System.Collections;

public class ConstOfMenu : MonoBehaviour {
	public static float DesiginWidth=800.0f; 		// 标准屏幕的宽度
	public static float DesiginHeight=480.0f;		// 标准屏幕的高度
	public static int START_BUTTON=0;		// 开始按钮的索引
	public static int MUSSIC_BUTTON=1;		// 声音按钮的索引
	public static int HELP_BUTTON=2;		// 帮助按钮的索引
	public static int ABOUT_BUTTON=3;		// 关于按钮的索引
	public static int EIGHT_BUTTON=0;		// 八球模式的索引按钮
	public static int NINE_BUTTON=1;		// 九球模式的索引按钮
	public static int COUNTDOW_BUTTON=0;		// 倒计时模式的按钮的索引
	public static int PRACTICE_BUTTON=1;		// 练习模式的按钮的索引
	public static int RANK_BUTTON=2;		// 排行榜按钮的索引
	public static float MovinSpeed = 80f;		// 主界面按钮的移动速度
	public static float [] ButtonPositionOfX = new float[4] {128 , 416,128,416} ;		// 主界面的按钮的位置
	public static float [] ButtonMovingStep = new float[4] {1,-1,1,-1} ;		// 主界面按钮的移动方向
	public static float [] BPositionOfMode = new float[3] {128,416,128}; 		// 模式选择界面按钮的位置
	public static float [] BMovingStepOfMode = new float[3] {-1,1,-1} ; 		// 按钮移动方向
	public static float  MovingSpeedOfMode = 80f;			//  该界面按钮的移动速度
	public static int MainID = 1;		// 主界面ID
	public static int ChoiceID = 2;		// 种类选择界面ID
	public static int SoundID =3;		// 声音控制界面ID
	public static int HelpID = 4;		// 帮助界面ID
	public static int AboutID = 5;		// 关于界面ID
	public static int ModeChoiceID =6;		// 模式选择界面的ID
	public static int RankID = 7;		// 排行榜界面ID

	/// <summary>
	/// 获取单位矩阵
	/// </summary>
	/// <returns>The matrix.</returns>
	public static Matrix4x4 GetMatrix () {
		Matrix4x4 guiMatrix = Matrix4x4.identity;		
		float lux = (Screen.width -ConstOfMenu.DesiginWidth*Screen.height/ConstOfMenu.DesiginHeight) / 2.0f;
		guiMatrix.SetTRS(new Vector3(lux,0,0),Quaternion.identity,new Vector3(Screen.height/ConstOfMenu.DesiginHeight,Screen.height / ConstOfMenu.DesiginHeight, 1)); 
		return guiMatrix; 

	}

	/// <summary>
	/// GUI 逆矩阵
	/// </summary>
	/// <returns>The invert matrix.</returns>
	public static Matrix4x4 GetInvertMatrix () {
		Matrix4x4 guiInverseMatrix = GetMatrix();
		guiInverseMatrix = Matrix4x4.Inverse(guiInverseMatrix);
		return guiInverseMatrix;
	}



}
