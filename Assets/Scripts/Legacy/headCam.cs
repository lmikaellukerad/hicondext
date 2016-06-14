using System.Collections;
using UnityEngine;

public class HeadCam : MonoBehaviour 
{
	public GameObject Head;
	public GameObject Camera;
	private Quaternion reset;
	private Vector3 offsetPos;

	// Use this for initialization
	public void Start() 
	{
		this.offsetPos = this.transform.position - Head.transform.position;
	}
	
	// Update is called once per frame
	public void Update()
	{
		if(Input.GetKeyDown("r")){
			Vector3 temp = Camera.transform.localRotation.eulerAngles;
			reset = reset * Quaternion.Euler(new Vector3(0,-temp.y,0));
			this.transform.rotation *= reset;
		}
		if (this.Head != null)
		{
			this.transform.position = this.Head.GetComponent<Transform>().position + offsetPos;
		}
	}
}
