using UnityEngine;
using System.Collections;

public class headCam : MonoBehaviour {
    public GameObject Head;
    private Quaternion initial;

    // Use this for initialization
    void Start () {
        initial = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (Head != null)
        {
            transform.position = Head.GetComponent<Transform>().position;
            transform.rotation = Quaternion.Euler(initial.eulerAngles + Head.GetComponent<Transform>().rotation.eulerAngles);
        }
    }
}
