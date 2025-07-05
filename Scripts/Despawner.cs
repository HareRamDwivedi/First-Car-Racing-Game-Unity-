using UnityEngine;

public class Despawner : MonoBehaviour
{
    [SerializeField] private Transform carTransform;
    [SerializeField] private float offset = -5f;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        if (other.transform.parent != null)
        {
            Debug.Log("Despawning: " + other.transform.parent.gameObject.name);
            Destroy(other.transform.parent.gameObject);
        }
        else
        {
            Debug.Log("No parent found, destroying object directly: " + other.gameObject.name);
            Destroy(other.gameObject);
        }
    }

}
