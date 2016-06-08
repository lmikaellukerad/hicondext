using UnityEngine;
using System.Collections;

public class Cart : MonoBehaviour {

    public GameObject player;
    public float cart_fric;
    public float cart_rot;
    public float interaction_radius;

    private float playerRot;

    // Use this for initialization
    void Start () {
        playerRot =-1; 
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Rigidbody>().velocity *= cart_fric;
        this.GetComponent<Rigidbody>().angularVelocity *= cart_rot;

        float d = Vector3.Distance(player.transform.position, this.transform.position);
        if (d < interaction_radius)
        {
            //print("COLLISIOOOOOON");
            playerRot -= player.transform.eulerAngles.y;
            this.transform.rotation = player.transform.rotation;
            this.transform.Rotate(0, 90, 0);
            this.transform.RotateAround(player.transform.position, Vector3.up, -playerRot);
        }
        playerRot = player.transform.eulerAngles.y;
    }
}
