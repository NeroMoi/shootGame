using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//此类需要声明属性 [System.Seriallizable]使之可序列化
//显示到jian shi 视图

[System.Serializable]
public class Anim
{
	public AnimationClip idle;
	public AnimationClip runForward;
	public AnimationClip runBackward;
	public AnimationClip runRight;
	public AnimationClip runLeft;

}


public class PlayerCtral : MonoBehaviour {

	private float h = 0.0f;
	private float v = 0.0f;

	/* 必须先分配变量，之后才能使用需要访问的组zu件*/
	private Transform  tr;
	//移动速度变量
	public float moveSpeed = 10.0f;

	//旋转速度变量
	public float rotSpeed = 100.0f;


	//要显示到监视视图的动画变量
	public Anim anim;

	//要访问下列3D模型Animation 组件对象的变量
	public Animation _animation;

	//表示玩家生命值的变量
	public int hp =100;

	//player的生命条图像
	public Image imgHpbar;

	//player的生命值初始值
	private int initHp;

	//访问游戏管理器的变量
	private GameMgr gameMgr;


	//声明委派和事件
	public delegate void PlayerDieHandler();
	public static event PlayerDieHandler OnPlayerDie;


	// Use this for initialization
	void Start () {

		//设置生命值
		initHp = hp;
	/*	float vec1 = Vector3.Magnitude (Vector3.forward);//获取向量大小.
		float vec2 = Vector3.Magnitude (Vector3.forward + Vector3.right);
		float vec3 = Vector3.Magnitude ((Vector3.forward + Vector3.right).normalized);

		Debug.Log (vec1);
		Debug.Log (vec2);
		Debug.Log (vec3); */
		//向脚本初始部分分配Transform组件
		/*从该脚本包含的游戏对象拥有的各组件中抽取Transform组件，并保存至tr变量*/
		tr = GetComponent<Transform>();

		//获取GameMgr脚本
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameMgr> ();

		//查找

		_animation = GetComponentInChildren<Animation> ();
		//保存并运行Animation组件的动画片段
		_animation.clip = anim.idle;
		_animation.Play ();
	
	}

	// Update is called once per frame
	void Update () {

	//	transform.position += new Vector (0, 0, 1); //每帧移动1

		//tr.Translate (Vector3.forward * moveSpeed *v *Time.deltaTime,Space.Self); // 其中deltatime 表示前一帧到当前帧花费的time

	
		h = Input.GetAxis("Horizontal"); //水平的
		v = Input.GetAxis("Vertical"); // 垂直的

		Debug.Log ("H = " + h.ToString()); //需要显示的字符
		Debug.Log ("V = " + v.ToString());

		Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);


		tr.Translate (moveDir.normalized * Time.deltaTime *moveSpeed, Space.Self);

		/*以vector3.up 轴为基准，以rotSpeed速度旋转*/
		tr.Rotate(Vector3.up *Time.deltaTime * rotSpeed *Input.GetAxis("Mouse X"));

		if (v >= 0.1f) {
			_animation.CrossFade(anim.runForward.name,0.3f);
		}
	
		else if( v <= -0.1f){
			_animation.CrossFade(anim.runBackward.name,0.3f);
		}
		else if( h >= 0.1f){
			_animation.CrossFade(anim.runRight.name,0.3f);
		}
		else if( h <= -0.1f){
			_animation.CrossFade(anim.runLeft.name,0.3f);
		}
		else{
			_animation.CrossFade(anim.idle.name,0.3f);

		}

	}

	void OnTriggerEnter(Collider coll)
	{
		//如果发生碰撞的collider为怪兽的punch,则减少玩家的hp
		if (coll.gameObject.tag == "PUNCH") 
		{
			hp -= 10;

			//调整Image UI元素的fillAmount 属性， 以调整生命条长度
			imgHpbar.fillAmount = (float)hp / (float) initHp;
			Debug.Log("Player HP = " + hp.ToString());

			//玩家生命值小于0时进行死亡处理

			if(hp <= 0 )
			{
				PlayerDie();
			}
		}
	}
	//玩家的死亡历程

	void PlayerDie()
	{
		Debug.Log("Player Die!");

		//获取所有拥有Monster tag的游戏对象
	//	GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

		//依次调用所有怪兽的onPlayerDie 

	//	foreach (GameObject monster in monsters) 
	//	{
	//		monster.SendMessage ("OnPlayerDie",SendMessageOptions.DontRequireReceiver);
	//	}
		OnPlayerDie ();

		//更新游戏管理器的isGameOver 变量值以停止生成怪兽
		//gameMgr.isGameOver = true;

		//访问GameMgr的单例并更改其isGameover的值
		GameMgr.instance.isGameOver = true;
	}
}
