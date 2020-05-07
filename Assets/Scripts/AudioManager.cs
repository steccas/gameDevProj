using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance { get; private set; }

	public AudioMixerGroup mixerGroup;

	public Audio[] audios;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
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
		//Play("MainTheme");
		Play("Encounter");
	}

	public void Play(string audio)
	{
		Audio s = Array.Find(audios, item => item.name == audio);
		if (s == null)
		{
			Debug.LogWarning("Audio: " + name + " not found!");
			return;
		}

		float volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		float pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.volume = volume;
		s.source.pitch = pitch;

		s.source.Play();
	}

}
