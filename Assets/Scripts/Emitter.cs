﻿using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour {
	//Waveプレハブを格納する
	public GameObject[] waves;

	//現在のWave
	private int currentWave;

	IEnumerator Start(){
		//Waveが存在しなければコルーチンを終了する
		if (waves.Length == 0) {
			yield break;
		}

		while (true) {
			//Waveを作成する
			GameObject wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);

			//WaveをEmitterの子要素にする
			wave.transform.parent = transform;

			//Waveの子要素のEnemyがすべて削除されるまで待機する
			while (wave.transform.childCount != 0) {
				yield return new WaitForEndOfFrame ();
			}

			//Waveの削除
			Destroy(wave);

			//格納されているWaveをすべて実行したらcurrentwaveを０にする（最初から→ループ）
			if (waves.Length <= ++currentWave) {
				currentWave = 0;
			}
		}
	}
}