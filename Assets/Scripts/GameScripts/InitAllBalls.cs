/// <summary>
/// Author: Fu
/// CreateDate: 2014-12-25 !%:!)
/// Function: 初始化相应桌球脚本
/// </summary>
using UnityEngine;
using System.Collections;

public class InitAllBalls : MonoBehaviour {
	public GameObject ball;
	public Texture2D [] textures ;

	public void initAllBalls (int billiard)  {
		int [] randomArray = RandomArray(billiard ,7);
		bool init = (billiard - 8) >0;
		int sum = 0;
		if (!init) {
			textures = new Texture2D[16];
			for (int i = 0 ; i < 16;i ++) {
				textures[i] = Resources.Load("snooker" + i) as Texture2D;

			}
			for (int i = 1; i <= 5; i ++) {
				for (int j=1;j<=i; j ++) {
					Vector3 ballPosition =new Vector3(-(0.5f +0.05f) * (i - 1) +(j - 1) * (0.5f  + 0.05f) * 2, 0.98f,5.8f +(0.5f +0.05f) * 2 * (i - 1));
					GameObject obj = Instantiate(ball,ballPosition,new Quaternion(1,0,0,Mathf.PI/2)) as GameObject;
					obj.transform.renderer.material.mainTexture = textures[randomArray[sum +j -1] +1] ;
					(obj.GetComponent("BallScript") as BallScript) .ballId = randomArray[sum + j -1] +1;
					if ((randomArray[sum +j -1] + 1) < 8) {
						GameLayer.BallGroup_ONE_EIGHT.Add(obj);
					} else if ((randomArray[sum + j - 1] +1) >8) {
						GameLayer.BallGroup_TWO_EIGHT.Add(obj);
					}
					GameLayer.BallGroup_TOTAL.Add(obj);
				}
				sum +=i;

			}
		} else {
			textures = new Texture2D[16];
			for (int i = 0 ; i < 16;i ++) {
				textures[i] = Resources.Load("snooker" + i) as Texture2D;
				
			}
			for (int i = 1; i <= 5; i ++) {
				for (int j=1;j<=i; j ++) {
					Vector3 ballPosition =new Vector3(-(0.5f +0.05f) * (i - 1) +(j - 1) * (0.5f  + 0.05f) * 2, 0.98f,5.8f +(0.5f +0.05f) * 2 * (i - 1));
					GameObject obj = Instantiate(ball,ballPosition,new Quaternion(1,0,0,Mathf.PI/2)) as GameObject;
					obj.transform.renderer.material.mainTexture = textures[randomArray[sum +j -1] +1] ;
					(obj.GetComponent("BallScript") as BallScript) .ballId = randomArray[sum + j -1] +1;
					if ((randomArray[sum +j -1] + 1) < 8) {
						GameLayer.BallGroup_ONE_EIGHT.Add(obj);
					} else if ((randomArray[sum + j - 1] +1) >8) {
						GameLayer.BallGroup_TWO_EIGHT.Add(obj);
					}
					GameLayer.BallGroup_TOTAL.Add(obj);
				}
				sum +=i;
				
			}
		}
	}
	private int [] RandomArray (int length, int index) {
		length = length > 8 ? 9:15;
		ArrayList orign = new ArrayList ();
		int [] result = new int[length];
		for (int i = 0; i < length; i ++) {
			if (i == index) {
				continue;
			}
			orign.Add(i);
		}
		for (int i = 0; i < length; i ++) {
			if (i == 4) {
				result[i] = index;
				continue;
			}
			int tempIndex = (int) Random.Range(0, orign.Count - 0.1f); 
			result[i] = (int) orign[tempIndex];
			orign.RemoveAt(tempIndex);
		}
		return result;
	}
}
