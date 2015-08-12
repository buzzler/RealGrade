using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RGResultSubject : MonoBehaviour {
	public	Text		textName;
	public	Text		textCategory;
	public	Text[]		textGrade;
	private	string		_encoded;

	public	void SetData(SubjectInfo info, string encoded, Color first, Color other) {
		textName.text = info.name;
		textCategory.text = info.tag;
		textCategory.transform.parent.gameObject.SetActive(!string.IsNullOrEmpty(info.tag));
		_encoded = encoded;

		string en = _encoded;
		for (int i = 0; i < textGrade.Length; i++) {
			string c = en [i].ToString ();
			textGrade [i].text = (c == "0") ? "" : c;
		}

		textGrade[0].color = (textGrade[0].text == "1") ? first:other;
	}


}
