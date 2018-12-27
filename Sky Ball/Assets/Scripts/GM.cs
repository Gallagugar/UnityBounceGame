using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GM : MonoBehaviour
{

    public int score;
    public int money;
    public int skinNum;
    public int level;
    float startTime;

    [SerializeField] Ball player;
    [SerializeField] PlatformSpawner platSpawner;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Canvas howToPlay;
    [SerializeField] Canvas deadScreen;
    [SerializeField] TextMeshProUGUI hstext;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Transform ballPreview;
    [SerializeField] Image locked;
    [SerializeField] Skin[] skins;
    [SerializeField] SkinButton skinButton;
    [SerializeField] GameObject unlockBox;
    [SerializeField] GameObject skinPreview;
    [SerializeField] Canvas adWait;
    [SerializeField] Canvas adWindow;
    [SerializeField] Canvas newLevelWindow;
    [SerializeField] TextMeshProUGUI adText;

    private int deaths;
    public SkinButton currentSkinButton;

    // Use this for initialization
    void Start()
    {

        // get info
        score = 0;
        money = PlayerPrefs.GetInt("Money");
        skinNum = 0;
        string temp = PlayerPrefs.GetString("Skin");
        for (int i = 0; i < skins.Length; i++)
        {
            if (temp == skins[i].name)
            {
                skinNum = i;
                break;
            }
        }


        if (moneyText != null)
            moneyText.SetText("$" + money);
        if (player != null)
            Instantiate(skins[skinNum], player.transform.position, Quaternion.identity, player.transform);
        else if (ballPreview != null)
            Instantiate(skins[skinNum], ballPreview.position, Quaternion.identity, ballPreview);


        PlayerPrefs.SetInt("DefaultSkin", 1);
        //PlayerPrefs.SetInt("Banana", 0);
        level = PlayerPrefs.GetInt("Level");

        deaths = PlayerPrefs.GetInt("Deaths");

        sound(PlayerPrefs.GetInt("Volume"));
    }

    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void died()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        hstext.SetText(PlayerPrefs.GetInt("HighScore") + "");
        deadScreen.gameObject.SetActive(true);

        if (Time.time - startTime >= 60)
        {
           // level++;
            PlayerPrefs.SetInt("Level", level);
            //newLevelWindow.gameObject.SetActive(true);
        }

        PlayerPrefs.SetInt("Money", money);
        deaths++;
        if (deaths == 5)
        {
            deaths = 0;
            ShowAd();
        }
        PlayerPrefs.SetInt("Deaths", deaths);
        print("oops");
    }

    public void ResetGame()
    {
        player.Start();
        platSpawner.Start();
        score = 0;
        scoreText.SetText(score + "");
        //levelText.text = "Level " + (level + 1);
        startTime = Time.time;
        deadScreen.gameObject.SetActive(false);
        howToPlay.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        howToPlay.gameObject.SetActive(false);
        deadScreen.gameObject.SetActive(false);
        //levelText.text = "Level " + (level + 1);
        startTime = Time.time;
        player.GetMovin();
    }

    public void UpdateScore()
    {
        //score += 1 + level;
        score++;
        scoreText.SetText(score + "");
    }

    public void MoneyUp()
    {
        money++;
        moneyText.SetText("$" + money);
    }

    public void AddMoney(int i)
    {
        money += i;
        moneyText.SetText("$" + money);
    }

    /*public void SetUpStore()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            SkinButton temp = Instantiate(skinButton);
            temp.skinName = skins[i].name;
            temp.num = i;
        }
    }*/

    public void ChangeSkin(int index)
    {
        print(index + "");
        Destroy(ballPreview.GetChild(0).gameObject);
        Instantiate(skins[index], ballPreview);
        if (PlayerPrefs.GetInt(skins[index].name) == 0)
            locked.gameObject.SetActive(true);
        else
            locked.gameObject.SetActive(false);

    }

    public void selectSkin(SkinButton button)
    {
        if (PlayerPrefs.GetInt(skins[button.GetNum()].name) == 0)
            return;
        skinNum = button.GetNum();
        currentSkinButton.Deselect();
        button.Select();
        currentSkinButton = button;

    }

    public void NextSkin()
    {
        if (skinNum + 1 == skins.Length)
            return;
        ChangeSkin(++skinNum);
    }

    public void PrevSkin()
    {
        if (skinNum == 0)
            return;
        ChangeSkin(--skinNum);
    }

    public void BuySkin()
    {
        if (money < 100)
        {
            //play sound
            print("not enough money");
            return;
        }

        int temp = 1;
        for (int i = 0; i < skins.Length; i++)
        {
            temp *= PlayerPrefs.GetInt(skins[i].name);
        }
        if (temp == 1)
        {
            print("all unlocked");
            return;
        }

        do
        {
            temp = UnityEngine.Random.Range(0, skins.Length);
        } while (PlayerPrefs.GetInt(skins[temp].name) == 1);

        PlayerPrefs.SetInt(skins[temp].name, 1);
        money -= 100;
        moneyText.SetText("$ " + money);

        skinNum = temp;

        unlockBox.SetActive(false);
        skinPreview.SetActive(true);
        Instantiate(skins[skinNum], skinPreview.transform.position, Quaternion.identity, skinPreview.transform);
    }

    public void CloseShop()
    {
        if (PlayerPrefs.GetInt(skins[skinNum].name) == 1)
            PlayerPrefs.SetString("Skin", skins[skinNum].name);
        PlayerPrefs.SetInt("Money", money);
        SwitchScene("Game");
    }

    public void ShowAd()
    {
        adWait.gameObject.SetActive(true);
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("video", options);
    }

    public void HandleShowResult(ShowResult result)
    {
        adWait.gameObject.SetActive(false);
    }

    public void ShowRewardedVideo()
    {
        adWait.gameObject.SetActive(true);
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResultRewarded;

        Advertisement.Show("rewardedVideo", options);
    }

    public void HandleShowResultRewarded(ShowResult result)
    {
        print("work");
        int reward = 0;
        if (result == ShowResult.Finished)
        {
            reward = UnityEngine.Random.Range(20, 30);
            Debug.Log("Video completed - Offer a reward to the player");
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + reward);
            adText.text = "You have recieved $" + reward;
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");
            adText.text = "Video skipped, you have recieved $0";
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
            adText.text = "There was an error loading the video, please try again";
        }
        else
            adText.text = "";

        adWait.gameObject.SetActive(false);
        adWindow.gameObject.SetActive(true);
        money += reward;
        PlayerPrefs.SetInt("Money", money);
        moneyText.SetText("$" + money);
    }

    public void ResetEverything()
    {
        money = 0;
        level = 0;
        PlayerPrefs.DeleteAll();
    }

    public void sound(int vol)
    {
        AudioListener.volume = vol;
        PlayerPrefs.SetInt("Volume", vol);
    }
}
