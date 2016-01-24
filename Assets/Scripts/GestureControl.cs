using UnityEngine;
using System.Collections;
using Leap;

public class GestureControl : MonoBehaviour {

    Controller controller;
    GameObject cube;
    GameObject box;

    // Use this for initialization
    void Start () {
        cube = GameObject.Find("Cube");
        box = GameObject.Find("Box");
        controller = new Controller();
        controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
        //controller.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", 30.0f);
        controller.Config.SetFloat("Gesture.KeyTap.HistorySeconds", 0.2f);
        //controller.Config.SetFloat("Gesture.KeyTap.MinDistance", 0.6f);

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k <4; k++)
                {
                    SpawnObject(1, (i + 1) * 0.6f - 1.5f, (j + 1) * 0.6f - 1.5f, (k + 1) * 0.6f - 1.5f);
                }
            }
        }
    }
    Frame lastFrame = Frame.Invalid;
    // Update is called once per frame
    void Update () {
        Frame frame = controller.Frame();
        GestureList gestures = frame.Gestures(lastFrame);
        lastFrame = frame;

        for (int i = 0; i < gestures.Count; i++)
        {
            Gesture gesture = gestures[i];
            if (gesture.Type == Gesture.GestureType.TYPE_KEY_TAP && gesture.Hands[0].IsRight)
            {
                KeyTapGesture tap = new KeyTapGesture(gesture);
                GameObject newCube = Instantiate(cube);
                newCube.transform.parent = box.transform;
                Vector tapPosition = tap.Position;
                newCube.transform.position = tapPosition.ToUnityScaled(false) * 10;

                Vector3 correctedPos = newCube.transform.position;
                correctedPos.y = correctedPos.y - 1.5f;
                correctedPos.z = correctedPos.z - 0.3f;
                newCube.transform.position = correctedPos;
                Debug.Log(correctedPos);
            }
        }
    }

    void SpawnObject(int player, float x, float y, float z)
    {
        GameObject newCube = Instantiate(cube);
        newCube.transform.parent = box.transform;
        Vector3 position = newCube.transform.position;
        position.x = x;
        position.y = y;
        position.z = z;
        newCube.transform.position = position;
    }

}
