using UnityEngine;
using System.Collections;

public class MyGizmo : MonoBehaviour {

	public Color _color = Color.yellow;
	public float _radius = 0.1f;
	// Use this for initialization
	void OnDrawGizmos()
	{
		//设置Gizmos的颜色
		Gizmos.color = _color;
		//创建球体Gizmos,函数参数为（Gizmos的位置，半径）
		Gizmos.DrawSphere (transform.position, _radius);
	}
}
