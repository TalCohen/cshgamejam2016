using UnityEngine;
using System.Collections;

public class GoToPlay : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("J1A") || Input.GetButtonDown("J2A"))
        {
            Application.LoadLevel("MainGame");
        }
    }

    void OnClick()
    {
       
    }
}
