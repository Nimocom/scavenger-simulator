using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] Transform target;

    [SerializeField] float movementSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] OffsetData[] offsetData;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 calculatedPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, calculatedPosition, movementSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform target, int type)
    {
        this.target = target;

        for (int i = 0; i < offsetData.Length; i++)
        {
            if (offsetData[i].Type == type)
            {
                offset = offsetData[i].Offset;
                break;
            }
        }
    }
}

[System.Serializable]
public struct OffsetData
{
    public int Type;
    public Vector3 Offset;
}