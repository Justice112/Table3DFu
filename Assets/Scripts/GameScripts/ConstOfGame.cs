/// <summary>
/// Author: Fu
/// CreateDate: 14-12-16 @@:@^
/// Function:
/// </summary>

using UnityEngine;
using System.Collections;

public class ConstOfGame : MonoBehaviour {
	public static Vector3 CUEBALL_POSITION = new Vector3(0,0.98f,-8);
	public static float SCALEX =4*Screen.width / 800.0f;
	public static float SCALEY =4*Screen.height / 480.0f;
	public static float MAX_ROTATION_X = 77;
	public static float btnPositionY = 420f;
	public static float btnSize = 60;
	public static float sdf;
	public static float rotationStep = 0.1f;		// 
	public static float MAX_SPEED =50;
	public static float miniMapScale = 2;
	public static int kitBallNum = 0;
}
