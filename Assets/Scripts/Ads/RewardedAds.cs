using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAds : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] 
    private bool _testMode = true;
    [SerializeField] 
    private Button _adsButton;

    private string _gameId = "4549947";
    private string _rewardedVideo = "Rewarded_Android";

    private bool _isShowed;

    void Start()
    {
        _adsButton = GetComponent<Button>();
        _adsButton.interactable = Advertisement.IsReady(_rewardedVideo);

        if (_adsButton)
        {
            _adsButton.onClick.AddListener(ShowRewardedVideo);
        }

        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId,true);

    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(_rewardedVideo);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (_isShowed)
        {
            _adsButton.interactable = false;
            return;
        }
        if (placementId == _rewardedVideo)
        {
            _adsButton.interactable = true; //Действие если реклама доступна
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Ошибка рекламы");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Только запустили рекламу

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) //Обработка рекламы(тут определяем вознаграждение)
    {
        if (showResult == ShowResult.Finished)  //Действие, если пользователь посмотрел рекламу до конца
        {
            if (!_isShowed)
            {
                Debug.Log("Действие, если пользователь посмотрел рекламу до конца");
                _isShowed = true;
                GameEvent.WatchedAds?.Invoke();
            }            
        }
        else if (showResult == ShowResult.Skipped)
        {
            //Действие, если пользователь пропустил рекламу
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log("Действие при ошибке");
        }
    }
}



