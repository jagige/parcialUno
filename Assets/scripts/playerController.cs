using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector2 _input;
    private float _speed = 5f;
    //private float _yVelocity;
    //private float _gravity = -9.81f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
   

    void Update()
    {
        Vector3 Move = new Vector3(_input.x, 0, _input.y);
        _characterController.Move(Move * _speed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

}
