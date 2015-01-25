/// <summary>
/// Author: Fu 
/// CreateDate: 14-12-12 !(:#&
/// Function: 各个界面之间的跳转调度
/// </summary>
/// 
using UnityEngine;
using System.Collections;

public class Constroler : MonoBehaviour {
	private int currentID = ConstOfMenu.MainID;		// 初始化当前界面ID
	MonoBehaviour [] script;		// 声明脚本组件

	void Awake () {
		script = GetComponents<MonoBehaviour>();		// 定义脚本组件
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			EscapeEvent();
		}
	}

	public void ChangeScrip (int offID, int onID) {
		restData();
		script[offID].enabled=false;
		script[onID].enabled=true;
		currentID= onID;		// 设置当前界面ID
	}

	/// <summary>
	/// Escapes the event.
	/// </summary>
	public void EscapeEvent () {
		switch ( currentID) {
		case 1: if ((GetComponent("MainLayer") as MainLayer).MoveFlag) {
				break;
			}
			Application.Quit();
			break;
		case 2:
		case 3:
		case 4:
		case 5:
			ChangeScrip(currentID,ConstOfMenu.MainID);
			break;
		case 6: 
			ChangeScrip(currentID,ConstOfMenu.ChoiceID);
			break;
		case 7:
			ChangeScrip(currentID,ConstOfMenu.ModeChoiceID);
			break;
		}
	}

	/// <summary>
	/// 对各个脚本组件的重新设置数据
	/// </summary>
	private void restData () {
		(GetComponent("MainLayer")  as MainLayer).RestData();
		(GetComponent("ChoiceLayer")  as ChoiceLayer).RestData();
		(GetComponent("ModeChoiceLayer") as ModeChoiceLayer).RestData();
		HelpLayer helpLayer = GetComponent("HelpLayer") as HelpLayer;
		helpLayer.RestData();
 	}
}
