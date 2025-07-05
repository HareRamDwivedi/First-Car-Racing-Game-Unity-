using System.Security.Cryptography;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform carTransform;
    [SerializeField] private float offset = -5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.z = carTransform.position.z + offset;
        transform.position = cameraPos;
    }
}
