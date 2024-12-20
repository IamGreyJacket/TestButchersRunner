using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameManager GameManager => GameManager.Instance;
    [Space, SerializeField, Min(0)] private int _requiredLevel = 1;
    [SerializeField, Min(1f)] private float _winMultiplier = 1.5f; 
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerWealth = other.GetComponentInParent<PlayerWealth>();
            Debug.Log(playerWealth.WealthLevel >= _requiredLevel);
            if (playerWealth.WealthLevel >= _requiredLevel) OpenGate();
            else
            {
                GameManager.WinGame();
            }
        }
    }

    public void OpenGate()
    {
        _animator.SetTrigger("Open");
        //todo: start animation to open gate
    }

    public void EndGame()
    {

        //todo
    }
}
