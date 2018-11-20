using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	public Transform targetTr; //要追踪的游戏对象的Transform变量
	public float dist = 10.0f; //与摄像机之间的距离
	public float height = 3.0f;//设置摄像机的高度
	public float dampTrace = 20.0f; //实现平滑追踪的变量

	//摄像机本身的TransFrom变量
	private Transform tr;


	// Use this for initialization
	void Start () {
	
		//将摄像机本身的Transform组件分配至tr
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//使用lateupdate函数，调用所有update函数后才会调用该函数
	//要追踪的目标游戏对象停止移动后，调用LateUpdate 函数

	void LateUpdate(){

		//将摄像机放置在最终目标后方的dist距离的位置
		//将摄像机向上抬高height

		tr.position = Vector3.Lerp (tr.position,
		                            targetTr .position
									- (targetTr.forward * dist)
									+ (Vector3.up * height),
		                            Time.deltaTime * dampTrace
									);
		//使摄像机朝向游戏对象
		tr.LookAt (targetTr.position);
	}

}
