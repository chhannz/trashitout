using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Speaker Components")] 
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;
    
    [Header ("SFX")]
    [SerializeField] private AudioClip trashPickupClip;
    [SerializeField] private AudioClip gameOverClip;

    public void PlayTrashSFX()
    {
        if (trashPickupClip != null)
        {
            sfxSource.PlayOneShot(trashPickupClip);
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGameOverSFX()
    {
        bgmSource.Stop();
        sfxSource.PlayOneShot(gameOverClip);
    }
}
