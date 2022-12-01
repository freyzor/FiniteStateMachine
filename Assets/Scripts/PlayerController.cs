using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // based on CharacterController Move example
    // you can take a look at that to see how to add jump handling
    // https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    public float speed = 0f;
    public float gravityValue = -9.8f;

    public Transform spawnPoint;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move.Normalize();
        _controller.Move(move * (Time.deltaTime * speed));

        // always facing the movement direction
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        
        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
        Physics.SyncTransforms();
    }

    public void Kill()
    {
        Teleport(spawnPoint.position);
    }
}
