using UnityEngine;
using System.Collections;

public class RGResult : FageEventDispatcher {
	public	RGResultSubject[] subjects;

	void OnEnable() {
		for (int i = 1; i <= 5; i++) {
			string code = PlayerPrefs.GetString ("subject" + i.ToString ());
			SubjectInfo info = SubjectManager.Find (code);
			subjects [i - 1].SetData (info, PlayerPrefs.GetInt ("grade" + i.ToString ()));
		}
	}

	void OnDisable() {
	}

	public	void OnClickRetry() {
		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT)));
	}

	public	void OnClickRefresh() {
		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.UPDATE)));
	}
}
