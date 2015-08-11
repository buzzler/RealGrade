using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RGSelect : FageStateMachine {
	public	GameObject	groupHidden;
	public	GameObject	groupEssence;
	public	GameObject	groupScience;
	public	GameObject	groupSocial;
	public	Text		textError;
	public	int			required;

	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			OnClickPrev();
		}
	}

	void OnEnable() {
		if (PlayerPrefs.HasKey("subject1")) {
			if (PlayerPrefs.HasKey("category")) {
				int c = PlayerPrefs.GetInt("category");
				if (c == 1) {
					OnClickScience();
				} else if (c == 2) {
					OnClickSocial();
				}
			}

			Toggle[] toggles = groupEssence.GetComponentsInChildren<Toggle>();
			foreach (Toggle t in toggles) {
				t.isOn = false;
			}
			toggles = groupScience.GetComponentsInChildren<Toggle>();
			foreach (Toggle t in toggles) {
				t.isOn = false;
			}
			toggles = groupSocial.GetComponentsInChildren<Toggle>();
			foreach (Toggle t in toggles) {
				t.isOn = false;
			}

			for (int i = 1 ; i <= required ; i++) {
				string str = "subject" + i.ToString();
				if (PlayerPrefs.HasKey(str)) {
					string code = PlayerPrefs.GetString(str);
					GameObject go = GameObject.FindWithTag(code);
					if (go != null) {
						go.GetComponent<Toggle>().isOn = true;
					}
				}
			}
		}
	}

	public	void OnClickScience() {
		PlayerPrefs.SetInt("category", 1);
		groupScience.SetActive (true);
		groupSocial.SetActive (false);
	}

	public	void OnClickSocial() {
		PlayerPrefs.SetInt("category", 2);
		groupScience.SetActive (false);
		groupSocial.SetActive (true);
	}

	public	void OnToggle() {
		int count = 0;
		Toggle[] toggles;

		if (groupScience.activeSelf) {
			toggles = groupScience.GetComponentsInChildren<Toggle>();
		} else {
			toggles = groupSocial.GetComponentsInChildren<Toggle>();
		}
		foreach (Toggle toggle in toggles) {
			if (toggle.isOn) {
				count++;
			}
		}

		toggles = groupHidden.GetComponentsInChildren<Toggle>();
		foreach (Toggle toggle in toggles) {
			if (toggle.isOn) {
				count++;
			}
		}

		toggles = groupEssence.GetComponentsInChildren<Toggle>();
		foreach (Toggle toggle in toggles) {
			if (toggle.isOn) {
				count++;
			}
		}

		if (count == required) {
			textError.gameObject.SetActive(false);
		}
	}

	public	void OnClickPrev() {
		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT)));
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

		toggles = groupHidden.GetComponentsInChildren<Toggle> ();
		foreach (Toggle toggle in toggles) {
			if (toggle.isOn) {
				selected.Add(toggle);
				PlayerPrefs.SetString("subject" + i.ToString(), toggle.gameObject.name);
				i++;
			}
		}

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

		string str = "subject"+i.ToString();
		if (PlayerPrefs.HasKey(str)) {
			PlayerPrefs.DeleteKey(str);
		}

		if (selected.Count != required) {
			textError.gameObject.SetActive(true);
			return;
		}

		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.INPUT)));
	}
}
