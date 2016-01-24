public Vector3 UnityToArrayIndex(Vector3 pv){} 
	// Function inputs are the Unity coordinates of a point
	// Maps and outputs the corresponding array coordinate (integer)
	
	int x = 5/3 * (pv.transform.position.x+0.9);
	int y = 5/3 * (pv.transform.position.y - 0.3);
	int z = 4 - (5/3 * (pv.transform.position.z + 0.9));
	
	Vector3 output = (x,y,z);
	return output;
}
