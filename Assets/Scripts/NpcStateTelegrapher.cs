using UnityEngine;

public class NpcStateTelegrapher : MonoBehaviour
{
    public AudioSource audioSource;
    public MeshRenderer goggleRenderer;
    public Material patrolGoggleMaterial;
    public Material attackGoggleMaterial;
    public Material stunnedGoggleMaterial;
    public Material searchGoggleMaterial;

    public AudioClip[] patrolAudioClips;
    public AudioClip attackAudioClip;
    public AudioClip stunnedAudioClip;
    public AudioClip searchAudioClip;
    
    public void OnStateChanged(State state)
    {
        if (state is PatrolState)
        {
            AudioClip clip = patrolAudioClips[Random.Range(0, patrolAudioClips.Length)];
            TelegraphState(patrolGoggleMaterial, clip, 0.5f, 4f);
        }
        else if (state is AttackState)
        {
            TelegraphState(attackGoggleMaterial, attackAudioClip);
        }
        else if (state is StunnedState)
        {
            TelegraphState(stunnedGoggleMaterial, stunnedAudioClip, 1f, 1f);
        }
        else if (state is SearchState)
        {
            TelegraphState(searchGoggleMaterial, searchAudioClip);
        }
    }

    private void TelegraphState(Material material, AudioClip clip)
    {
        goggleRenderer.material = material;
        if (!audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    
    private void TelegraphState(Material material, AudioClip clip, float minDelay, float maxDelay)
    {
        goggleRenderer.material = material;
        if (!audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = clip;
        audioSource.PlayDelayed(Random.Range(minDelay, maxDelay));
    }
}
