using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Helpers
{
	public static IEnumerable<GameObject> OfPlayerState(this IEnumerable<GameObject> self, params PlayerState[] states)
	{
		return self.Where ((o) => states.Contains(o.GetComponent<PlayerController> ().State));
	}

	public static IEnumerable<T> AsComponent<T>(this IEnumerable<GameObject> self)
	{
		return self.Select ((o) => o.GetComponent<T>());
	}
}