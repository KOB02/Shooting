using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//ヒットポイント
	public int hp = 1;

	//Spaceshipコンポーネント
	Spaceship spaceship;

	IEnumerator Start(){
		//Spaceshipコンポーネントを取得
		spaceship = GetComponent<Spaceship> ();

		//ローカル座標のY軸のマイナス方向に移動する
		Move (transform.up * -1);

		//canShotがfalseの場合、ここでコルーチンを終了させる
		if (spaceship.canShot == false) {
			yield break;
		}


		while (true) {
			//子要素をすべて取得する
			for (int i = 0; i < transform.childCount; i++) {
				Transform shotPosition = transform.GetChild (i);

				//Shotpositionの位置・角度で作成
				spaceship.Shot(shotPosition);
			
				//shotDelayを待つ
				yield return new WaitForSeconds (spaceship.shotDelay);
			}
		}
	}

	public void Move(Vector2 direction){
		GetComponent<Rigidbody2D> ().velocity = direction * spaceship.speed;
	}

	void OnTriggerEnter2D(Collider2D c){
		//レイヤー名を取得
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

		//レイヤー名がBullet(Player)以外の時は何も行わない
		if (layerName != "Bullet(Player)") return;

		//PlayerBulletのTransformを取得
		Transform playerBulletTransform = c.transform.parent;

		//Bulletコンポーネントを取得
		Bullet bullet = playerBulletTransform.GetComponent<Bullet>();

		//ヒットポイントを減らす
		hp = hp -bullet.power;

		//弾の削除
		Destroy (c.gameObject);

		if (hp <= 0) {
			//爆発する
			spaceship.Explosion ();

			//エネミーの削除
			Destroy (gameObject);
		} else {
			spaceship.GetAnimator().SetTrigger("Damage");
		}

	}
}