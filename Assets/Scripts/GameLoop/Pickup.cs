using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int _wealthChange = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerWealth = other.GetComponentInParent<PlayerWealth>();
            playerWealth.ChangeWealth(_wealthChange);
            SelfDestroy();
        }
    }

    private void SelfDestroy()
    {
        //todo: activate particles and sound effect
        Destroy(gameObject);
    }
}
