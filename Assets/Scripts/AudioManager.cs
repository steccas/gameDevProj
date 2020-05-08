using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
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
