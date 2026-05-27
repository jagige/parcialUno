using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class playerController : NetworkBehaviour
{
    private CharacterController _characterController;
    private Vector2 _input;
    private float _speed = 5f;
    private PlayerInput _playerInput;
    //private float _yVelocity;
    //private float _gravity = -9.81f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.enabled = false;
    }
   

    void Update()
    {
        Vector3 Move = new Vector3(_input.x, 0, _input.y);
        _characterController.Move(Move * _speed * Time.deltaTime);
    }

    public override void OnNetworkSpawn()
    {
        _playerInput.enabled = IsOwner;
    }

    public override void OnNetworkDespawn()
    {
        _playerInput.enabled = false;
    }

    public void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

}
