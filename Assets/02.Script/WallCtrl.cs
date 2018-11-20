using UnityEngine;
using System.Collections;

public class WallCtrl : MonoBehaviour {

	//表示火花粒子对象的变量
	public GameObject sparkEffect;



	//碰撞开始时触发的事件
	void OnCollisionEnter(Collision coll)
	{
		//比较发生碰撞的游戏对象的tag值

		if(coll.collider.tag == "BULLET")
		{
			//动态生成火粒子
			GameObject spark = (GameObject) Instantiate(sparkEffect,
			                                            coll.transform.position,
			                                            Quaternion.identity);
			//经过particleSYsterm组件的duration时间后删除
			Destroy (spark,spark.GetComponent<ParticleSystem>().duration + 0.2f);
			//删除发生碰撞的游戏对象
			Destroy(coll.gameObject);

		}

	}
}
