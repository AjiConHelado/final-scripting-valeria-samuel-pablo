using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource scr;

    [SerializeField] private AudioClip sword, whipe;

    public AudioClip Sword
    {
        get
        {
            return sword;
        }
    }
    
    public AudioClip Whipe
    {
        get
        {
            return whipe;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        scr.PlayOneShot(clip);
    }
}
