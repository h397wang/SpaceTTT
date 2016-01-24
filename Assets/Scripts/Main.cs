using UnityEngine;
using System.Collections;
using Leap;

public class Main : MonoBehaviour {

    public static GameLogic logic = new GameLogic();
    int[,,] board = new int[4, 4, 4];
    Controller controller;
    GameObject cube;
    GameObject sphere;
    GameObject box;
    GameObject objects;
    GameObject handController;
    public string xStr;
    public string yStr;
    public string zStr;

    public int curPlayer = 1;
    public int gameOver = 0;


    // Use this for initialization
    void Start () {
        cube = GameObject.Find("Cube");
        box = GameObject.Find("Box");
        objects = GameObject.Find("Objects");
        sphere = GameObject.Find("Sphere");
        handController = GameObject.Find("HandController");
        controller = new Controller();
        controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
        //controller.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", 30.0f);
        controller.Config.SetFloat("Gesture.KeyTap.HistorySeconds", 0.2f);
        //controller.Config.SetFloat("Gesture.KeyTap.MinDistance", 0.6f);

        /*
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
        */

    }


    public Vector3 snapPos;
    Frame lastFrame = Frame.Invalid;
    public GameObject lastMove;
    // Update is called once per frame
    void Update () {
        Frame frame = new Frame();
        frame = controller.Frame();
        GestureList gestures = frame.Gestures(lastFrame);
        lastFrame = frame;
        /*
        for (int i = 0; i < gestures.Count; i++)
        {
            Gesture gesture = gestures[i];
            if (gesture.Type == Gesture.GestureType.TYPE_KEY_TAP && gesture.Hands[0].IsRight)
            {   
                KeyTapGesture tap = new KeyTapGesture(gesture);
                GameObject newCube = Instantiate(cube);
                newCube.transform.position = box.transform.position;
                newCube.transform.rotation = box.transform.rotation;
                newCube.transform.parent = box.transform;
                
                Vector tapPosition = tap.Position;
                Vector3 correctedPos = handController.transform.TransformPoint(tapPosition.ToUnityScaled());
                //correctedPos.y = correctedPos.y - 1.5f;
                //correctedPos.z = correctedPos.z - 0.3f;
                //newCube.transform.position = SnapToGrid(correctedPos);
                snapPos = SnapToGrid(correctedPos);
                SpawnObject(curPlayer, snapPos);
                Vector3 arrayPos = UnityToArrayIndex(snapPos);
                xStr = "X: " + ((int)arrayPos.x).ToString();
                yStr = "Y: " + ((int)arrayPos.y).ToString();
                zStr = "Z: " + ((int)arrayPos.z).ToString();

                Debug.Log(correctedPos + " | " + snapPos);
            }
        }       */

        //Find the current position of the index finger and display it in a text box

        HandList hands = frame.Hands;
        Vector3 currentPos;
        Vector tipPos;
        foreach (Hand hand in hands)
        {
            if (hand.IsRight)
            {
                foreach (Finger finger in hand.Fingers)
                {
                    if ( (int)finger.Type == 1)
                    {
                        for (int i = 0; i < gestures.Count; i++)
                        {
                            Gesture gesture = gestures[i];
                            if (gesture.Type == Gesture.GestureType.TYPE_KEY_TAP && gesture.Hands[0].IsRight)
                            {
                                if (gameOver == 1)
                                {
                                    Destroy(objects);
                                    if (objects != null) objects = GameObject.Find("Objects");
                                    gameOver = 0;
                                    winStr = "";
                                }
                                else
                                {

                                    KeyTapGesture tap = new KeyTapGesture(gesture);
                                    /*newCube.transform.position = box.transform.position;
                                    newCube.transform.rotation = box.transform.rotation;
                                    newCube.transform.parent = box.transform;
                                    */
                                    Vector tapPosition = tap.Position;
                                    Vector3 correctedPos = handController.transform.TransformPoint(tapPosition.ToUnityScaled());
                                    //correctedPos.y = correctedPos.y - 1.5f;
                                    //correctedPos.z = correctedPos.z - 0.3f;
                                    //newCube.transform.position = SnapToGrid(correctedPos);
                                    snapPos = SnapToGrid(correctedPos);
                                    if (snapPos.x > 3f || snapPos.y > 3f || snapPos.z > 3f)
                                    {
                                        Undo(lastMove);
                                        if (curPlayer == 1)
                                        {
                                            Debug.Log("hello");
                                            curPlayer = 2;
                                        }
                                        else
                                            curPlayer = 1;
                                    }
                                    else
                                        SpawnObject(curPlayer, snapPos);
                                    Vector3 arrayPos = UnityToArrayIndex(snapPos);
                                }
                            }
                        }

                        tipPos = finger.TipPosition;
                        currentPos = handController.transform.TransformPoint(tipPos.ToUnityScaled());
                        snapPos = SnapToGrid(currentPos);
                        Vector3 gridPos = UnityToArrayIndex(snapPos);
                        xStr = "X: " + ((int)gridPos.x).ToString();
                        yStr = "Y: " + ((int)gridPos.y).ToString();
                        zStr = "Z: " + ((int)gridPos.z).ToString();
                    }

                    
                }
            }

        }

        
    }

