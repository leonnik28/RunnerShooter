using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Dropdown _fpsDropdown;

    private void Start()
    {
        int savedFPS = PlayerPrefs.GetInt("TargetFPS", 0);
        _fpsDropdown.value = savedFPS;
        SetTargetFPS(savedFPS);
       
        _fpsDropdown.onValueChanged.AddListener(delegate {
            SetTargetFPS(_fpsDropdown.value);
            PlayerPrefs.SetInt("TargetFPS", _fpsDropdown.value);
        });
    }

    private void SetTargetFPS(int dropdownValue)
    {
        int targetFPS = 30;

        switch (dropdownValue)
        {
            case 0:
                targetFPS = 30;
                Debug.Log("30");
                break;
            case 1:
                targetFPS = 60;
                Debug.Log("60");

                break;
            case 2:
                Debug.Log("90");
                targetFPS = 90;
                break;
        }

        Application.targetFrameRate = targetFPS;
    }
}
