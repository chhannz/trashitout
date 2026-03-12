using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip gameOverSound;

    public void PlayPickUpSound()
    {
        if (pickUpSound != null)
        {
            sfxSource.PlayOneShot(pickUpSound);
        }
    }

    public void PlayGameOverSound()
    {
        if (gameOverSound != null)
        {
            bgmSource.Stop();
            sfxSource.PlayOneShot(gameOverSound);
        }
    }
}
