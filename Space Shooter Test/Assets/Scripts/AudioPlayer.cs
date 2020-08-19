using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioPlayer : Singleton<AudioPlayer>, IAwake
{
	private AudioSource source;

	public void OnAwake()
	{
		source = GetComponent<AudioSource>();
	}

	public void Play(AudioClip clip)
	{
		source.PlayOneShot(clip);
	}
}
