using UnityEngine;
using System.Collections;

public class BarrelCtrl : MonoBehaviour {

	//表示爆咋效果的变量
	public GameObject expEffect;
	//要随机选择的纹理数组
	public Texture[] textures;

	private Transform tr;

	//保存被子弹击中次数的变量

	private int hitCount = 0;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();

		int idx = Random.Range (0, textures.Length);
		GetComponentInChildren<MeshRenderer>().material.mainTexture = textures [idx];
	
	}

	//被射线击中时调用函数
	void OnDamage(object[] _params)
	{
		//射线发射的原点位置
		Vector3 firePos = (Vector3)_params [0];

		//射线击中酒桶的位置
		Vector3 hitPos = (Vector3)_params [1];

		//入射向量（射线角度） = 被击中的位置 - 发射原点
		Vector3 incomeVector = hitPos - firePos;

		//将入射向量变为单位向量
		incomeVector = incomeVector.normalized;

		//根据入射向量生成物理力
		GetComponent<Rigidbody> ().AddForceAtPosition (incomeVector * 1000f, hitPos);

		//累加油桶被子弹击中的次数，次数达到3则爆炸

		if (++hitCount >= 3) 
		{
			ExpBarrel ();
		
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.tag == "BULLET") 
		{
			Destroy(coll.gameObject );

			//累加油桶被子弹击中的次数，三次以上则触发爆咋
			if(++hitCount >=3)
			{
				ExpBarrel();
			}
		}
	}
	//实现油桶爆咋的函数
	void ExpBarrel()
	{
		//生成爆咋效果例子
		Instantiate (expEffect, tr.position, Quaternion.identity);
		//以指定原点为中心，获取半径为10.0f内的collider对象
		Collider[] colls = Physics.OverlapSphere (tr.position, 10.0f);
		//对获取的Collider对象施加爆炸力
		foreach (Collider coll in colls) 
		{
			Rigidbody rbody = coll.GetComponent<Rigidbody>();
			if(rbody != null)
			{
				rbody.mass = 1.0f;
				rbody.AddExplosionForce(1000.0f,tr.position,10.0f,300.0f);
			}
		}
		//5秒后删除油桶模型
		Destroy (gameObject, 5.0f);

	}
}
