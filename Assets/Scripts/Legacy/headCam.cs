using System.Collections;
using UnityEngine;

public class HeadCam : MonoBehaviour 
{
    public GameObject Head;
    private Quaternion initial;

    // Use this for initialization
    public void Start() 
    {
        this.initial = transform.rotation;
	}
	
	// Update is called once per frame
    public void Update()
    {
        if (this.Head != null)
        {
            this.transform.position = this.Head.GetComponent<Transform>().position;
            this.transform.rotation = Quaternion.Euler(this.initial.eulerAngles + this.Head.GetComponent<Transform>().rotation.eulerAngles);
        }
    }

    /// <summary>
    /// Gets the initial Quaternion.
    /// </summary>
    /// <returns>The initial Quaternion.</returns>
    public Quaternion GetInitial()
    {
        return this.initial;
    }
}
