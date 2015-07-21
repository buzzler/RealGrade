using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RGSelect : FageStateMachine {
	public	GameObject	groupEssence;
	public	GameObject	groupScience;
	public	GameObject	groupSocial;
	public	Text		textError;

	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void OnEnable() {
		if (PlayerPrefs.HasKey("subject5")) {
			SubjectInfo info = SubjectManager.Find(PlayerPrefs.GetString("subject5"));
			if (info.category == SubjectCategory.SCIENCE) {
				OnClickScience();
			} else if (info.category == SubjectCategory.SOCIETY) {
				OnClickSocial();
			}

			Toggle[] toggles = GetComponentsInChildren<Toggle>();
			foreach (Toggle t in toggles) {
				t.isOn = false;
			}

			for (int i = 1 ; i <= 5 ; i++) {
				GameObject go = GameObject.FindWithTag(PlayerPrefs.GetString("subject" + i.ToString()));
				if (go != null) {
					go.GetComponent<Toggle>().isOn = true;
				}
			}
		}
	}

	public	void OnClickScience() {
		groupScience.SetActive (true);
		groupSocial.SetActive (false);
	}

	public	void OnClickSocial() {
		groupScience.SetActive (false);
		groupSocial.SetActive (true);
	}

	public	void OnToggle() {
		Toggle[] toggles;
		if (groupScience.activeSelf) {
			toggles = groupScience.GetComponentsInChildren<Toggle>();
		} else {
			toggles = groupSocial.GetComponentsInChildren<Toggle>();
		}

		int count = 0;
		foreach (Toggle toggle in toggles) {
			if (toggle.isOn) {
				count++;
			}
		}

		if (count == 2) {
			textError.gameObject.SetActive(false);
		}
	}

	public	void OnClickNext() {
		int i = 1;
		Toggle[] toggles;
		List<Toggle> selected = new List<Toggle> ();
		toggles = groupEssence.GetComponentsInChildren<Toggle> ();
		foreach (Toggle toggle in toggles) {
			if (toggle.isOn) {
				selected.Add(toggle);
				PlayerPrefs.SetString("subject" + i.ToString(), toggle.gameObject.name);
				i++;
			}
		}

		PlayerPrefs.SetString ("subject" + i.ToString(), SubjectCode.ENG.ToString ());
		i++;

		if (groupScience.activeSelf) {
			toggles = groupScience.GetComponentsInChildren<Toggle> ();
		} else {
			toggles = groupSocial.GetComponentsInChildren<Toggle> ();
		}
		foreach (Toggle toggle in toggles) {
			if (toggle.isOn) {
				selected.Add(toggle);
				PlayerPrefs.SetString("subject" + i.ToString(), toggle.gameObject.name);
				i++;
			}
		}

		if (selected.Count != 4) {
			textError.gameObject.SetActive(true);
			return;
		}

		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.INPUT)));
	}
}
