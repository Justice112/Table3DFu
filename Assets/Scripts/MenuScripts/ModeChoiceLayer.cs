/// <summary>
/// Author: Fu 
/// CreateDate: 14-12-12 !(:#&
/// Function: 
/// </summary>
/// 
using UnityEngine;
using System.Collections;

public class ModeChoiceLayer : MonoBehaviour {
	public Texture BackgroundOfModeChoicerMenu ;		// 菜单界面背景图片
	public GUIStyle [] ButtonStyleOfModeChoice ;		// 菜单界面按钮图样式

	private float [] buttonPositionOfX = new float[3];		// 
	private float buttonOfCurrentMovingDistance;		// 
	private float buttonOfMaxDistance;		// 
	private float startYOfModeChoice;		//
	private float buttonOfficerOfHeight;		// 
	private bool moveFlag;		//
	private Matrix4x4 guiMatrix;		// 
	// Use this for initialization
	void Start () {
		moveFlag = true;		
		RestData();		
		buttonOfCurrentMovingDistance = 0;
		buttonOfMaxDistance = 144f;
		startYOfModeChoice = 180f;
		buttonOfficerOfHeight = 90f;
		guiMatrix = ConstOfMenu.GetMatrix();		//
	}

	void ButtonMove () {
		float length = ConstOfMenu.MovingSpeedOfMode * Time.deltaTime;
		buttonOfCurrentMovingDistance += length;
		for (int i = 0;i < ConstOfMenu.BPositionOfMode.Length; i++) {
			buttonPositionOfX[i] += ConstOfMenu.ButtonMovingStep[i] * length;
		}
		moveFlag = buttonOfCurrentMovingDistance < buttonOfMaxDistance ;

	}

	public void RestData() {
		for (int i = 0; i < ConstOfMenu.BPositionOfMode.Length; i++ ) {
			buttonPositionOfX[i] = ConstOfMenu.BPositionOfMode[i];
		}
		buttonOfCurrentMovingDistance = 0;
		moveFlag = true;
	}

	void OnGUI () {
		GUI.matrix = guiMatrix;
		GUI.DrawTexture (new Rect(0,0,ConstOfMenu.DesiginWidth,ConstOfMenu.DesiginHeight),BackgroundOfModeChoicerMenu);
		if (GUI.Button(new Rect(buttonPositionOfX[ConstOfMenu.COUNTDOW_BUTTON],startYOfModeChoice,256,64),
		               "",ButtonStyleOfModeChoice[ConstOfMenu.COUNTDOW_BUTTON])) {
			if (!moveFlag) {
				PlayerPrefs.SetInt("isTime",1);		// 
				GameLayer.RestAllStaticData ();
				Application.LoadLevel("GameScene");
			}
		}
		if (GUI.Button(new Rect(buttonPositionOfX[ConstOfMenu.PRACTICE_BUTTON],startYOfModeChoice + buttonOfficerOfHeight * 1,256,64),
		               "",ButtonStyleOfModeChoice[ConstOfMenu.PRACTICE_BUTTON])) {
			if (!moveFlag) {
				PlayerPrefs.SetInt("isTime",0);		// 
				GameLayer.RestAllStaticData ();
				Application.LoadLevel("GameScene");
			}
		}
		if (GUI.Button(new Rect(buttonPositionOfX[ConstOfMenu.RANK_BUTTON],startYOfModeChoice+ buttonOfficerOfHeight * 2,256,64),
		               "",ButtonStyleOfModeChoice[ConstOfMenu.RANK_BUTTON])) {
			if (!moveFlag) {
				(GetComponent("Constroler") as Constroler) .ChangeScrip (ConstOfMenu.ModeChoiceID ,ConstOfMenu.RankID);
			}
		}
		if (moveFlag) {
			ButtonMove ();
		}
	}
}
