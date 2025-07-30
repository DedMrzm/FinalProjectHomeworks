using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayCycle : MonoBehaviour
{
    private DIContainer _projectContainer;

    private SymbolsGenerator _generator;
    private SceneSwitcherService _sceneSwitcher;
    private CoroutinesPerformer _coroutinesPerformer;

    public GameplayCycle(DIContainer projectContainer)
    {
        _projectContainer = projectContainer;

        _generator = _projectContainer.Resolve<SymbolsGenerator>();
        _sceneSwitcher = _projectContainer.Resolve<SceneSwitcherService>();
        _coroutinesPerformer = _projectContainer.Resolve<CoroutinesPerformer>();
    }

    private void Awake()
    {
        string allKeys = System.Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().ToString();
        Debug.Log(allKeys);
    }

    public IEnumerator Launch()
    {
        string correctAnswer = _generator.Generate();

        Debug.Log("������� ����� ��������, ��� � ����� ����");

        string allKeys = System.Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().ToString();

        yield return new WaitForSeconds(100);
    }

    public IEnumerator OnGameModeDefeat()
    {
        Debug.Log("�����������, ��������");
        Debug.Log($"����� {KeyCode.Space} ����� ������ ������");

        yield return Input.GetKeyDown(KeyCode.Space);

        _coroutinesPerformer.StartPerform(Launch());
    }

    public IEnumerator OnGameModeWin()
    {
        Debug.Log("�������, �� �������!");
        Debug.Log($"����� {KeyCode.Space} ����� ������� � ������� ����");

        yield return Input.GetKeyDown(KeyCode.Space);

        _sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu);
    }
}
