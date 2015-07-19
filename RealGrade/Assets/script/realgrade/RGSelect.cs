using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RGSelect : FageStateMachine {
	public	GameObject groupEssence;
	public	GameObject groupScience;
	public	GameObject groupSocial;

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

		if (count >= 2) {
			foreach (Toggle toggle in toggles) {
				if (!toggle.isOn) {
					toggle.interactable = false;
				}
			}
		} else {
			foreach (Toggle toggle in toggles) {
				if (!toggle.isOn) {
					toggle.interactable = true;
				}
			}
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
			return;
		}

		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.INPUT)));
	}
}
