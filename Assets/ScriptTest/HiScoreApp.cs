using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HiScoreApp : MonoBehaviour {

    // ��ťλ��
    public Rect m_uploadBut;
    public Rect m_downLoadBut;

    // �����λ��
    public Rect m_nameLablePos;
    public Rect m_scoreLablePos;
    public Rect m_nameTxtField;
    public Rect m_scoreTxtField;

    // ������λ��
    public Rect m_scrollViewPosition;
    public Vector2 m_scrollPos;
    public Rect m_scrollView;

    // ����λ��
    public Rect m_gridPos;


    public string[] m_hiscores;

    // �û���
    protected string m_name="";

    // �÷�
    protected string m_score = "";

	// Use this for initialization
	void Start () {

        m_hiscores = new string[20];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {

        m_name=GUI.TextField(m_nameTxtField, m_name);
        m_score = GUI.TextField(m_scoreTxtField, m_score);

		GUI.Label(m_nameLablePos, "UserName:");
      		  GUI.Label(m_scoreLablePos, "Score");

        // �ϴ�����
        if (GUI.Button(m_uploadBut, "Upload")) {
		StartCoroutine(UploadScore(m_name,m_score));      
            		m_name = "";
            		m_score = "";
        }

        // ���ط���
        if (GUI.Button(m_downLoadBut, "Download")) {
		StartCoroutine(DownloadScores(m_name,m_score)); 
        }

        m_scrollPos=GUI.BeginScrollView(m_scrollViewPosition, m_scrollPos, m_scrollView);

        m_gridPos.height = m_hiscores.Length * 30;

        // ��ʾ�������а�
        GUI.SelectionGrid(m_gridPos, 0, m_hiscores, 1);

        GUI.EndScrollView();
    }
	IEnumerator UploadScore (string name,string score) {
		WWWForm form = new WWWForm ();
		form.AddField("username",name);
		form.AddField("score",score);
		WWW www = new WWW ("127.0.0.1:81/Uploadscore.php",form);
		yield return www;
		if (www.error !=null) {
			Debug.LogError(www.error);
		}
	}
	
	public class UserData {
		public int id;
		public string username;
		public int score;
	}

	IEnumerator DownloadScores (string name, string score) {
		WWW www = new WWW ("127.0.0.1:81/DownloadScores.php" );
		yield return www;
		if (www.error !=null) {
			Debug .Log (www.error);
		} else {
			var dict = MiniJSON.Json.Deserialize(www.text) as Dictionary<string,object>;
			int index = 0;
			foreach (object v in dict.Values) {
				UserData user = new UserData();
				MiniJSON.Json.ToObject(user,v);

				m_hiscores[index] = user.username + ":" + user.score;
				Debug.Log(m_hiscores[index]);
				index++;
			}
		}
	}


}


