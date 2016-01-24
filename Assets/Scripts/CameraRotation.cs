using UnityEngine;
using Leap;

public class CameraRotation : MonoBehaviour
{

    /// <summary>
    /// The Leap controller.
    /// </summary>
    Controller controller;
    
    /// <summary>
    /// The current frame captured by the Leap Motion.
    /// </summary>
    Frame CurrentFrame
    {
        get { return (IsReady) ? controller.Frame() : null; }
    }

    /// <summary>
    /// Gets the hands data captured from the Leap Motion.
    /// </summary>
    /// <value>
    /// The hands data captured from the Leap Motion.
    /// </value>
    HandList Hands
    {
        get { return (CurrentFrame != null && CurrentFrame.Hands.Count > 0) ? CurrentFrame.Hands : null; }
    }

    /// <summary>
    /// Gets a value indicating whether the Leap Motion is ready.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is ready; otherwise, <c>false</c>.
    /// </value>
    bool IsReady
    {
        get { return (controller != null && controller.Devices.Count > 0 && controller.IsConnected); }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        controller = new Controller();
    }

    /// <summary>
    /// Update function called every frame.
    /// </summary>
    float curYaw;
    float curYRotation;
    void Update()
    {
        Hand leftHand; // The front most hand captured by the Leap Motion Controller

        // Check if the Leap Motion Controller is ready
        if (!IsReady || Hands == null)
        {
            return;
        }
        leftHand = Hands.Leftmost;
        if (leftHand.IsLeft && leftHand.Direction.Yaw > curYaw && leftHand.Direction.Yaw > 0.1)
        {
            curYRotation += leftHand.Direction.Yaw * 15;
            transform.rotation = Quaternion.Euler(0, curYRotation, 0);
        }
        //For relative orientation
        //transform.rotation *= Quaternion.Euler( leftHand.Direction.Pitch, leftHand.Direction.Yaw, leftHand.PalmNormal.Roll );
        curYaw = leftHand.Direction.Yaw;

 

        
    }

    

}