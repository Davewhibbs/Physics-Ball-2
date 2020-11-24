using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip pop1, pop2, pop3, bouncy1, bouncy2, bouncy2Quiet;
    static AudioSource audioSrc;

    


    // Start is called before the first frame update
    void Start()
    {
        pop1 = Resources.Load<AudioClip>("pop1");
        pop2 = Resources.Load<AudioClip>("pop2");
        pop3 = Resources.Load<AudioClip>("pop3");
        bouncy1 = Resources.Load<AudioClip>("bouncy1");
        bouncy2 = Resources.Load<AudioClip>("bouncy2");
        bouncy2Quiet = Resources.Load<AudioClip>("bouncy2Quiet");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "pop1":
                audioSrc.PlayOneShot(pop1);
                break;

            case "pop2":
                audioSrc.PlayOneShot(pop2);
                break;

            case "pop3":
                audioSrc.PlayOneShot(pop3);
                break;

            case "bouncy1":
                audioSrc.PlayOneShot(bouncy1);
                break;

            case "bouncy2":
                audioSrc.PlayOneShot(bouncy2);
                break;

            case "bouncy2Quiet":
                audioSrc.PlayOneShot(bouncy2);
                break;
        }
    }

    /*public static void StopSound(string clip)
    {
        switch (clip)
        {
            case "pop1":
                audioSrc.Stop(pop1);
                break;

            case "pop2":
                audioSrc.Stop(pop2);
                break;

            case "pop3":
                audioSrc.Stop(pop3);
                break;

            case "bouncy1":
                audioSrc.Stop(bouncy1);
                break;

            case "bouncy2":
                audioSrc.Stop(bouncy2);
                break;
        }
    }*/
}
