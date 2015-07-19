using UnityEngine;
using System.Collections;

public class RGInput : FageStateMachine {
	public	RGInputSubject		itemPrefab;
	public	RGInputSubject[]	subjects;

	void OnEnable() {
		for (int i = 1 ; i <= 5 ; i++) {
			string code = PlayerPrefs.GetString("subject"+i.ToString());
			SubjectInfo info = SubjectManager.Find(code);
			subjects[i-1].SetSubjectInfo(info);
		}
	}

	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			OnClickPrev();
		}
	}

	void OnDisable() {

	}

	public	void OnClickPrev() {
		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT)));
	}

	public	void OnClickNext() {
		foreach (RGInputSubject subject in subjects) {
			string code = subject.GetSubjectInfo().subject.ToString();
			int score = subject.GetScore();
			PlayerPrefs.SetInt(code, score);
		}

		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.UPDATE)));
	}
}
