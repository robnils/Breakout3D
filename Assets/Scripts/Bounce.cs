using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
	// Public fields
	public float speed;
	public float maxSpeed;
	public GameObject player;

	// Private fields
	private GameObject ball;
	private Rigidbody ballRigidBody;
	private int normal; // -1 is up, 1 is up
	private float angle; 
	private bool ballIsActive;
	private Vector2 ballInitialForce;
	
	private float Vx;
	private float Vy;
	
	// Use this for initialization
	void Start () 
	{
		//speed = 2.0f;
		//normal = -1;
		//angle = 0.0f;
		//Vx = 0.0f;
		//Vy = speed * -1.0f;

		ballIsActive = false;
		ballInitialForce = new Vector2(0.0f, -300.0f);

		ball = GameObject.Find ("Ball");
		ballRigidBody = ball.GetComponent<Rigidbody>();

		//Vector2 initialDirection = Vector2.down;
		//ballRigidBody.velocity = initialDirection * speed;
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
		
		/*
		//ball.transform.Translate(Time.deltaTime * speed * angle,Time.deltaTime * speed * normal,0);

		float xPrev = ball.transform.position.x;
		float yPrev = ball.transform.position.y;
		/*ball.transform.position = new Vector3(Time.deltaTime * Vx ,
		                                      Time.deltaTime * Vy ,
		                                      0);
		                                      */
		//ball.transform.Translate(Time.deltaTime * Vx,Time.deltaTime * Vy,0);

	}
	

	void OnTriggerEnter(Collider other)
	{

		Vector2 paddlePosition = other.transform.position;
		Vector2 ballPosition = ball.transform.position;
		
		// Vector pointing from paddle to ball
		Vector2 delta = ballPosition - paddlePosition;
		Vector2 direction = delta.normalized; // Unit vector
		
		ballRigidBody.velocity = direction * speed;


		if(other.tag == "Block")
		{
			Destroy(other.gameObject);
			// Play sound
		}
	}

	/*
	void OnTriggerEnter(Collider other)
	{
		// Change x component dependent on the distance between paddle and ball

		/*
		float paddleX = other.transform.position.x;
		// MeshRenderer mesh = other.GetComponent<MeshRenderer>();
		//float paddleWidth = mesh.bounds.size.x;
		float paddleWidth = 2.0f;
		
		float ballX = ball.transform.position.x;
		float relativeIntersect = ballX - (paddleX + (paddleWidth / 2.0f));
		float normalisedRelativeIntersect = relativeIntersect / (paddleWidth / 2.0f);
		float bounceAngle = normalisedRelativeIntersect * maxBounceAngle;
				
		Vx = speed * Mathf.Cos(bounceAngle);
		Vy = speed * -Mathf.Sin (bounceAngle);
		*/

		



		/*
		float xDistance = ballRigidBody.position.x - other.transform.position.x;
		float yDistance = ballRigidBody.position.y - other.transform.position.y;

		ballRigidBody.velocity = new Vector3(
			ballRigidBody.velocity.x + xDistance + Mathf.Sign(xDistance) * Random.Range(0.1f, 0.2f),
			ballRigidBody.velocity.y + yDistance + Mathf.Sign(yDistance) * Random.Range(0.1f, 0.2f),
			0);

		if(other.tag == "Paddle")
		{

		}

		if(other.tag == "Border")
		{
			// Change x component dependent on the distance between paddle and ball

		}

		if(other.tag == "DeathBorder")
		{
			Destroy(ball);
		}

		if(other.tag == "Block")
		{
			Destroy(other.gameObject);
			// Play sound
		}
	}
	*/
}
