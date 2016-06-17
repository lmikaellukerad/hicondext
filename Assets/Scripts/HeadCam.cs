using System.Collections;
using UnityEngine;

public class HeadCam : MonoBehaviour 
{
	public GameObject Head;
	public GameObject Camera;
	private Quaternion reset;

	// Use this for initialization
	public void Start() 
	{
	}
	
	// Update is called once per frame
	public void Update()
	{
		if (this.Head != null)
		{
			this.transform.position = this.Head.GetComponent<Transform>().position;
		}
	}
}
