using System.Collections;
using UnityEngine;

public class AttackState : State
{
    public float moveSpeed = 5f;
    public float attackDistance = 1f;
    public float maxChaseDistance = 10f;
    public State targetLostState;
    public State targetKilledState;
    public AudioClip attackAudioClip;
    public AudioClip attackBarkAudioClip;
    public AudioSource audioSource;
    
    private NpcController _controller;
    private NpcKnowledge _knowledge;
    private bool _isKilling;
    
    private void Start()
    {
        _controller = GetComponent<NpcController>();
        _knowledge = GetComponent<NpcKnowledge>();
    }

    public override void OnEnter()
    {
        _isKilling = false;
    }

    public override void OnUpdate()
    {
        if (_isKilling)
            return;
        
        if (!_knowledge.HasTarget())
        {
            TrySetState(targetLostState);
            return;
        }

        var target = _knowledge.target;
        var position = transform.position;
        var targetPosition = target.position;
        float distance = Vector3.Distance(position, targetPosition);
        if (distance < attackDistance)
        {
            _controller.speed = 0f;
            AttackTarget();
        }
        else if (distance > maxChaseDistance)
        {
            TrySetState(targetLostState);
        }
        else
        {
            _controller.direction = targetPosition - position;
            _controller.speed = moveSpeed;
        }
    }

    private void AttackTarget()
    {
        var target = _knowledge.target;
        PlayerController controller = target.GetComponent<PlayerController>();
        if (controller == null)
        {
            Debug.LogWarning($"Attack Target {target.name} does not have a PlayerController");
            TrySetState(targetLostState);
        }
        controller.Kill();
        _isKilling = true;
        StartCoroutine(PlayAudio());
    }

    private IEnumerator PlayAudio()
    {
        if(audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = attackAudioClip;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = attackBarkAudioClip;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        TrySetState(targetKilledState);
    }
    
    private void OnDrawGizmos()
    {
        if (_knowledge == null || !_knowledge.HasTarget() || !IsActive()) return;

        Color color = new Color(1f, 0.05f, 0.09f);
        var targetPosition = _knowledge.target.position;
        var position = transform.position;
        // DrawGizmo.DrawCircle(targetPosition, 1.0f, color);
        Gizmos.color = color;
        Gizmos.DrawLine(position, targetPosition);
        DrawGizmo.DrawCircle(position, maxChaseDistance, new Color(1f, 0.87f, 0.14f));
    }
}
