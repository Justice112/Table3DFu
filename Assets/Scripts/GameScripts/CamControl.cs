/// <summary>
/// Author: Fu 
/// CreateDate: 2014-12-16   @):%^
/// Function: 负责摄像机的切换，包括第一人称视角和第三人称视角，同时负责切换后摄像机角度和位置的恢复；
/// </summary>
using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {
	public LayerMask Mask = -1;
	public GameLayer CueBall;
	private float totalRotationX; 		// 绕X轴旋转的角度
	public float FreeViewRotationMatrixY = 0 ; 		// free 视角时绕Y轴的旋转角度
	Logic logic;		// 获取组件
	public GameObject [] Cameras;		// 摄像机数组
	public static int CURRENT_CAM ; 		// 当前摄像机编号
	public static Vector3 PRE_POSITION = Vector3.zero;		// 
	public static bool TOUCH_FLAG = true;		// 是否允许触控的标志位
	Matrix4x4 inverse;		// GUI的逆矩阵 
	Quaternion qua;		// 记录初始旋转位置的变量
	Vector3 vec;		// 记录初始位置的变量



	// Use this for initialization
	void Start () {
		qua = Cameras[4].transform.rotation;		// 记录初始位置，主要是用于恢复位置
		vec = Cameras[4].transform.position;		// 
		for (int i = 0; i < 3; i ++) {		// 进行自适应
			Cameras[i].camera.aspect = 800.0f / 480.0f;		// 设置视口的缩放比
			float lux = ( Screen.width - ConstOfMenu.DesiginWidth * Screen.height / ConstOfMenu.DesiginHeight ) / 2.0f;
			Cameras[i].camera.pixelRect = new Rect (lux ,0, Screen.width-2*lux ,Screen.height); 		// 
		}
		totalRotationX = 13 ;		// 
		logic = GetComponent("Logic") as Logic;		// 
		inverse = ConstOfMenu.GetInvertMatrix();		// 获取逆转矩阵


	}
	public void ChangeCam(int index) {
		SetFreeCamera();
		Cameras[CURRENT_CAM].SetActive(false);
		Cameras[index].SetActive(false);
		CURRENT_CAM = index;		// 设置当前摄像机索引

	}
	public void MovieCamera(int sign) {
		Cameras[CURRENT_CAM].transform.Translate(new Vector3(0,0,sign*Time.deltaTime));
		Vector3 posCueBall = Cameras[CURRENT_CAM].transform.InverseTransformPoint(CueBall.transform.position);
		// 设置移动的最大距离与最新记录
		if(posCueBall.z > 35 || posCueBall.z < 7) {
			Cameras[CURRENT_CAM].transform.Translate(new Vector3(0,0,-sign*Time.deltaTime));
		}
	}
	// Update is called once per frame
	void Update () {
		if(!TOUCH_FLAG) {
			return ;
		} 
		if (!GameLayer.TOTAL_FLAG) {
			return;
		}
		if (Input.GetMouseButton(0)) {
			float angleY = (Input.mousePosition.x - PRE_POSITION.x) / ConstOfGame.SCALEX;
			float angleX = (Input.mousePosition.y - PRE_POSITION.y) / ConstOfGame.SCALEY;
			Vector3 newPoint = ConstOfMenu.GetInvertMatrix().MultiplyVector(Input.mousePosition);
			switch (CURRENT_CAM) {
			case 0: MainFunction(Input.mousePosition); break ;
			case 1: FirstFunction(angleX,angleY); break;
			case 2: FreeFunction (angleX,angleY); break;
			}
			PRE_POSITION = Input.mousePosition ;
		}
	}
	/// <summary>
	/// 重新设置各个摄像机的各种信息
	/// </summary>
	public void SetFreeCamera() {
		Cameras[3].transform.rotation = Cameras[4].transform.rotation;
		Cameras[3].transform.position = Cameras[4].transform.position;
		FreeViewRotationMatrixY = GameLayer.TOTAL_ROTATION;
		totalRotationX = 13;		// 重新设置旋转角度的度数
		Cameras[2].transform.rotation = Cameras[4].transform.rotation;
		Cameras[2].transform.position = Cameras[4].transform.position;
		Cameras[1].transform.rotation = Cameras[4].transform.rotation;
		Cameras[1].transform.position = Cameras[4].transform.position;

	}

	/// <summary>
	/// 主摄像机旋转操作
	/// </summary>
	public void MainFunction(Vector3 pos ) {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(pos);
		// 重置位置及旋转位置
		if ( Physics.Raycast(ray,out hit, 100 ,Mask.value)) {
			Cameras[3].transform.rotation = qua;
			Cameras[3].transform.position = vec;
			Vector3 hitPoint = hit.point;
			Vector3 cubBallPoint = CueBall.transform.position ;		// 获取与球台交点坐标
			float angle = 180 - Mathf.Atan2(cubBallPoint.x - hitPoint.x ,cubBallPoint.z- hitPoint.z) * Mathf.Rad2Deg;
			GameLayer.TOTAL_ROTATION = -angle;
			Cameras[3] .transform.RotateAround( ConstOfGame.CUEBALL_POSITION,Vector3.up,GameLayer.TOTAL_ROTATION);
			logic.cueObject.transform.rotation = Cameras[3].transform.rotation;		// 设置球杆的旋转角度


		}
	} 

	/// <summary>
	/// 第一人称摄像机回调
	/// </summary>
	public void FirstFunction (float angleX, float angleY) {
		if (Mathf.Abs(angleY) > Mathf.Abs(angleX) && Mathf.Abs (angleY )>1f) {
			GameLayer.TOTAL_ROTATION += angleY;
			logic.cueObject.transform.RotateAround(logic.cueBall.transform.position,Vector3.up,angleY);
		} else {
			if(totalRotationX + angleX > 10 && totalRotationX +angleX < 90) {
				if (Mathf .Abs(angleX) > 1f) {
					Vector3 right = new Vector3 (Mathf .Cos(-GameLayer.TOTAL_ROTATION / 180.0f * Mathf.PI),0,Mathf.Sin(-GameLayer.TOTAL_ROTATION / 180.0f * Mathf .PI));  		// 计算旋转轴
					totalRotationX += angleX;
					Cameras[1].transform .RotateAround(logic.cueBall.transform.position,right,angleX);	//
				}
			}
		}
 	}

	public void FreeFunction(float  angleY, float angleX ) {
		if (Mathf.Abs(angleY) > 0.5f) {
			FreeViewRotationMatrixY += angleY;
			Cameras[2].transform.RotateAround (logic.cueBall.transform.position,Vector3.up,angleY);
		}else {
			if (totalRotationX +angleX > 10&& totalRotationX +angleX<90f) {
				Vector3 right = Cameras[CURRENT_CAM] .transform.TransformDirection(Vector3.right);
				totalRotationX += angleX;
				Cameras[2].transform.RotateAround(logic.cueBall.transform.position,right,angleX);
			}
		}
	}
}
