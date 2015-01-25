/// <summary>
/// Author: Fu 
/// CreateDate: 14-12-12  	 @@:@)
/// Function: 
/// </summary>
/// 
using UnityEngine;
using System.Collections;

public class HelpLayer : MonoBehaviour {
	public Texture2D [] HelpTexture;		// 帮助界面的图片数组
	private float positionY;		// 第一张图片的位置
	private float officerY;		// 图片直接Y方向的偏移量
	private Matrix4x4 guiMatrix;		// GUI自适应矩阵
	private int currentIndex;		// 当前图片的索引
	private Vector2 touchPoint;		// 触控点坐标
	private float currentDistance;		// 当前移动距离
	private float scale;		// 自适应的滑动距离缩放系数
	private bool isMoving;		// 图片是否在移动
	private float moveStep;		// 图片移动的步径
	private float stepHao;		// 移动距离的正负号
	private Vector2 prePosition;		// 上一次触控点的位置

	// Use this for initialization
	void Start () {
		touchPoint = Vector2.zero;
		prePosition = Vector2.zero;		// 初始化二维向量
		currentIndex = 0;		// 当前图片索引
		positionY = 0.0f;		// 移动的Y距离
		moveStep = 300;		// 移动步径
		currentDistance = 0;		// 当前移动距离
		isMoving = false;		// 是否移动的标志位
		officerY = ConstOfMenu.DesiginHeight;		// 屏幕高度
		guiMatrix = ConstOfMenu.GetMatrix();		// 获取GUI 自适应矩阵
		scale = Screen.height / 480f;		// 滑动自适应系数

	}

	void OnGUI () {
		GUI.matrix  = guiMatrix ;
		for (int i= 0; i< HelpTexture.Length; i++ ) {
			if(Mathf.Abs(currentIndex - i) < 2) {
				GUI.DrawTexture(new Rect (0,positionY + officerY*i,ConstOfMenu.DesiginWidth,ConstOfMenu.DesiginHeight),HelpTexture[i]);
			}
		}
		if (isMoving) {

		}
	}

	/// <summary>
	/// Indexs the change.
	/// </summary>
	/// <param name="step">Step.</param>
	void IndexChange(int step) {
		int newIndex = currentIndex+step;		// 计算当前编号
		// 如果编号超出边界
		if(newIndex>7||newIndex<0) {
			return ;
		}
		currentIndex = newIndex;		// 修改当前索引值curentIndex确保其在0-6之间
		isMoving = true;		// 设置为可移动
	}
	// Update is called once per frame
	void Update () {
		if(isMoving && Input.touchCount>0) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {		// 按钮按下时的回调方法
				touchPoint  = touch.position;
				prePosition = touch.position;
			} else if (touch.phase == TouchPhase.Moved) {
				float newPositionY = positionY - touch.position.y + prePosition.y;
				positionY = (newPositionY > 0) ?0:(newPositionY > (-480*7)? newPositionY : (-480*7));
				prePosition=touch.position;
			} else if (touch.phase ==TouchPhase.Ended) {
				isMoving = true;
				currentDistance = (touch.position.y - touchPoint.y) / scale;
				stepHao = (Mathf.Abs (currentDistance)>150.0f) ? (currentDistance  > 0?-Mathf.Abs(moveStep):Mathf.Abs(moveStep)) : (currentDistance>0?Mathf.Abs(moveStep):-Mathf.Abs(moveStep));
				IndexChange((int)stepHao);		// 
			}
		}
	}
	public void RestData() {
		currentIndex = 0;
		positionY = 0.0f;
		moveStep = 30;
		currentDistance = 0;
		isMoving = false;
	}

	/// <summary>
	/// Textures the move.
	/// </summary>
	private void textureMove() {
		float positionYOfNew = positionY + moveStep * Time.deltaTime;
		float minDistance = -480*Mathf.Abs(currentIndex) ;
		if(stepHao==1) {
			positionY = Mathf.Max(positionYOfNew,minDistance);
		}  else if (stepHao == -1) {
			positionY = Mathf.Min(positionYOfNew,minDistance);
		}else {
			if (moveStep > 0) {
				positionY = Mathf.Min(positionYOfNew,minDistance);
			}else {
				positionY = Mathf.Max(positionYOfNew,minDistance);
			}
		}
		isMoving=!(positionY==minDistance); 		// 计算是否可移动的标志位
	}
}
