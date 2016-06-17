using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Matthias
/// This class controls a cart. 
/// </summary>
public class CartControl : MonoBehaviour
{
    public GameObject Player;
    public float CartFric;
    public float CartRot;
    public float InteractionRadius;
    public float MaxCos;

    private float initialY;
    private float playerRot;

    public float PlayerRot
    {
        get { return this.playerRot; }
    }

    public float InitialY
    {
        get { return this.initialY; }
    }

    /// <summary>
    /// Checks if the player is within a certain range of the cart.
    /// </summary>
    /// <returns>Boolean b</returns>
    public bool PlayerIsInRange()
    {
        float distance = Vector3.Distance(this.Player.transform.position, this.transform.position);
        if (distance < this.InteractionRadius)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the player is behind the cart.
    /// </summary>
    /// <returns>Boolean b</returns>
    public bool PlayerIsBehind()
    {
        Vector3 direction = (this.transform.position - this.Player.transform.position).normalized;
        float cosA = Vector3.Dot(direction, -this.transform.right.normalized);
        if (cosA > this.MaxCos)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Moves the cart and repositions it in case of rotation.
    /// </summary>
    public void MoveCart()
    {
        // rotate cart
        this.playerRot -= this.Player.transform.eulerAngles.y;
        this.transform.rotation = this.Player.transform.rotation;
        this.transform.Rotate(0, 90, 0);

        // move cart
        this.GetComponent<Rigidbody>().velocity = this.Player.transform.forward * this.InteractionRadius;
        this.transform.position = new Vector3(this.transform.position.x, this.initialY, this.transform.position.z);
    }

    /// <summary>
    /// Applies friction to a moving cart.
    /// </summary>
    public void ApplyFriction()
    {
        this.GetComponent<Rigidbody>().velocity *= this.CartFric;
        this.GetComponent<Rigidbody>().angularVelocity *= this.CartRot;
    }

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public void Init()
    {
        this.playerRot = -1;
        this.initialY = this.transform.position.y;
    }

    // Use this for initialization
    private void Start()
    {
        this.Init();
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.PlayerIsInRange() && this.PlayerIsBehind())
        {
            this.MoveCart();
        }

        this.ApplyFriction();
        this.playerRot = this.Player.transform.eulerAngles.y;
    }
}
