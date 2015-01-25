/// <summary>
/// Author: Fu
/// CreateDate: 2014-12-25 !#: #*
/// Function: 桌球阴影脚本
/// </summary>
using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {
	Vector3 parentPositon;		// 桌球的位置变量

	// Update is called once per frame
	void Update () {
		parentPositon = transform.parent.position;
		transform.rotation = new Quaternion(1,0,0,-Mathf.PI * 0.32f);
		transform .position = new Vector3(parentPositon.x, 0.55f,parentPositon.z - 0.4f);
	}
}
