using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class RGClass : FageEventDispatcher {
	public	Text[] texts;

	public	void OnEnable() {
		GodRoot root = GodRoot.instance;
		DateTime today = DateTime.Today;
		Exam[] exams = new Exam[root.examGroups.Length];
		int i = 0;

		foreach (ExamGroup eg in root.examGroups) {
			Exam last = null;
			foreach (Exam ex in eg.exams) {
				TimeSpan ts = today.Subtract(ex.date);
				if (ts.TotalMinutes >= 0) {
					if (last!=null) {
						if (last.date.Subtract(ex.date).TotalMinutes < 0) {
							last = ex;
						}
					} else {
						last = ex;
					}
				}
			}
			exams[i++] = last;
		}

		i = 0;
		foreach (Text t in texts) {
			t.text = exams[i++].name;
		}
	}

	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public	void OnClick1() {
		PlayerPrefs.SetInt("selected", 1);
		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT1)));
	}

	public	void OnClick2() {
		PlayerPrefs.SetInt("selected", 2);
		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT2)));
	}

	public	void OnClick3() {
		PlayerPrefs.SetInt("selected", 3);
		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT3)));
	}
}
