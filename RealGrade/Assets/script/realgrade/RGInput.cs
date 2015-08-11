using UnityEngine;
using System.Collections;

public class RGInput : FageStateMachine {
	public	RGInputSubject		itemPrefab;
	public	Transform			grid;
	public	RGInputSubject[]	subjects;

	void OnEnable() {
		int total = 0;
		for (int i = 1 ; i <= 20 ; i++) {
			if (!PlayerPrefs.HasKey("subject"+i.ToString())) {
				total = i - 1;
				break;
			}
		}

		subjects = new RGInputSubject[total];
		for (int i = 1 ; i <= total ; i++) {
			RGInputSubject item = GameObject.Instantiate<RGInputSubject>(itemPrefab);
			item.transform.SetParent(grid, false);
			subjects[i-1] = item;

			string code = PlayerPrefs.GetString("subject"+i.ToString());
			SubjectInfo info = SubjectManager.Find(code);
			item.SetSubjectInfo(info);
		}
	}

	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			OnClickPrev();
		}
	}

	void OnDisable() {
		int total = subjects.Length;
		for (int i = total-1 ; i >= 0 ; i--) {
			GameObject.Destroy(subjects[i].gameObject);
		}
		subjects = null;
	}

	public	void OnClickPrev() {
		switch (PlayerPrefs.GetInt("selected")) {
		case 1:
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT1))); break;
		case 2:
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT2))); break;
		case 3:
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT3))); break;
		}
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
