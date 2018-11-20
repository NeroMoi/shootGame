using UnityEngine;
//为了访问UI组件，需要声明使用UI空间
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

	//声明文本变量
	public Text txtScore;
	//表示分数的变量
	private int totScore = 0;
	// Use this for initialization
	void Start () {

		totScore = PlayerPrefs.GetInt ("TOT_SCORE", 0);
		DispScore (0);
	
	}
	
	//累积添加分数并显示到游戏画面
	public void DispScore (int score)
	{
		totScore += score;
	/*	txtScore.text = "score<color=#ff0000>" + totScore.ToString()+"</color>";*/

		//保存分数
		PlayerPrefs.SetInt ("TOT_SCORE", totScore);
	}
}
