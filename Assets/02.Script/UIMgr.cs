using UnityEngine;
using System.Collections;

public class UIMgr : MonoBehaviour {

	public void OnClickStartBtn(RectTransform rt)
	{
		Debug.Log ("Click Buttion");
		Application.LoadLevel("scLevel101");
		Application.LoadLevelAdditive("scPlay");


		//Debug.Log ("Scalex ："+ rt.localScale.x.ToString());
	}
}
