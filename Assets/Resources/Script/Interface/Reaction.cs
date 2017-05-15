using UnityEngine;
using System.Collections;

public interface Reaction {

	void ApplyReaction ();
	bool TriggerReaction(Action action);
}
