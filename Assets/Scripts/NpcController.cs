using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class NpcController : MonoBehaviour
{
    // based on CharacterController Move example
    // you can take a look at that to see how to add jump handling
    // https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    public Vector3 direction;
    public float speed = 0f;
    public float gravityValue = -9.8f;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        direction = transform.forward;
    }

    void Update()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        direction.y = 0;
        Vector3 move = direction.normalized;
        
        _controller.Move(move * (Time.deltaTime * speed));

        // always facing the movement direction
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        
        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}
