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

	[SerializeField] private GameObject openedlLevelPrefabUI;
	[SerializeField] private GameObject lockedlLevelPrefabUI;
	[SerializeField] private GameObject finishedlLevelPrefabUI;

	[SerializeField] private List<DefaultObjective> levels = new List<DefaultObjective>();


	public void LoadLevels(GameObject parent, MainMenu menu)
	{
		for (int i = 0; i < levels.Count; i++)
		{
			GameObject level;
			int num = i;

			switch (levels[i].State)
			{
				case ObjectiveState.Opened: 
					level = Instantiate(openedlLevelPrefabUI);
					level.GetComponent<Button>().onClick.AddListener(() => { menu.LoadLevel(levels[num]); });
					break;
				case ObjectiveState.Locked:
					level = Instantiate(lockedlLevelPrefabUI);
					break;
				case ObjectiveState.Finished:
					level = Instantiate(finishedlLevelPrefabUI);
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
		return levels[index];
	}
}
