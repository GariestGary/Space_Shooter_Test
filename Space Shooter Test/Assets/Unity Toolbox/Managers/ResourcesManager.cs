using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(menuName = "Toolbox/Managers/Resources")]
public class ResourcesManager : ManagerBase, IExecute
{
    [SerializeField] private string PrefabsFolderName;
    [SerializeField] private string SpritesFolderName;
    [SerializeField] private string AudioFolderName;

	private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
	private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
	private Dictionary<string, AudioClip> audio = new Dictionary<string, AudioClip>();

	private Dictionary<Type, object> dictionaries = new Dictionary<Type, object>();

	public void OnExecute()
	{
		InitializeDictionaries();

		LoadPrefabs();
	}

	private void InitializeDictionaries()
	{
		dictionaries.Add(typeof(GameObject), prefabs);
		dictionaries.Add(typeof(Sprite), sprites);
		dictionaries.Add(typeof(AudioClip), audio);
	}

	private void LoadPrefabs()
	{
		GameObject[] prefabs = Resources.LoadAll<GameObject>(PrefabsFolderName);

		foreach (var prefab in prefabs)
		{
			try
			{
				this.prefabs.Add(prefab.name, prefab);
			}
			catch
			{
				throw new AssertionException("Invalid file in corresponding " + PrefabsFolderName + " folder", "");
			}
		}
	}

	public T GetResourceByName<T>(string name)
	{
		dictionaries.TryGetValue(typeof(T), out object targetDict);

		Dictionary<string, T> dict = targetDict as Dictionary<string, T>;

		if(dict.TryGetValue(name, out T value))
		{
			return value;
		}
		else
		{
			return default(T);
		}
	}
}
