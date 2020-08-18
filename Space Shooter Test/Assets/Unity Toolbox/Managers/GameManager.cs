using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Toolbox/Managers/Game Manager", fileName = "Game")]
public class GameManager: ManagerBase
{
	public GameObject Player { get; private set; }

	public void SetPlayer(GameObject player)
	{
		Player = player;
	}
}