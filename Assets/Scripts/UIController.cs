using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;
public class UIController : MonoBehaviour
{
    [SerializeField] private Slider enemyCounterSlider;
    [SerializeField] private Transform winPanel;
    [SerializeField] private Transform startPanel;
    [SerializeField] private Transform moneyIcon;
    [SerializeField] private Transform dragToPlay;
    [SerializeField] private Transform pointForDragToPlayRight;
    [SerializeField] private Transform pointForDragToPlayLeft;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button getMoneyForAdsButton;
    [SerializeField] private Button skillButton;    
    [SerializeField] private List<Transform> flyMoney;
    [SerializeField] private List<Transform> flyMoneyInTheAds;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text earnedMoneyText;
    [SerializeField] private TMP_Text percentOfLevelText;
    [SerializeField] private TMP_Text currentLevelText;
    [SerializeField] private TMP_Text nextLevelText;
    private GameController _gameController;
    private int _amountEarnedMoney = 0;
    private int _moneyForAds = 200;
    private float _duringFlyMoney = 0.5f;
    private float _duringAccrualsOfMoney = 2f;
    private float _stepOfSliderScale = 0;
    private void Awake()
    {
        _gameController = GameController.Instance;
        GameEvent.LevelCompleted += LevelCompleted;
        GameEvent.FillSliderScale += UpdateEnemyCounterSlider;
        GameEvent.WatchedAds += NextLevelAfterWatchingAds;
        nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void Start()
    {
        UpdateCoinText();
        ComputeStepOfSliderScale();
        MoveDragToStartImage();
    }
    private void OnDestroy()
    {
        GameEvent.LevelCompleted -= LevelCompleted;
        GameEvent.FillSliderScale -= UpdateEnemyCounterSlider;
        GameEvent.WatchedAds -= NextLevelAfterWatchingAds;
    }
    
    private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
    
                if (touch.phase == TouchPhase.Moved)
                {
                    StartLevel();
                }
            }
        }
    

    private void StartLevel()
    {
        startPanel.gameObject.SetActive(false);        
        enemyCounterSlider.gameObject.SetActive(true);
        currentLevelText.text = (_gameController.CurrentLevelNumber + 1).ToString();
        nextLevelText.text = (_gameController.CurrentLevelNumber + 2).ToString();

    }
    private void LevelCompleted()
    {
        enemyCounterSlider.gameObject.SetActive(false);
        _amountEarnedMoney = UnityEngine.Random.Range(50, 150);
        earnedMoneyText.text = _amountEarnedMoney.ToString();
        _gameController.AddCoins(_amountEarnedMoney);        
        skillButton.gameObject.SetActive(false);
        Invoke("ActiveWinPanel", 3);
    }

    private void ComputeStepOfSliderScale()
    {
        _stepOfSliderScale = 1f / _gameController.AmountOfEnemies;
    }
    
    private void UpdateCoinText()
    {
        moneyText.text = _gameController.Coin.ToString();
    }
    private void ActiveWinPanel()
    {
        winPanel.gameObject.SetActive(true);
        Invoke("ActiveNextLevelButton", 3);
    }

    private void ActiveNextLevelButton()
    {
        nextLevelButton.gameObject.SetActive(true);
    }
    
    private void NextLevel()
    {
        nextLevelButton.interactable = false;
        getMoneyForAdsButton.interactable = false;
        var seq = LeanTween.sequence();
        foreach (Transform item in flyMoney)
        {
            item.gameObject.SetActive(true);
            seq.append(LeanTween.move(item.gameObject, moneyIcon.position, _duringFlyMoney));           
        }
              
        LeanTween.value(gameObject, CallBack, _gameController.Coin - _amountEarnedMoney, _gameController.Coin, _duringAccrualsOfMoney)
            .setOnComplete(() => SceneManager.LoadScene(0));
    }

    private void NextLevelAfterWatchingAds()
    {
        _gameController.AddCoins(_moneyForAds);
        nextLevelButton.interactable = false;
            var seq = LeanTween.sequence();
        foreach (Transform item in flyMoneyInTheAds)
        {
            item.gameObject.SetActive(true);
            seq.append(LeanTween.move(item.gameObject, moneyIcon.position, _duringFlyMoney));           
        }
              
        LeanTween.value(gameObject, CallBack, _gameController.Coin - _amountEarnedMoney - _moneyForAds, _gameController.Coin, _duringAccrualsOfMoney)
            .setOnComplete(() => SceneManager.LoadScene(0));
    }

    private void CallBack(float value)
    {
        moneyText.text = Convert.ToInt32(value).ToString();
    }

    private void MoveDragToStartImage()
    {
        var seq = LeanTween.sequence();
        seq.append(LeanTween.move(dragToPlay.gameObject, pointForDragToPlayRight, 1f).setLoopPingPong());
        seq.append(LeanTween.move(dragToPlay.gameObject, pointForDragToPlayLeft, 1f).setLoopPingPong());
        
    }
    
    private void UpdateEnemyCounterSlider()
    {
        enemyCounterSlider.value += _stepOfSliderScale;
        percentOfLevelText.text = Convert.ToInt32(enemyCounterSlider.value * 100).ToString() + " %";
    }
}
