using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
	// Public fields
	public float speed;
	public float maxSpeed;
	public GameObject player;
	public GameObject topBorder;
	public float randomBounceDeflect = 5.0f;

	// Private fields
	private GameObject ball;
	private Rigidbody ballRigidBody;
	private bool ballIsActive;
	private Vector2 ballInitialForce;

	// Use this for initialization
	void Start () 
	{
		ballIsActive = false;
		ballInitialForce = new Vector2(0.0f, -300.0f);

		ball = GameObject.Find ("Ball");
		ballRigidBody = ball.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(ballRigidBody.velocity.magnitude > maxSpeed)
		{
			ballRigidBody.velocity = ballRigidBody.velocity.normalized * maxSpeed;
		}

		// check for user input
		if (Input.GetButtonDown ("Jump") == true) 
		{
			// check if is the first play
			if (!ballIsActive)
			{
				ballRigidBody.AddForce(ballInitialForce)	;
				ballIsActive = !ballIsActive;
			}
		}

		if(!ballIsActive && player != null)
		{
			Vector2 ballPosition = ball.transform.position;

			ballPosition.x = player.transform.position.x;
			ball.transform.position = ballPosition;
		}
	}
	

	void OnTriggerEnter(Collider other)
	{
		// If we wall off the edge of the screen, die
		if(other.tag == "DeathBorder")
		{
			Debug.Log("asdad");
			Destroy(ball);
			return;
		}


		// If hit paddle, rebound dependent on location
		if(other.tag == "Paddle")
		{
			Vector2 paddlePosition = other.transform.position;
			Vector2 ballPosition = ball.transform.position;
			
			// Vector pointing from paddle to ball
			Vector2 delta = ballPosition - paddlePosition;
			Vector2 direction = delta.normalized; // Unit vector

			ballRigidBody.velocity = direction * speed;
		}
		else
		{
			SphereCollider ballCollider = GetComponent<SphereCollider>();
			Vector2 newVelocity;

			// To ensure the ball bounces off the left and right wall, you have to only 
			// change the vertical velocity when you hit a block or TopBorder
			if(other.tag == "TopBorder" || other.tag == "Block")
			{
				// If the vertical position of the position plus its radius plus a small safety value proportional 
				// to the radius (to make sure the ball doesn't get stuck or anything), is greater than the object's y value 				
				if(ballRigidBody.position.y + ballCollider.radius + (ballCollider.radius / 10.0f)  > other.transform.position.y)
				{
					newVelocity = new Vector2(ballRigidBody.velocity.x, -ballRigidBody.velocity.y);	
					ballRigidBody.velocity = newVelocity;
				}
			}

			// If side walls then change x direction
			else
			{
				newVelocity = new Vector2(-ballRigidBody.velocity.x, ballRigidBody.velocity.y);
				ballRigidBody.velocity = newVelocity;
			}

			if(other.tag == "Block")
			{
				Destroy(other.gameObject);
				// Play sound
			}
		}
	}
}
