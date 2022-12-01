using UnityEngine;

public class Stunner : MonoBehaviour
{
    public AudioSource audioSource;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        Debug.Log($"Stunner Detected {other.name}");
        StunnedState state = other.GetComponent<StunnedState>();
        if (state == null) return;
        
        state.Activate();
        
        audioSource.Play();
        
        // gameObject.SetActive(false);
        Vector2 randomPos2D = Random.insideUnitCircle * 15f;
        Vector3 displacement = new Vector3(randomPos2D.x, 0, randomPos2D.y);
        transform.position = displacement;
    }
}
