using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IObjective
{
	ObjectiveState State { get; }
	int LevelNumber { get; }
	void Initialize();
	void Save();

	void SetNumber(int num);
	void SetState(ObjectiveState state);
}
