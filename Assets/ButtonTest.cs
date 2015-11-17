using UnityEngine;
using System.Collections;

public class ButtonTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        Application.LoadLevelAsync(Application.loadedLevelName);
        //gameObject.SetActive(false);
        gameObject.transform.Translate(new Vector3(0, 600, 0));
    }
}
