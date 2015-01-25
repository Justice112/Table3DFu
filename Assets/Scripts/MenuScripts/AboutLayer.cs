/// <summary>
/// Author: Fu 
/// CreateDate: 14-12-12 	@@:)&
/// Function: 
/// </summary>
/// 
using UnityEngine;
using System.Collections;

public class AboutLayer : MonoBehaviour {
	public Texture BackgroundOfAboutLayer;		// 界面背景图片
	public Texture AboutOfAboutLayer;		// 关于界面背景图片
	private Matrix4x4 guiMatrix;		// GUI 自适应矩阵

	void Start() {
		guiMatrix = ConstOfMenu.GetMatrix(); 		// 获取GUI自适应矩阵
	}
	void OnGUI () {
		GUI.matrix = guiMatrix;		// 获取GUI自适应矩阵
		GUI.DrawTexture(new Rect(0,0,ConstOfMenu.DesiginWidth,ConstOfMenu.DesiginHeight),BackgroundOfAboutLayer) ;
		GUI.DrawTexture(new Rect (148,150,504,299),AboutOfAboutLayer); 		// 绘制关于界面图片
	}
}
