using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

	private Transform tr;
	private LineRenderer line;

	//获取被射线击中的游戏对象
	private RaycastHit hit;
	// Use this for initialization
	void Start () {

		//分配组件
		tr = GetComponent<Transform> ();

		line = GetComponent<LineRenderer> ();

		//以局部坐标为基准

		line.useWorldSpace = false;

		//游戏运行时禁用
		line.enabled = false;

		//设置头部宽度和尾部宽度
		line.SetWidth (0.3f, 0.01f);
	
	}
	
	// Update is called once per frame
	void Update () {

		//生成射线

		Ray ray = new Ray (tr.position + (Vector3.up * 0.02f), tr.forward);

		//使用以下函数以在场景中显示射线

		Debug.DrawRay (ray.origin, ray.direction * 100, Color.blue);

		if (Input.GetMouseButtonDown (0)) 
		{

			//设置Line Renderer的初始位置

			line.SetPosition (0, tr.InverseTransformPoint (ray.origin));

		//将物体被射线击中的位置设置为line Render的中点位置
			if(Physics.Raycast(ray, out hit,100.0f))
			{
				line.SetPosition (1, tr.InverseTransformPoint (hit.point));
			}
			else
			{
				line.SetPosition (1, tr.InverseTransformPoint (ray.GetPoint (100.0f)));
			}
			//调用绘制激光束的协程函数
			StartCoroutine(this.ShowLaserBeam());
		}
	
	}

	IEnumerator ShowLaserBeam()
	{
		line.enabled = true;
		yield return new WaitForSeconds (Random.Range (0.01f, 0.2f));
		line.enabled = false;
	}


}
