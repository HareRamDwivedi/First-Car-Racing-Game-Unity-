using System.Security.Permissions;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [Header("Car Settings")]
    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float steeringAngle = 35f;
    [SerializeField] private float brakeForce = 3000f;
    [SerializeField] private Transform centerOfMass;

    [Header("UI Manager Script")]
    [SerializeField] private UIM_Scrip uiManager;

    private float horizontalInput;
    private float verticalInput;
    private bool isBraking;

    private Rigidbody carRigidbody;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = centerOfMass.localPosition;
        carRigidbody.interpolation = RigidbodyInterpolation.Interpolate; // Smooth physics
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        HandleBraking();
        UpdateWheels();
        PowerStearing();
        UnityEngine.Debug.Log("Speed: " + CarSpeed());
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    private void HandleSteering()
    {
        float currentSteerAngle = steeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void HandleBraking()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            frontLeftWheelCollider.brakeTorque = brakeForce;
            frontRightWheelCollider.brakeTorque = brakeForce;
            rearLeftWheelCollider.brakeTorque = brakeForce;
            rearRightWheelCollider.brakeTorque = brakeForce;
            carRigidbody.linearDamping = 1.6f;
        }
        else
        {
            frontLeftWheelCollider.brakeTorque = 0;
            frontRightWheelCollider.brakeTorque = 0;
            rearLeftWheelCollider.brakeTorque = 0;
            rearRightWheelCollider.brakeTorque = 0;
            carRigidbody.linearDamping = 0f;
        }
    }
    private void PowerStearing()
    {
        if (horizontalInput == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 1f);
        }
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    public float CarSpeed()
    {
        return carRigidbody.linearVelocity.magnitude * 2.369594945f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TrafficVehicles"))
        {
            //UnityEngine.Debug.Log("Game Over");
            //Time.timeScale = 0f;
            uiManager.GameOver();   

        }
    }
}