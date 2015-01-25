/// <summary>
/// Author: Fu 
/// CreateDate: 14-12-12 !(:#&
/// Function: 
/// </summary>
/// 
using UnityEngine;
using System.Collections;

public class MusicLayer : MonoBehaviour {
	public Texture BackgroundOfMusicLayer;		// 菜单界面背景图片
	public Texture2D [] MusicBtns;		// 声明音乐按钮图片数组
	public Texture2D [] MusicTex;		// 声音按钮对应的图片
	public Texture2D [] EffectBtns;		// 声明音效按钮数组
	public Texture2D [] EffectTex;		// 音效按钮对应的图片
	private int effectIndex;		// 音效索引
	private int musicIndex;		// 音效索引
	private GUIStyle btStyle;		// 按钮样式
	private Matrix4x4 guiMatrix;		// GUI自适应矩阵

	// Use this for initialization
	void Start () {
		effectIndex = PlayerPrefs.GetInt("offEffect");		// 初始化音效索引
		musicIndex = PlayerPrefs.GetInt("offMusic");		// 初始化音乐索引
		guiMatrix = ConstOfMenu.GetMatrix();		// 初始化GUI自适应矩阵

	}

	void OnGUI () {
		GUI.matrix = guiMatrix;
		GUI.DrawTexture(new Rect(0,0,ConstOfMenu.DesiginWidth,ConstOfMenu.DesiginHeight),BackgroundOfMusicLayer);
		GUI.DrawTexture(new Rect(200,180,273,80),MusicTex[musicIndex %2]);

		if(GUI.Button(new Rect(473,190,110,80),MusicBtns[musicIndex % 2],btStyle)) {
			musicIndex++;
			PlayerPrefs.SetInt("offMusic",musicIndex % 2); 		// 将新的索引按钮存入prefer中
		}
		GUI.DrawTexture(new Rect(200,320,273,80) ,EffectBtns[effectIndex % 2]);		// 绘制显示图片
		if ( GUI.Button( new Rect(473,190,110,80),EffectBtns[effectIndex % 2],btStyle)) {
			effectIndex++;
			PlayerPrefs.SetInt("offEffect",effectIndex % 2);		// 将新的按钮索引存入prefer中
		}
	}

}
