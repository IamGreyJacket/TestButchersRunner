using UnityEngine;

public class TurnTrigger : MonoBehaviour
{
    [SerializeField] private bool _turnRight = true;
    private bool _used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_used) return;
            var playerController = other.GetComponentInParent<PlayerController>();
            playerController.StartTurn(_turnRight);
            _used = true;
        }
    }
}