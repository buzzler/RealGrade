using UnityEngine;
using System.Collections;

public class ConnectionManager : FageStateMachine {
	public	bool IsOnline() {
		if (current is ConnectionPing) {
			return (current as ConnectionPing).IsOnline();
		} else {
			return false;
		}
	}
}
