using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Gameplay.Utils;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using System.Collections;
using UnityEngine;

public class GameplayCycle : IUpdatable
{
    private DIContainer _gameplayContainer;

    private SymbolsGenerator _generator;
    private SceneSwitcherService _sceneSwitcher;
    private ICoroutinesPerformer _coroutinesPerformer;

    private WalletService _walletService;
    private PlayerDataProvider _playerDataProvider;
    private StatisticsService _statisticsService;
    private StartGameModeRulesConfig _rulesConfig;
    private ConfigsProviderService _configsProviderService;
    private InputTextService _inputService;
    private TutorialService _tutorialService;

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
        _inputService = _gameplayContainer.Resolve<InputTextService>();
        _tutorialService = _gameplayContainer.Resolve<TutorialService>();

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

        if (_inputService.CurrentSymbol == _correctAnswer[_counter].ToString())
        {
            Debug.Log("Right");
            _counter++;
        }
    }

    public IEnumerator Launch()
    {
        _tutorialService.StartBeginnerTutorial();

        _counter = 0;

        _correctAnswer = _generator.Generate();
        //Debug.Log("Правильный ответ: " + _correctAnswer);

        yield return new WaitForSeconds(0.5f);

        _inputService.Enable();

        _isRunning = true;
    }

    public void ProcessEndGame()
    {
        _isRunning = false;
        _inputService.Disable();
    }

    public IEnumerator ProcessDefeat()
    {
        ProcessEndGame();

        _walletService.Waste(CurrencyTypes.Gold, _rulesConfig.CostOfLose);
        _statisticsService.ProcessDefeat();

        yield return _coroutinesPerformer.StartPerform(_tutorialService.StartDefeatTutorial());

        _coroutinesPerformer.StartPerform(Launch());
    }

    public IEnumerator ProcessWin()
    {
        ProcessEndGame();

        _walletService.Add(CurrencyTypes.Gold, _rulesConfig.PrizeForWin);
        _statisticsService.ProcessWin();

        yield return _coroutinesPerformer.StartPerform(_tutorialService.StartWinTutorial());

        _coroutinesPerformer.StartPerform(Launch());
    }

    public bool WinConditionsCompleted(int counter)
        => counter > _correctAnswer.Length - 1;

    public bool DefeatConditionsCompleted(int counter)
     => _inputService.CurrentSymbol != _correctAnswer[counter].ToString()
        && _inputService.InputedText.Length >= _correctAnswer.Length;



}
