using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Objectives: Singleton<Objectives>
{
	public IObjective CurrentObjective { get; private set; }

	public void SetObjective(IObjective objective)
	{
		CurrentObjective = objective;
	}

	public void Initialize()
	{
		CurrentObjective.Initialize();
	}
}