using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int _wealthChange = 0;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private AudioClip _audioOnPickup; //played on pick up

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerWealth = other.GetComponent<PlayerWealth>();
            playerWealth.ChangeWealth(_wealthChange);
            var player = other.GetComponent<PlayerController>();
            player.PlayAudio(_audioOnPickup);
            SelfDestroy();
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime, Space.World);
    }

    private void SelfDestroy()
    {
        //todo:n sound effect
        Destroy(gameObject);
    }
}
