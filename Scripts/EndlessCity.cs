using System.Security.Cryptography;
using UnityEngine;

public class EndlessCity : MonoBehaviour
{

    [SerializeField] Transform carTransform;
    [SerializeField] Transform otherCityTransform;
    [SerializeField] float halfLength = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(carTransform.position.z > (transform.position.z + halfLength + 15f))
        {
            transform.position = new Vector3(0,0, otherCityTransform.position.z + halfLength * 2);
        }
    }
}
