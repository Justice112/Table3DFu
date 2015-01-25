/// <summary>
/// Author: Fu
/// CreateDate: 14-12-13  @#: @) 
/// Function: 
/// </summary>
using UnityEngine;
using System.Collections;

public class RankLayer : MonoBehaviour {
	public float GroupX = 177, GroupY = 0, GroupW = 300, GroupH = 240;		// 显示数字图片的高宽
	public Texture2D BackGroundOfRankLayer;
	public Texture2D Box ;
	public Texture2D Date;
	public Texture2D Score;
	public Texture2D [] NumberTextures;
	public GUIStyle RankGUIStyle;		// 

	private int MaxHeight;
	private int numSize;
	private string txt;
	private float oldPosY;		// 
	private float currPosY;		//
	private string [] showRecords;		//
	private Matrix4x4 guiMatrix; 

	// Use this for initialization
	void Start () {
		guiMatrix = ConstOfMenu.GetMatrix();
		showRecords = Result.LoadData();
		MaxHeight = numSize * showRecords.Length - 192 ;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			oldPosY = Input.mousePosition.y;
		}
		if (Input.GetMouseButton(0)) {
			currPosY = Input.mousePosition.y;
			GroupY = Mathf.Clamp((GroupY - currPosY +oldPosY), -MaxHeight,0);
			oldPosY = currPosY;
		}
	}
	void OnGUI () {
		GUI.matrix = guiMatrix;
		GUI.DrawTexture (new Rect(0,0,ConstOfMenu.DesiginWidth,ConstOfMenu.DesiginHeight),BackGroundOfRankLayer);
		GUI.DrawTexture(new Rect (150,150,530,294),Box);
		if (GUI.Button (new Rect (230,180,130,40),Date,RankGUIStyle)) {
			string [] records = Result.LoadData ();
			showRecords = records;
		}
		if (GUI.Button ( new Rect(470,180,130,40),Score,RankGUIStyle)) {
			string [] records = Result.LoadData ();
			RecordsSort(ref records);
			showRecords = records;
		}
		GUI.BeginGroup(new Rect (177,220,476,192)); 
		GUI.BeginGroup(new Rect(0,GroupY,476,numSize * showRecords.Length));
		if (showRecords[0] != "") {
			DrawRecords(showRecords);
		}
		GUI.EndGroup();
		GUI.EndGroup();
	}
	public void DrawRecords(string [] records) {
		for (int i=0;i < records.Length;i++) {
			string date = records[i].Split(',')[0];
			string score = records[i].Split(',')[1];
			int [] dateNum = StringToNumber(date);
			int [] scoreNum = StringToNumber(score);
			for (int j = 0;j < dateNum.Length; j++ ) {
				GUI.DrawTexture(new Rect((j +1)*numSize,i*numSize,numSize,numSize),NumberTextures[dateNum[j]]);
			}
			for (int j = 0;j < scoreNum.Length; j++ ) {
				GUI.DrawTexture(new Rect((j +1)*numSize,i*numSize,numSize,numSize),NumberTextures[scoreNum[j]]);
			}
 		}
	}
	public void RecordsSort (ref string[] records) {
		for (int i = 0; i < records.Length-1; i++){
			for(int j=i+1;j< records.Length;j++) {
				if(int.Parse(records[i].Split(',')[1]) < int.Parse(records[j].Split(',')[1])) {
					string tempRecord = records[i];
					records[i] = records[j];
					records[j] = tempRecord;
				}
			}
		}

	}
	public static int[] StringToNumber (string str) {
		int [] result =new int[str.Length];
		for (int i = 0;i < str.Length;i ++) {
			char c = str [i];
			if (c=='-') {
				result[i] =10;
			} else {
				result[i] = str [i] -'0';
			}
		}
		return result ;
	}

}
