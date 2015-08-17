using UnityEngine;
using System.Collections;

public class InitialConfig : MonoBehaviour 
{
	public Texture texture;
	public int numberOfBlocks;
	public int yOffset;
	public int xOffset;

	// Use this for initialization
	void Start () 
	{
		// Arrange borders depending on screen size


		// Create grid of blocks
		for(int i = 0; i < numberOfBlocks; i++)
		{
			//GameObject block = Instantiate(Resources.Load("Block")) as GameObject;
			//block.transform.position = new Vector3(0,7,0);

		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
