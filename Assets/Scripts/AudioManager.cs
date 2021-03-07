using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioManager
{

	public static AudioManager Instance { get; private set; }

	public static AudioManager GetInstance()
	{
		if (Instance == null)
		{
			//Instance = gameObject.AddComponent<AudioManager>();
			//Instance = this;
			Instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
		}
		return Instance;
	}

	public AudioMixerGroup mixerGroup;

	public Audio[] audios;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		foreach (Audio s in audios)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

	private void Start()
	{
		Play("MainTheme");
		//Play("Encounter");
		//Play("Encounter");
	}

	public void Play(string audio)
	{
		Audio s = AudioFind(audio);
		if (s == null) return;

		float volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		float pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.volume = volume;
		s.source.pitch = pitch;

		s.source.Play();
	}

	public void Stop(string audio)
	{
		Audio s = AudioFind(audio);
		if (s == null) return;

		s.source.Stop();
	}

	public void FadeOut(string audio)
    {
		Audio s = AudioFind(audio);
		if (s == null) return;
		AudioSource source = s.source;
		StartCoroutine(FadeOut(source, 1.5f));
	}

	public void FadeIn(string audio)
	{
		Audio s = AudioFind(audio);
		if (s == null) return;
		AudioSource source = s.source;
		StartCoroutine(FadeIn(source, 1.5f));
	}

	private static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

			yield return null;
		}

		audioSource.Stop();
		audioSource.volume = startVolume;
	}

	private static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
	{
		float startVolume = 0.2f;

		audioSource.volume = 0;
		audioSource.Play();

		while (audioSource.volume < 1.0f)
		{
			audioSource.volume += startVolume * Time.deltaTime / FadeTime;

			yield return null;
		}

		audioSource.volume = 1f;
	}

	private Audio AudioFind(string audio)
	{
		Audio r = Array.Find(audios, item => item.name == audio);
		if (r == null)
		{
			Debug.LogWarning("Audio: " + name + " not found!");
			return null;
		}
		return r;
	} 
}
