using UnityEngine;

public class NpcStateTelegrapher : MonoBehaviour
{
    public AudioSource audioSource;
    public MeshRenderer googleRenderer;
    public Material patrolGoogleMaterial;
    public Material attackGoogleMaterial;
    public Material stunnedGoogleMaterial;
    public Material searchGoogleMaterial;

    public AudioClip[] patrolAudioClips;
    public AudioClip attackAudioClip;
    public AudioClip stunnedAudioClip;
    public AudioClip searchAudioClip;
    
    public void OnStateChanged(State state)
    {
        if (state is PatrolState)
        {
            AudioClip clip = patrolAudioClips[Random.Range(0, patrolAudioClips.Length)];
            TelegraphState(patrolGoogleMaterial, clip, 0.5f, 4f);
        }
        else if (state is AttackState)
        {
            TelegraphState(attackGoogleMaterial, attackAudioClip);
        }
        else if (state is StunnedState)
        {
            TelegraphState(stunnedGoogleMaterial, stunnedAudioClip, 1f, 1f);
        }
        else if (state is SearchState)
        {
            TelegraphState(searchGoogleMaterial, searchAudioClip);
        }
    }

    private void TelegraphState(Material material, AudioClip clip)
    {
        googleRenderer.material = material;
        if (!audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    
    private void TelegraphState(Material material, AudioClip clip, float minDelay, float maxDelay)
    {
        googleRenderer.material = material;
        if (!audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = clip;
        audioSource.PlayDelayed(Random.Range(minDelay, maxDelay));
    }
}
