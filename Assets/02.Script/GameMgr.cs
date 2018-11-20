using UnityEngine;
using System.Collections;
//使用list数据类型需要添加的命名空间
using System.Collections.Generic;

public class GameMgr : MonoBehaviour {

	//保存怪兽出现的所有位置的数组
	public Transform[] points;
	//要分配怪兽预设的变量
	public GameObject monsterPrefab;

	//保存事先生成的怪兽的Lisr数据类型
	public List<GameObject>monsterPool = new List<GameObject> ();


	//生成怪兽的周期
	public float createTime =2.0f;
	//生成怪兽的最大数量
	public int MaxMonster = 20;
	//控制是否终止游戏的变量
	public bool isGameOver = false;
	// Use this for initialization

	//声明单例模式的示例变量
	public static GameMgr instance = null;

	//声明表示音量的变量
	public float sfxVolumn = 1.0f;
	//静音功能
	public bool isSfxNute = false;

	void Awake()
	{
		//将GameMgr类带入示例
		instance = this;
	}

	void Start () {

		Debug.Log ("run success0");
		//获取层次视图下spawn point 所有Transform组件
		points = GameObject.Find ("SpawnPoint").GetComponentsInChildren<Transform> ();


		//生成怪兽预设并保存到对象池
		for (int i=0; i<MaxMonster; i++)
		{
			//生成怪兽预设
			GameObject monster =(GameObject)Instantiate(monsterPrefab);

			//设置生成的怪兽名
			monster.name = "Monster_" + i.ToString();

			//禁用生成的怪兽
			monster.SetActive(false);
			//将生成的怪兽添加到对象池
			monsterPool.Add (monster);

		}
		Debug.Log ("run success1");
		if (points.Length > 0)
		{
			Debug.Log ("run success2");
			//调用生成怪兽的协程函数
			StartCoroutine(this.CreateMonster());

		}
	
	}
	
	//生成怪兽的协程函数
	IEnumerator CreateMonster()
	{
		//无限循环直到游戏结束
		while (!isGameOver) 
		{
			yield return new WaitForSeconds(createTime);
			//玩家死亡时跳出当前协程
			if(isGameOver)
			{
				yield break;
			}

			//循环处理对象池中的每个对象
			foreach(GameObject monster in monsterPool)
			{
				//通过是否禁用判断可以使用的怪兽
				if(!monster.activeSelf)
				{
					//计算随机位置
					int idx = Random.Range (1,points.Length);

					//设置怪兽出现的位置
					monster.transform.position = points[idx].position;

					//激活怪兽
					monster.SetActive(true);

					//激活最后一个对象池中的怪兽预设并跳出循环
					break;
				}
			}

			//当前已生成的怪兽数量
	/*		int monsterCount = (int) GameObject.FindGameObjectsWithTag("MONSTER").Length;
			//只有比怪兽最大数量小时才需要继续生成怪兽

			Debug.Log ("run success2");
			if(monsterCount < MaxMonster)
			{
				Debug.Log ("run succese4");
				//程序挂机一段时间（挂售生成周期）

				yield return new WaitForSeconds(createTime);

				//计算随机位置
				int idx = Random.Range (1,points.Length);
				//动态生成怪兽
				Instantiate(monsterPrefab,points[idx].position,points[idx].rotation);

			}
		
			else
			{
				yield return null;
			} */
		}
	}

	//声音共享函数
	public void PlaySfx(Vector3 pos,AudioClip sfx)
	{
		//如果静音选项为true则立刻停止声音

		if (isSfxNute) 
		{
			return;
		}
		//动态生成游戏对象
		GameObject soundObj = new GameObject("Sfx");
		//指定声音发出的位置
		soundObj.transform.position = pos;

		//向生成的游戏对象添加AudioSource组件

		AudioSource audioSource = soundObj.AddComponent<AudioSource> ();

		//设置AudioSource属性
		audioSource.clip = sfx;
		audioSource.minDistance = 10.0f;
		audioSource.maxDistance = 10.0f;

		//可以用sfxVolume变量控制游戏声音

		audioSource.volume = sfxVolumn;

		//播放声音
		audioSource.Play ();

		//声音播放结束后，删除之前动态生成的游戏对象
		Destroy (soundObj, sfx.length);
	}
}
