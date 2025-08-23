using Assets._Project.Develop.Runtime.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private const KeyCode NextStep = KeyCode.Space;

    private TutorialService _service;

    public void Initialize(TutorialService service)
    {
        _service = service;

        _service.TextInputed += AddText;
        _service.TextCleared += ClearText;
    }


    private void AddText(string letter)
        => _text.text += letter;

    private void ClearText()
        => _text.text = string.Empty;

    private void OnDestroy()
    {
        _service.TextInputed -= AddText;
    }
}
