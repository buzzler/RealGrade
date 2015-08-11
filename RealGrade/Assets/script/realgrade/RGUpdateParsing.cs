using UnityEngine;
using System.Collections;

public class RGUpdateParsing : FageState {
	private	string _raw;

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		RGUpdate fsm = stateMachine as RGUpdate;
		fsm.SetPercentage (66);
		fsm.SetMessage ("실시간 최신 정보 가져오는 중");
		_raw = PlayerPrefs.GetString ("raw");
		PlayerPrefs.DeleteKey ("raw");
	}
	
	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
	}
	
	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		RGUpdate fsm = stateMachine as RGUpdate;
		if (_raw == null) {
			return;
		}

		int selected = PlayerPrefs.GetInt("selected");
		int total = GodRoot.instance.subjectGroups[selected-1].GetSubjectCount();

		string[] lines = _raw.Replace ("\r\n", "\n").Replace ("\r", "\n").Split ("\n" [0]);
		for (int i = 0 ; i < total ; i++) {
			string code = lines[i*9];
			SubjectInfo info = SubjectManager.Find(code);
			Debug.Log(code);
			for (int j = 1 ; j <= 8 ; j++) {
				info.SetData(lines[i*9+j]);
			}
		}

//		string[] lines = _raw.Replace ("\r\n", "\n").Replace ("\r", "\n").Split ("\n" [0]);
//		int i;
//		SubjectInfo info;
//
//		info = SubjectManager.Find ("KOR_A");
//		for (i = 3; i <= 10; i++) {
//			info.SetData (lines [i]);
//		}
//
//		info = SubjectManager.Find ("KOR_B");
//		for (i = 12; i <= 19; i++) {
//			info.SetData (lines [i]);
//		}
//
//		info = SubjectManager.Find ("MAT_A");
//		for (i = 21; i <= 28; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("MAT_B");
//		for (i = 30; i <= 37; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("ENG");
//		for (i = 39; i <= 46; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("PHY_I");
//		for (i = 48; i <= 55; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("CHM_I");
//		for (i = 57; i <= 64; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("BIO_I");
//		for (i = 66; i <= 73; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("GEO_I");
//		for (i = 75; i <= 82; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("PHY_II");
//		for (i = 84; i <= 91; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("CHM_II");
//		for (i = 93; i <= 100; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("BIO_II");
//		for (i = 102; i <= 109; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("GEO_II");
//		for (i = 111; i <= 118; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("LIF");
//		for (i = 120; i <= 127; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("REL");
//		for (i = 129; i <= 136; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("HIS_K");
//		for (i = 138; i <= 145; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("MAP_K");
//		for (i = 147; i <= 154; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("MAP_W");
//		for (i = 156; i <= 163; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("HIS_A");
//		for (i = 165; i <= 172; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("HIS_W");
//		for (i = 174; i <= 181; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("LAW");
//		for (i = 183; i <= 190; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("ECO");
//		for (i = 192; i <= 199; i++) {
//			info.SetData (lines [i]);
//		}
//		
//		info = SubjectManager.Find ("CUL");
//		for (i = 201; i <= 208; i++) {
//			info.SetData (lines [i]);
//		}

		_raw = null;
		fsm.ReserveState ("RGUpdateCalculate");
	}
}
