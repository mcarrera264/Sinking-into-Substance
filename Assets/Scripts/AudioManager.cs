using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioSource musicSource;

    public AudioClip clickSound;
    public AudioClip[] randomSoundClips;
    public AudioClip[] randomClickClips;

    private float userVolume = 1;
    public bool inverseMute = false;

    public bool fadeInAwake = true;
    private bool isFading = false;
    private float fadeDuration = 1.0f;


    private void Awake()
    {
        if (fadeInAwake)
            FadeIn();


    }

    public void SetAudioMixer(AudioMixerGroup amg)
    {
        soundSource.outputAudioMixerGroup = amg;

    }

    public void PlaySound(AudioClip audioClip)
    {
        soundSource.PlayOneShot(audioClip);
    }

    public void PlayClickSound()
    {
        PlaySound(clickSound);

    }

    public void PlayRandomSound()
    {
        int index = Random.Range(0, randomSoundClips.Length);

        PlaySound(randomSoundClips[index]);

    }

    public void PlayRandomClick()
    {
        int index = Random.Range(0, randomClickClips.Length);

        PlaySound(randomClickClips[index]);

    }

    public void MuteMusic(bool boolean)
    {
        if (inverseMute)
            boolean = !boolean;
        musicSource.mute = boolean;

    }

    public void SetUserVolume(float volume)
    {
        userVolume = volume;
        musicSource.volume = userVolume;

    }

    public void SwitchMusic(AudioClip toMusic)
    {
        if (!toMusic)
        {
            Debug.Log("Music file to switch to was not found");
            return;

        }

        StartCoroutine(SwitchMusicCoroutine(toMusic));

    }

    public void FadeIn()
    {
        if (isFading)
            return;

        StartCoroutine(FadeMusicCoroutine(0.0f, userVolume));

    }

    public void FadeOut()
    {
        if (isFading)
            return;

        StartCoroutine(FadeMusicCoroutine(userVolume, 0.0f));

    }

    private IEnumerator FadeMusicCoroutine(float startVolume, float targetVolume)
    {
        isFading = true;

        float elapsedTime = 0.0f;
        float volumeValue = targetVolume;

        while (elapsedTime < fadeDuration)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            volumeValue = Mathf.Lerp(startVolume, targetVolume, normalizedTime);
            musicSource.volume = volumeValue;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isFading = false;
        musicSource.volume = targetVolume;

    }

    private IEnumerator SwitchMusicCoroutine(AudioClip toMusic)
    {
        yield return StartCoroutine(FadeMusicCoroutine(userVolume, 0.0f));

        musicSource.Stop();
        musicSource.clip = toMusic;
        musicSource.Play();

        yield return StartCoroutine(FadeMusicCoroutine(0.0f, userVolume));
    }
}
