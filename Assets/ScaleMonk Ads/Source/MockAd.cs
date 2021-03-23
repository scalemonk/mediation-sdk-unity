using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MockAd : MonoBehaviour
{
    public Button closeButton;

    public Text title;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        title.text = text;
    }
}
