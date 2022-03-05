using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    /// <summary>
    /// Sound effects, music, does not matter as you call the clip via its name.
    /// </summary>
    public AudioClip[] audioClips;
    private Dictionary<string, AudioClip> allClips;

    private AudioSource musicPlayer;
    private List<AudioSource> soundPlayers;

    private void Awake()
    {
        allClips = new Dictionary<string, AudioClip>();
        
        foreach (AudioClip clip in audioClips)
        {
            if (!clip)
            {
                continue;
            }
            if (allClips.ContainsKey(clip.name))
            {
                throw new System.Exception("There are two clips with the name: '" + clip.name + "'");
            }
            allClips.Add(clip.name, clip);
        }
        soundPlayers = new List<AudioSource>();
        musicPlayer = gameObject.AddComponent<AudioSource>();
        musicPlayer.loop = true;
    }

    private void PlayAudio(AudioSource source, string clipName)
    {
        if (!allClips.ContainsKey(clipName))
        {
            Debug.Log("The audioclip '" + clipName + "' does not exist." );
            return;
        }

        if (source)
        {
            source.Stop();
            source.clip = allClips[clipName];
            source.Play();
        }
    }

    public void PlayMusic(string clipName)
    {
        if (!musicPlayer.clip || !musicPlayer.clip.name.Equals(clipName))
        {
            PlayAudio(musicPlayer, clipName);
        }
    }

    public void PlaySound(string clipName)
    {
        AudioSource player = null;
        foreach (AudioSource source in soundPlayers)
        {
            if (source && !source.isPlaying)
            {
                player = source;
            }
        }

        if (player == null)
        {
            player = gameObject.AddComponent<AudioSource>();
            player.loop = false;
            soundPlayers.Add(player);
        }

        PlayAudio(player, clipName);
    }
}
