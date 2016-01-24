using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class splashloader : MonoBehaviour {
    public float delayTime = 3.0f;
    public bool done = false;
    public float timer;
	// Use this for initialization
	void Start () {
        timer = delayTime;

	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            SceneManager.LoadScene(1);

        }
	}
}