    void SpawnObject(int player, Vector3 position)
    {
        GameObject newObject;
        Vector3 arrayPos = UnityToArrayIndex(snapPos);
        if (player == 1)
        {
            int x = (int)arrayPos.x;
            int y = (int)arrayPos.y;
            int z = (int)arrayPos.z;
            newObject = Instantiate(cube);
            if (!logic.insertMark(curPlayer, x, y, z))
                return;
            Debug.Log(logic.board[0, 0, 0]);
            if (logic.isWin(1))
            {
                Debug.Log("Player 1 Wins");
                Restart();
            }
            curPlayer = 2;
        }
        else
        {
            newObject = Instantiate(sphere);
            if (!logic.insertMark(2, (int)arrayPos.x, (int)arrayPos.y, (int)arrayPos.z))
                return;
            if (logic.isWin(2))
            {
                Debug.Log("Player 2 Wins");
                Restart();
            }

            curPlayer = 1;
        }
        newObject.transform.parent = objects.transform;
        newObject.transform.position = position;
        lastMove = newObject;

    }

    public Vector3 UnityToArrayIndex(Vector3 pv) {
        // Function inputs are the Unity coordinates of a point
        // Maps and outputs the corresponding array coordinate (integer)

        int x = (int)(5.2 / 3 * (pv.x + 0.9));
        int y = (int)(5.2 / 3 * (pv.y + 0.9));
        int z = (int)(5.2 / 3 * (- pv.z + 0.9));

        Vector3 output = new Vector3(x, y, z);
        return output;
    }

    public Vector3 SnapToGrid(Vector3 tapPosition)
    {
        float x = tapPosition.x;
        float y = tapPosition.y;
        float z = tapPosition.z;
        float newX = 0;
        float newY = 0;
        float newZ = 0;

        if (-1.4f < x && x < -0.6f)
            newX = -0.9f;
        else if (-0.6f < x && x < 0f)
            newX = -0.3f;
        else if (0f < x && x < 0.6f)
            newX = 0.3f;
        else if (0.6f < x && x < 1.2f)
            newX = 0.9f;
        else
            newX = 50f;

        if (-1.4f < y && y < -0.6f)
            newY = -0.9f;
        else if (-0.6f < y && y < 0f)
            newY = -0.3f;
        else if (0f < y && y < 0.6f)
            newY = 0.3f;
        else if (0.6f < y && y < 1.2f)
            newY = 0.9f;
        else
            newY = 50f;

        if (-1.4f < z && z < -0.6f)
            newZ = -0.9f;
        else if (-0.6f < z && z < 0f)
            newZ = -0.3f;
        else if (0f < z && z < 0.6f)
            newZ = 0.3f;
        else if (0.6f < z && z < 1.2f)
            newZ = 0.9f;
        else newZ = 50f;

        Vector3 newPosition = new Vector3(newX, newY, newZ);

        return newPosition;
    }
    public string winStr = "";
    void OnGUI()
    {
        GUI.Box(new Rect(60, 60, 120, 120), "Current Player: " + curPlayer + "\n"
            + xStr + 
            "\n" + yStr + "\n" + zStr + "\n" + winStr);
    }

    void Undo(GameObject lastObject)
    {
        Destroy(lastObject);
    }
    void Restart()
    {
        logic.board = new int[4,4,4];
        winStr = "Game Over!\n " + curPlayer + " wins!";
        gameOver = 1;
        curPlayer = 1;
        objects = GameObject.Find("Objects");
    }
}
