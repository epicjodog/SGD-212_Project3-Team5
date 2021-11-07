using UnityEngine.Audio;
using System;
using UnityEngine;
/// <summary>
/// Manages audio either for a whole scene or just a GameObject, created by Josiah Holcom (with help from Brackys)
/// </summary>
public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	[SerializeField] bool isSingleton = false;

	void Awake()
	{
		if (isSingleton)
        {
			if (instance != null)
			{
				Destroy(gameObject);
			}
			else
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			

		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;
			s.source.spatialBlend = s.spatialBlend;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}
	/// <summary>
	/// Plays the selected sound
	/// </summary>
	/// <param name="sound"></param>
	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
	/// <summary>
	/// Stops playing the selected sound
	/// </summary>
	/// <param name="sound"></param>
	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}

		s.source.Stop();
	}
	/// <summary>
	/// Changes the pitch at runtime
	/// </summary>
	/// <param name="sound"></param>
	/// <param name="pitch"></param>
	public void ChangePitch(string sound, float pitch)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}

		s.source.pitch = pitch;
	}
	/// <summary>
	/// Changes the volume at runtime
	/// </summary>
	/// <param name="sound"></param>
	/// <param name="volume"></param>
	public void ChangeVolume(string sound, float volume)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}

		s.source.volume = volume;
	}
}
