/// <summary>
/// Author: Fu 
/// CreateDate: 14-12-12 !(:#&
/// Function: 搭建主菜单界面及4个按钮的初始化动态效果
/// </summary>
using UnityEngine;
using System.Collections;

public class MainLayer : MonoBehaviour {
	public Texture BackgroundOfMainMenu;		// 菜单界面背景图片
	public GUIStyle [] ButtonStyleOfMain;		// 菜单界面按钮图样式
	public bool MoveFlag;		// 主菜单界面按钮是否进行移动的标志位

	private float [] ButtonPositionOfX = new float[4] ;		// 创建按钮数组
	private float buttonOfficerOfHeight;		// 按钮之间的高度差
	private float startYOfMainmMenu;		// 第一个按钮的起始Y坐标
	private float buttonOfCurrentMovingDistance; 		// 主菜单界面当前移动的距离
	private float buttonOfMaxDistance;		// 主菜单界面按钮移动的最大距离
	private Matrix4x4 guiMatrix;		// GUI矩阵


	// Use this for initialization
	void Start () {
		buttonOfficerOfHeight = 75;
		startYOfMainmMenu = 150; 
		MoveFlag = true;
		buttonOfCurrentMovingDistance = 0;
		buttonOfMaxDistance = 80;		// 
		guiMatrix= ConstOfMenu.GetMatrix();		// 获取GUI自适应矩阵
		RestData();		// 重新设置位置等信息

	}

	void OnGUI () {
		GUI.matrix = guiMatrix;		// 设置GUI自适应矩阵
		GUI.DrawTexture(new Rect(0,0,ConstOfMenu.DesiginWidth,ConstOfMenu.DesiginHeight),BackgroundOfMainMenu);		// 绘制背景图片
		DrawMainMenu () ;
		if (MoveFlag ) {
			ButtonOfMainMenuMove() ;
		}
	}

	/// <summary>
	/// 重新设置数据
	/// </summary>
	public void RestData() {
		// 设置位置
		for ( int i = 0; i< ConstOfMenu.ButtonPositionOfX.Length; i++ ) {
			ButtonPositionOfX[i] = ConstOfMenu.ButtonPositionOfX[i] ;
			buttonOfCurrentMovingDistance = 0; 		// 重置当前移动距离为0
			MoveFlag = true; 		// 移动的标志位为True
		}

	} 

	/// <summary>
	/// 绘制主菜单界面
	/// </summary>
	public void DrawMainMenu() {
		// 主界面跳到模式选择界面
		if(GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.START_BUTTON],
		                       startYOfMainmMenu+buttonOfficerOfHeight*0,256,64),
		              "",ButtonStyleOfMain[ConstOfMenu.START_BUTTON])) {
			if(!MoveFlag) {
				(GetComponent("Constroler") as Constroler).ChangeScrip (
					ConstOfMenu.MainID,ConstOfMenu.ChoiceID) ;
			}
		} 
		// 主界面跳到声音控制界面
		if(GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.MUSSIC_BUTTON],
		                       startYOfMainmMenu+buttonOfficerOfHeight*0,256,64),
		              "",ButtonStyleOfMain[ConstOfMenu.MUSSIC_BUTTON])) {
			if(!MoveFlag) {
				(GetComponent("Constroler") as Constroler).ChangeScrip (
					ConstOfMenu.MainID,ConstOfMenu.SoundID) ;
			}
		} 
		// 主界面跳到帮助界面
		if(GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.HELP_BUTTON],
		                       startYOfMainmMenu+buttonOfficerOfHeight*0,256,64),
		              "",ButtonStyleOfMain[ConstOfMenu.HELP_BUTTON])) {
			if(!MoveFlag) {
				(GetComponent("Constroler") as Constroler).ChangeScrip (
					ConstOfMenu.MainID,ConstOfMenu.HelpID) ;
			}
		} 
		// 主界面跳到关于界面
		if(GUI.Button(new Rect(ButtonPositionOfX[ConstOfMenu.ABOUT_BUTTON],
		                       startYOfMainmMenu+buttonOfficerOfHeight*0,256,64),
		              "",ButtonStyleOfMain[ConstOfMenu.ABOUT_BUTTON])) {
			if(!MoveFlag) {
				(GetComponent("Constroler") as Constroler).ChangeScrip (
					ConstOfMenu.MainID,ConstOfMenu.AboutID) ;
			}
		} 
	}

	/// <summary>
	/// 按钮移动的方法
	/// </summary>
	public void ButtonOfMainMenuMove() {
		float length = ConstOfMenu.MovinSpeed * Time.deltaTime; 		// 按钮移动的距离
		buttonOfCurrentMovingDistance+= length;		// 按钮移动一次
		// 设置按钮的位置
		for(int i =0;i<ButtonPositionOfX.Length;i++) {
			ButtonPositionOfX[i] += (ConstOfMenu.ButtonMovingStep[i] *length); 
		}
		MoveFlag = buttonOfCurrentMovingDistance<buttonOfMaxDistance; 		// 计算是否移动到最大距离
	}

}
