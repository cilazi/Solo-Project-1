using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHPControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HPChange(int hp)
    {
        Text text = gameObject.GetComponent<Text>();
        text.text = System.Convert.ToString(hp);
    }
}
