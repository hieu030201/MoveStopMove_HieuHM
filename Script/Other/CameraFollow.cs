using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform playerTF;
    private Vector3 cameraOffset;
    private Vector3 newPos;
    private float desiredRotationY = 20f; // Góc quay mong muốn
    private Quaternion desiredRotation;

    [Range(0.01f, 1f)]
    public float SmoothFactor = 0.5f;

    public Camera Camera;

    private void Awake()
    {
        Camera = Camera.main;
    }
    private void Start()
    {
        cameraOffset = transform.position - playerTF.position;
    }

    private void LateUpdate()
    {
        if(playerTF != null)
        {
            if (GameManager.Ins.IsState(GameState.MainMenu))
            {
                newPos = new Vector3(0, 3.5f, -8);
                desiredRotation = Quaternion.Euler(desiredRotationY, 0, 0);
            }
            else if (!GameManager.Ins.IsState(GameState.MainMenu))
            {
                newPos = playerTF.position + cameraOffset;
                desiredRotation = Quaternion.Euler(65, 0, 0);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, SmoothFactor);
            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        }
      
    }
}