using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed;
	public float boundary;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Boundary
		if(Mathf.Abs(transform.position.x) > boundary)
		{
			int sign = (transform.position.x > 0) ? 1 : -1;

			transform.position = new Vector3(sign * boundary, transform.position.y, 0);
		}

		else
		{		
			if(Input.GetKey("d"))
			{
				transform.Translate(Vector3.right * Time.deltaTime * speed); 
			}

			if(Input.GetKey("a"))
			{
				transform.Translate(Vector3.left * Time.deltaTime * speed); 
			}	
		}
	}
}
