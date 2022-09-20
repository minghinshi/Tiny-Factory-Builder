using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler instance;

    [SerializeField] private AudioSource audioSource;
    private bool isPlaying = false;

    public AudioClip placementSound;
    public AudioClip destroySound;
    public AudioClip pickUpSound;
    public AudioClip craftingSound;
    public AudioClip errorSound;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        isPlaying = false;
    }

    public void PlaySound(AudioClip sound, bool randomizePitch = true)
    {
        if (isPlaying) return;
        isPlaying = true;
        SetPitch(randomizePitch);
        audioSource.PlayOneShot(sound);
    }

    private void SetPitch(bool randomizePitch)
    {
        audioSource.pitch = randomizePitch ? Random.Range(0.9f, 1.1f) : 1f;
    }
}
