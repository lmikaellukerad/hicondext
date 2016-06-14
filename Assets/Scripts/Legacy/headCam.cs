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
		if(Input.GetKeyDown("r")){
		}
		if (this.Head != null)
		{
			this.transform.position = this.Head.GetComponent<Transform>().position;
		}
	}

	private void Reset()
	{
		Vector3 temp = Camera.transform.localRotation.eulerAngles;
		reset = reset * Quaternion.Euler(new Vector3(0, -temp.y, 0));
		this.transform.rotation *= reset;
		Vector3 tempPosition = this.transform.position;
		Transform tempParent = Camera.transform.parent;
		Camera.transform.parent = null;
		this.transform.position = Camera.transform.position;
		Camera.transform.parent = tempParent;
		this.transform.position = tempPosition;
	}

}
