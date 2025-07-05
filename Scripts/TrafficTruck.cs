using System.Security.Cryptography;
using UnityEngine;

public class TrafficTruck : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,speed* Time.deltaTime);
    }
}
