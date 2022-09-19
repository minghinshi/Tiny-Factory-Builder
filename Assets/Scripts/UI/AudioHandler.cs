using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip placementSound;
    [SerializeField] private AudioClip destroySound;

    private void Awake()
    {
        instance = this;
    }

    public void RandomizePitch()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
    }

    public void PlayPlacement()
    {
        RandomizePitch();
        audioSource.PlayOneShot(placementSound);
    }

    public void PlayDestroy()
    {
        RandomizePitch();
        audioSource.PlayOneShot(destroySound);
    }
}
