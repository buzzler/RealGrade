using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RGInputSubject : MonoBehaviour {
	public	Text		textName;
	public	Text		textCategory;
	public	Text		textScore;
	private	int			_score;
	private	SubjectInfo	_info;

	void Update() {
		textScore.text = _score.ToString ();
	}

	public	void SetSubjectInfo(SubjectInfo info) {
		_info = info;
		textName.text = info.name;
		textCategory.text = info.category;
		textCategory.transform.parent.gameObject.SetActive(!string.IsNullOrEmpty(info.category));

		if (PlayerPrefs.HasKey (info.subject.ToString ())) {
			_score = Mathf.Clamp (PlayerPrefs.GetInt (info.subject.ToString ()), 0, info.max);
		} else {
			_score = (int)((float)info.max * 0.8f);
		}
	}

	public	void OnClickPlus() {
		_score = Mathf.Clamp (_score + 1, 0, _info.max);
	}

	public	void OnClickMinus() {
		_score = Mathf.Clamp (_score - 1, 0, _info.max);
	}

	public	SubjectInfo GetSubjectInfo() {
		return _info;
	}

	public	int GetScore() {
		return _score;
	}
}
