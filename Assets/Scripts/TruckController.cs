using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;

    public Transform leftWheelMesh;
    public Transform rightWheelMesh;

    public bool motor;
    public bool steering;
}

public class TruckController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    public bool IsActive;

    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform wheelMesh)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        if (!IsActive)
        {
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = 0f;
                    axleInfo.rightWheel.motorTorque = 0f;
                }

                ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftWheelMesh);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightWheelMesh);
            }

            return;
        }
   

        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftWheelMesh);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightWheelMesh);
        }
    }
}