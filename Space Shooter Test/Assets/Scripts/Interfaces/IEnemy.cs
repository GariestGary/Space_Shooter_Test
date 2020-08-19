using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IEnemy
{
	int CurrentHealth { get; }
	void Kill();
	void Hit();
}
