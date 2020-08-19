using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader: Singleton<LevelLoader>
{
	public LevelLoader()
	{
		destroyOnLoad = false;
	}

	[SerializeField] private bool loadLevelsFromPlayerPrefs;

	[SerializeField] private GameObject openedLevelPrefabUI;
	[SerializeField] private GameObject lockedLevelPrefabUI;
	[SerializeField] private GameObject finishedLevelPrefabUI;

	[SerializeField] private List<DefaultObjective> levels = new List<DefaultObjective>();


	public void LoadLevels(GameObject parent, MainMenu menu)
	{
		for (int i = 0; i < levels.Count; i++)
		{
			GameObject level;
			int num = i;

			if (loadLevelsFromPlayerPrefs && PlayerPrefs.HasKey(i + " level"))
			{
				levels[i].SetState((ObjectiveState)PlayerPrefs.GetInt(i + " level")); //Load state from playerprefs
			}

			switch (levels[i].State)
			{
				case ObjectiveState.Opened: 
					level = Instantiate(openedLevelPrefabUI);
					level.GetComponent<Button>().onClick.AddListener(() => { menu.LoadLevel(levels[num]); });
					break;
				case ObjectiveState.Locked:
					level = Instantiate(lockedLevelPrefabUI);
					break;
				case ObjectiveState.Finished:
					level = Instantiate(finishedLevelPrefabUI);
					level.GetComponent<Button>().onClick.AddListener(() => { menu.LoadLevel(levels[num]); });
					break;
				default:
					level = null;
					break;
			}

			if (!level) continue;

			levels[i].SetNumber(i);

			level.transform.SetParent(parent.transform);

			level.GetComponentInChildren<Text>().text = (i + 1).ToString();
		}
	}

	public IObjective GetObjective(int index)
	{
		if (index >= levels.Count) return null;

		return levels[index];
	}
}
