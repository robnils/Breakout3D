using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
	// Public fields
	public float speed;
	public float maxSpeed;
	public GameObject player;
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
		// If hit paddle, rebound dependent on location
		if(other.tag == "Paddle")
		{
			Vector2 paddlePosition = other.transform.position;
			Vector2 ballPosition = ball.transform.position;
			
			// Vector pointing from paddle to ball
			Vector2 delta = ballPosition - paddlePosition;
			Vector2 direction = delta.normalized; // Unit vector
			
			Vector2 trueVelocity = direction * speed; // Untampered velocity

			ballRigidBody.velocity = new Vector2(trueVelocity.x + Mathf.Sign(delta.x) * Random.Range(-randomBounceDeflect, randomBounceDeflect)
			                                     ,trueVelocity.y + Mathf.Sign(delta.y) * Random.Range(-randomBounceDeflect, randomBounceDeflect)
			                                	); // Add random components so the ball doesn't get stuck in a loop
		}
		else
		{
			// If hitting side walls, same velocity but negative x
			if(other.tag == "Border")
			{
				Debug.Log("hit left or right wall");
				Vector2 newVelocity = new Vector2(-ballRigidBody.velocity.x, ballRigidBody.velocity.y);

				ballRigidBody.velocity = newVelocity;
			}

			// If hitting top wall, same velocity but negative y
			else if (other.tag == "TopBorder")
			{
				Vector2 newVelocity = new Vector2(ballRigidBody.velocity.x, -ballRigidBody.velocity.y);
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
