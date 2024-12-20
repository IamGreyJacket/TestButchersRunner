using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    public PlayerAnimator PlayerAnimator => _playerAnimator;

    private GameManager _gameManager => GameManager.Instance;

    [Space, SerializeField, Min(0f)] private float _speed = 1f;
    [SerializeField, Min(0f)] private float _sideSpeed = 0.2f;
    [SerializeField, Min(1f)] private float _maxSideSpeed = 1f;
    [SerializeField, Min(0f)] private float _turnTime = 1f;
    [SerializeField, Min(1f)] private float _turnSpeed = 1f; //speed while turning
    [SerializeField] private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;


    private bool _canWalk = false;
    private bool _isTurning = false;
    private float _sideInput = 0;

    private void OnEnable()
    {
        if (_gameManager == null) return;
        _gameManager.OnLevelStarted += StartWalking;
    }

    private void OnDisable()
    {
        if (_gameManager == null) return;
        _gameManager.OnLevelStarted -= StartWalking;
    }

    private void Start()
    {
        OnEnable();
    }

    void Update()
    {
        var pointerDelta = Pointer.current.delta.value;
        if (pointerDelta.x != 0 && Input.GetMouseButton(0) && EventSystem.current.isFocused)
        {
            _sideInput = pointerDelta.x;
        }
        else _sideInput = 0;
        if (Input.GetKey(KeyCode.R))
        {
            StartWalking();
        }
        else if (Input.GetKey(KeyCode.F))
        {
            StopWalking();
        }
    }

    public void StartWalking()
    {
        _canWalk = true;
        StartCoroutine(Walk());
        //_playerAnimator.PlayWalk();
        //

    }

    public void StopWalking()
    {
        _canWalk = false;
        //_playerAnimator.PlayIdle();
    }

    public void StartTurn(bool turnRight)
    {
        StartCoroutine(Turn(turnRight));
    }

    private IEnumerator Turn(bool turnRight)
    {
        _isTurning = true;

        var time = _turnTime;

        float degrees = -90;
        if (turnRight) degrees = 90;

        var startAngles = transform.eulerAngles;
        var targetAngles = startAngles;
        targetAngles.y += degrees;

        while(time > 0)
        {
            time -= Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(targetAngles, startAngles, time / _turnTime);
            yield return null;
        }
        _isTurning = false;
        transform.eulerAngles = targetAngles;
        yield return null;
    }

    private IEnumerator Walk()
    {
        
        while (_canWalk)
        {
            if (_isTurning) _sideInput = 0;

            var direction = Vector3.ClampMagnitude(transform.right *
                _sideInput * _sideSpeed * Time.deltaTime, _maxSideSpeed);
            _rigidbody.linearVelocity = (transform.forward + direction) * _speed;
            yield return null;
        }
        yield return null;
    }
}
