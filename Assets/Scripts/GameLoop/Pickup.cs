using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int _wealthChange = 0;
    [SerializeField] private float _rotationSpeed = 1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerWealth = other.GetComponentInParent<PlayerWealth>();
            playerWealth.ChangeWealth(_wealthChange);
            SelfDestroy();
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime, Space.World);
    }

    private void SelfDestroy()
    {
        //todo: activate particles and sound effect
        Destroy(gameObject);
    }
}
