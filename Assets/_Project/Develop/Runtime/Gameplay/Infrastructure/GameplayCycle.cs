using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GameplayCycle
{
    private const KeyCode NextCode = KeyCode.Space;
    private const KeyCode RestartCode = KeyCode.Space;
    private const KeyCode GoToMenuCode = KeyCode.Space;

    private DIContainer _projectContainer;

    private SymbolsGenerator _generator;
    private SceneSwitcherService _sceneSwitcher;
    private CoroutinesPerformer _coroutinesPerformer;
    private WalletService _walletService;

    private string _correctAnswer;
    private int _counter = 0;

    private bool _isRunning = false;

    public GameplayCycle(DIContainer projectContainer)
    {
        _projectContainer = projectContainer;

        _walletService = _projectContainer.Resolve<WalletService>();
        _generator = _projectContainer.Resolve<SymbolsGenerator>();
        _sceneSwitcher = _projectContainer.Resolve<SceneSwitcherService>();
        _coroutinesPerformer = (CoroutinesPerformer)_projectContainer.Resolve<ICoroutinesPerfomer>();
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        if (WinConditionsCompleted(_counter))
        {
            _coroutinesPerformer.StartPerform(ProcessWin());
            return;
        }

        if (DefeatConditionsCompleted(_counter))
        {
            _coroutinesPerformer.StartPerform(ProcessDefeat());
            return;
        }

        if (Input.inputString == _correctAnswer[_counter].ToString())
        {
            Debug.Log("Right");
            _counter++;
        }

        //Debug.Log($"Counter: {_counter}\nCorrectAnswerLength {_correctAnswer.Length}");
    }

    public IEnumerator Launch()
    {
        _counter = 0;
        Debug.Log($"����� ������, ������� {NextCode}");

        yield return new WaitWhile(() => Input.GetKeyDown(NextCode) == false);

        Debug.Log("������� ����� ��������, ��� � ����� ����");

        _correctAnswer = _generator.Generate();

        yield return new WaitForSeconds(0.5f);

        _isRunning = true;

        Debug.Log("ISRUNNING: " + _isRunning);
    }

    public void ProcessEndGame()
        => _isRunning = false;

    public IEnumerator ProcessDefeat()
    {
        ProcessEndGame();
        Debug.Log("�����������, ��������");
        Debug.Log($"����� {RestartCode} ����� ������ ������");

        yield return new WaitWhile(() => Input.GetKeyDown(RestartCode) == false);

        Debug.Log("�������!");

        _coroutinesPerformer.StartPerform(Launch());
    }

    public IEnumerator ProcessWin()
    {
        ProcessEndGame();
        Debug.Log("�������, �� �������!");
        Debug.Log($"����� {GoToMenuCode} ����� ������� � ������� ����");

        yield return new WaitWhile(() => Input.GetKeyDown(GoToMenuCode) == false);

        Debug.Log("������� �� ����");

        _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
    }

    public bool WinConditionsCompleted(int counter)
        => counter > _correctAnswer.Length - 1;

    public bool DefeatConditionsCompleted(int counter)
     => Input.inputString != _correctAnswer[counter].ToString() && Input.inputString != "";
}
