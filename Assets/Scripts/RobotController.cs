// using UnityEngine;
// using UnityEngine.InputSystem;

// [RequireComponent(typeof(Rigidbody))]
// public class RobotDrive : MonoBehaviour
// {
//     [Header("Ustawienia Prędkości")]
//     [Tooltip("Maksymalna prędkość obrotowa kół (w radianach na sekundę).")]
//     public float maxVelocity = 30f;

//     [Header("Referencje do Kół (Configurable Joints)")]
//     public ConfigurableJoint frontLeftWheel;
//     public ConfigurableJoint frontRightWheel;
//     public ConfigurableJoint backLeftWheel;
//     public ConfigurableJoint backRightWheel;

//     void FixedUpdate()
//     {
//         float moveInput = 0f;
//         float turnInput = 0f;

//         var keyboard = Keyboard.current;
//         if (keyboard != null)
//         {
//             if (keyboard.upArrowKey.isPressed) moveInput += 1f;
//             if (keyboard.downArrowKey.isPressed) moveInput -= 1f;

//             if (keyboard.rightArrowKey.isPressed) turnInput += 1f;
//             if (keyboard.leftArrowKey.isPressed) turnInput -= 1f;
//         }

//         // Miksowanie sterowania czołgowego (Tank Drive)
//         float leftSpeed = (moveInput + turnInput) * maxVelocity;
//         float rightSpeed = (moveInput - turnInput) * maxVelocity;

//         // Przekazujemy prędkości do kół
//         SetWheelVelocity(frontLeftWheel, leftSpeed);
//         SetWheelVelocity(backLeftWheel, leftSpeed);

//         SetWheelVelocity(frontRightWheel, rightSpeed);
//         SetWheelVelocity(backRightWheel, rightSpeed);
//     }

//     void SetWheelVelocity(ConfigurableJoint joint, float speed)
//     {
//         if (joint == null) return;

//         joint.targetAngularVelocity = new Vector3(speed, 0f, 0f);
//     }
// }

using UnityEngine;
using UnityEngine.InputSystem;

public class RobotDrive : MonoBehaviour
{
    [Header("Velocity")]
    public float maxVelocity = 2500f;

    [Header("Articulation Bodies References")]
    public ArticulationBody frontLeftWheel;
    public ArticulationBody frontRightWheel;
    public ArticulationBody backLeftWheel;
    public ArticulationBody backRightWheel;

    void FixedUpdate()
    {
        float moveInput = 0f;
        float turnInput = 0f;

        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.upArrowKey.isPressed) moveInput += 1f;
            if (keyboard.downArrowKey.isPressed) moveInput -= 1f;
            if (keyboard.rightArrowKey.isPressed) turnInput += 1f;
            if (keyboard.leftArrowKey.isPressed) turnInput -= 1f;
        }

        float leftSpeed = (moveInput + turnInput) * maxVelocity;
        float rightSpeed = (moveInput - turnInput) * maxVelocity;

        SetWheelSpeed(frontLeftWheel, leftSpeed);
        SetWheelSpeed(backLeftWheel, leftSpeed);
        SetWheelSpeed(frontRightWheel, rightSpeed);
        SetWheelSpeed(backRightWheel, rightSpeed);
    }

    void SetWheelSpeed(ArticulationBody wheel, float speed)
    {
        if (wheel == null) return;
        ArticulationDrive drive = wheel.xDrive;
        drive.targetVelocity = speed;
        wheel.xDrive = drive;
    }
}
