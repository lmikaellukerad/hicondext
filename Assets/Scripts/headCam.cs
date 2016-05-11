using UnityEngine;
using System.Collections;

public class headCam : MonoBehaviour {
    public GameObject Head;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Head != null)
        {
            transform.position = Head.GetComponent<Transform>().position;
            transform.rotation = Head.GetComponent<Transform>().rotation;
        }
    }
}
