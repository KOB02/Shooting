using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	//スコアを表示するGUIText
	public GUIText scoreGUIText;

	//ハイスコアを表示するGUIText
	public GUIText highScoreGUIText;

	//スコア
	private int score;
	//ハイスコア
	private int highScore;

	//playerPrefsで保存するためのキー
	private string highScoreKey = "highScore";

	void Start(){
		Initialize ();
	}

	void Update(){
		//スコアがハイスコアより大きければ
		if (highScore < score) {
			highScore = score;
		}

		//スコア・ハイスコアを表示する
		scoreGUIText.text = score.ToString ();
		highScoreGUIText.text = "HighScore : " + highScore.ToString ();
	}

	//ゲーム開始前の状態に戻す
	private void Initialize(){
		//スコアを０に戻す
		score = 0;

		//ハイスコアを所得する、保存されていなければ０を取得する
		highScore = PlayerPrefs.GetInt (highScoreKey, 0);
	}

	//pointの追加
	public void AddPoint(int point){
		score = score + point;
	}
	 
	//ハイスコアを保存する
	public void Save(){
		PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.Save ();
	}
}

