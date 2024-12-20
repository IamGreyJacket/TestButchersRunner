using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        PlayIdle();
    }

    public void PlayWalk()
    {
        _animator.SetTrigger("Walk");
    }

    public void PlayIdle()
    {
        _animator.SetTrigger("Idle");
    }

    public void PlayAngry()
    {
        _animator.SetTrigger("Angry");
    }

    public void PlayDance()
    {
        _animator.SetTrigger("Dance");
    }
}
