using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class playerController : NetworkBehaviour
{
    private CharacterController _characterController;
    private Vector2 _input;
    private float _speed = 5f;
    private PlayerInput _playerInput;
    private float _yVelocity;
    private float _gravity = -9.81f;

    public Material verde;
    public Material rojo;
    public MeshRenderer meshRenderer;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.enabled = false;
    }
   

    void Update()
    {
        if (!IsOwner) return;

        Vector3 move = new Vector3(_input.x, 0, _input.y);
        

        if (_characterController.isGrounded && _yVelocity < 0)
        {
            _yVelocity = -2f;
        }

        _yVelocity += _gravity * Time.deltaTime;
        move.y = _yVelocity;

        _characterController.Move(move * _speed * Time.deltaTime);
    }

    public override void OnNetworkSpawn()
    {
        _playerInput.enabled = IsOwner;

        if (!IsOwner) return;

        if (NetworkManager.LocalClientId == 0)
        {
            meshRenderer.material = verde;
        }
        
        if (NetworkManager.LocalClientId == 1)
        {
            meshRenderer.material = rojo;
        }
        
        /*if (NetworkManager.LocalClientId == 1)
        {
            meshRenderer.material.color = Color.yellow;
        }*/

    }
    /*public void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material = verde;
    }*/


    public override void OnNetworkDespawn()
    {
        _playerInput.enabled = false;
    }

    public void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

}
