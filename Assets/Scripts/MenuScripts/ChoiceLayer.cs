/// <summary>
/// Author: Fu 
/// CreateDate: 14-12-12 !(:#&
/// Function: 
/// </summary>
/// 
using UnityEngine;
using System.Collections;

public class ChoiceLayer : MonoBehaviour {
	public Texture BackgroundOfChoiceMenu ;		// 菜单界面背景图片
	public GUIStyle [] ButtonStyleOfChoice ;		// 菜单界面按钮图样式

	private bool scaleFlag;		// 进行缩放的标志位
	private float scaleFactor;		// 缩放因子
	private float buttonSize;		// 按钮大小
	private float buttonStartX;		// 按钮X方向位置
	private float buttonStartY;		// 按钮Y方向位置
	private Matrix4x4 guiMatrix;		// GUI 自适应矩阵
	// Use this for initialization
	void Start () {
		scaleFlag = true;
		scaleFactor = 0.0f;
		buttonSize = 120;
		buttonStartX = 200;
		buttonStartY = 220;
		guiMatrix = ConstOfMenu.GetMatrix();
	}

	void OnGUI () {
		GUI.matrix = guiMatrix;
		GUI.DrawTexture (new Rect(0,0,ConstOfMenu.DesiginWidth,ConstOfMenu.DesiginHeight),BackgroundOfChoiceMenu);
		ButtonScale() ;
		if (GUI.Button (new Rect(buttonStartX,buttonStartY,buttonSize*scaleFactor,buttonSize*scaleFactor),"",ButtonStyleOfChoice[ConstOfMenu.EIGHT_BUTTON])) {
			if (!scaleFlag) {
				PlayerPrefs.SetInt("billiard",8);		// 8球模式标志存入
				(GetComponent("Constroler") as Constroler).ChangeScrip (ConstOfMenu.ChoiceID,ConstOfMenu.ModeChoiceID);
			}
		} 
		if (GUI.Button (new Rect(buttonStartX+240,buttonStartY,buttonSize*scaleFactor,buttonSize*scaleFactor),"",ButtonStyleOfChoice[ConstOfMenu.NINE_BUTTON])) {
			if (!scaleFlag) {
				PlayerPrefs.SetInt("billiard",9);		// 8球模式标志存入
				(GetComponent("Constroler") as Constroler).ChangeScrip (ConstOfMenu.ChoiceID,ConstOfMenu.ModeChoiceID);
			}
		} 
	}

	/// <summary>
	/// 按钮执行缩放动作
	/// </summary>
	void ButtonScale () {
		scaleFactor  = Mathf.Min(1.0f,scaleFactor+Time.deltaTime); 		// 计算缩放比
		scaleFlag = (scaleFactor != 1f);		// 计算缩放标志位
	}
	public void RestData() {
		scaleFlag = true ;
		scaleFactor = 0.0f;
	}
}
