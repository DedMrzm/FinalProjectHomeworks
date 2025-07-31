using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.DataManagment.DataProviders;
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

    private DIContainer _gameplayContainer;

    private SymbolsGenerator _generator;
    private SceneSwitcherService _sceneSwitcher;
    private ICoroutinesPerformer _coroutinesPerformer;

    private WalletService _walletService;
    private PlayerDataProvider _playerDataProvider;
    private StatisticsService _statisticsService;
    private StartGameModeRulesConfig _rulesConfig;
    private ConfigsProviderService _configsProviderService;

    private string _correctAnswer;
    private int _counter = 0;

    private bool _isRunning = false;

    public GameplayCycle(DIContainer projectContainer)
    {
        _gameplayContainer = projectContainer;

        _playerDataProvider = _gameplayContainer.Resolve<PlayerDataProvider>();
        _walletService = _gameplayContainer.Resolve<WalletService>();
        _generator = _gameplayContainer.Resolve<SymbolsGenerator>();
        _sceneSwitcher = _gameplayContainer.Resolve<SceneSwitcherService>();
        _coroutinesPerformer = _gameplayContainer.Resolve<ICoroutinesPerformer>();
        _configsProviderService = _gameplayContainer.Resolve<ConfigsProviderService>();
        _statisticsService = _gameplayContainer.Resolve<StatisticsService>();

        _rulesConfig = _configsProviderService.GetConfig<StartGameModeRulesConfig>();
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
        Debug.Log($"Чтобы начать, введите {NextCode}");

        yield return new WaitWhile(() => Input.GetKeyDown(NextCode) == false);

        Debug.Log("Введите набор символов, как в дебаг логе");

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
        Debug.Log("Неправильно, проиграл");

        _walletService.Waste(CurrencyTypes.Gold, _rulesConfig.CostOfLose);
        _statisticsService.ProcessDefeat();

        _coroutinesPerformer.StartPerform(_playerDataProvider.Save());

        Debug.Log($"Сейчас у тебя {_walletService.GetCurrency(CurrencyTypes.Gold).Value} золота");

        Debug.Log($"Нажми {RestartCode} чтобы начать заново");

        yield return new WaitWhile(() => Input.GetKeyDown(RestartCode) == false);

        Debug.Log("Рестарт!");

        _coroutinesPerformer.StartPerform(Launch());
    }

    public IEnumerator ProcessWin()
    {
        ProcessEndGame();
        Debug.Log("Отлично, ты выиграл!");

        _walletService.Add(CurrencyTypes.Gold, _rulesConfig.PrizeForWin);
        _statisticsService.ProcessWin();

        _coroutinesPerformer.StartPerform(_playerDataProvider.Save());

        Debug.Log($"Сейчас у тебя {_walletService.GetCurrency(CurrencyTypes.Gold).Value} золота");

        Debug.Log($"Нажми {GoToMenuCode} чтобы перейти в главное меню");

        yield return new WaitWhile(() => Input.GetKeyDown(GoToMenuCode) == false);

        Debug.Log("Переход на меню");

        _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
    }

    public bool WinConditionsCompleted(int counter)
        => counter > _correctAnswer.Length - 1;

    public bool DefeatConditionsCompleted(int counter)
     => Input.inputString != _correctAnswer[counter].ToString() && Input.inputString != "";


}
