using UnityEngine;
using System.Collections;

public class Cart : MonoBehaviour {

    public GameObject player;
    public float cart_fric;
    public float cart_rot;
    public float interaction_radius;

    private float playerRot;
    private float initialY;

    public void SetInFront(float dist)
    {
        this.transform.position = player.transform.forward * dist + player.transform.position;
        this.transform.position = new Vector3(this.transform.position.x, initialY, this.transform.position.z);
    }

    // Use this for initialization
    private void Start () {
        playerRot = -1;
        initialY = this.transform.position.y;
    }

    // Update is called once per frame
    private void Update() {
        this.GetComponent<Rigidbody>().velocity *= cart_fric;
        this.GetComponent<Rigidbody>().angularVelocity *= cart_rot;

        float d = Vector3.Distance(player.transform.position, this.transform.position);
        if (d < interaction_radius)
        {
            playerRot -= player.transform.eulerAngles.y;
            this.transform.rotation = player.transform.rotation;
            this.transform.Rotate(0, 90, 0);
            SetInFront(d);
            //  this.transform.RotateAround(player.transform.position, Vector3.up, -playerRot);
        }
        playerRot = player.transform.eulerAngles.y;
    }
}
