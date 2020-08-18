using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject levelsUI;

	private void Awake()
	{
		LevelLoader.Instance.LoadLevels(levelsUI, this);
	}
	public void LoadLevel(IObjective objective)
	{
		SceneManager.LoadScene(1);

		Objectives.Instance.SetObjective(objective);
	}
}
