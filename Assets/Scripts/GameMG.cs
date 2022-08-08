using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Cinemachine;
//using GameAnalyticsSDK;

public class GameMG : MonoBehaviour
{
    public GameObject StartScene, IngameScene, FailScene, WinScene, PostWinScene, FinalWinScene;
    // Start is called before the first frame update

    public GameObject MainPlayer, Platfrom;

    public int LevelNumber;

    public GameObject[] Levels;

    public TextMeshProUGUI StartMenuMoneyCountTxt,EndMenuMoneyCountTxt,StartmenuGemCountTxt,EndMenuGemCountTxt,LevelTxt,KillCount;

    private int TotalMoney,TotalGem;

    public Color[] ChibiColorList;

    public Material PlayerMaterial,DeathMT1,DeatMT2,Buff;

    public GameObject SkinShopUI,MainMenuUI;

    public GameObject[] ItemButtons;

    public Sprite[] ItemIcons;

    public TextMeshProUGUI CoinShopTxt;

    public GameObject DefaultPlayer;

    public int PurchaseCounter;

    public GameObject DeathPRT;
    public GameObject Enemy;
    public GameObject Cam1, Cam2;
    public GameObject[] Chest;
    public GameObject GOldFountain;
    public GameObject HitAnim;
    public TextMeshProUGUI DiamondCount, StartLevelTxt;

    private int ColorIndexTemp;

    public Material[] SkyboxMaterial;

    public GameObject[] Environments;

    public Material Base;

    public Color[] BaseColor;

    public GameObject EndLevel;

    public GameObject Joystick;

    public GameObject HandHelper;

    public GameObject StatusSlider;

    private int minienemycounter;

    public GameObject[] MiniEnemy;

    public GameObject[] Slidinggates;

    private Vector3 pos1 = new Vector3(-1, 0, 0);
    private Vector3 pos2 = new Vector3(1, 0, 0);
    public float speed = 1.0f;

    private void Start()
    {
        //Base.color = BaseColor[Random.Range(0, BaseColor.Length - 1)];
       // RenderSettings.skybox = SkyboxMaterial[Random.Range(0, SkyboxMaterial.Length)];
        //int temp = Random.Range(0, 2);
        //if(temp > 0)
        //{
          //  Environments[Random.Range(0, Environments.Length)].gameObject.SetActive(true);
        //}


        int Number = PlayerPrefs.GetInt("TotalMoney");
        StartMenuMoneyCountTxt.text = (Number).ToString();

        int NumberGem = PlayerPrefs.GetInt("TotalGem");
        StartmenuGemCountTxt.text = (NumberGem).ToString();   

        LevelNumber = PlayerPrefs.GetInt("LevelNumber");
       // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, LevelNumber.ToString());
        TotalMoney = PlayerPrefs.GetInt("TotalMoney");
        StartLevelTxt.text = "level " + (LevelNumber + 1).ToString();
        if (LevelNumber < Levels.Length - 1)
        {
            GameObject Level = Instantiate(Levels[LevelNumber]);
        }
        else
        {
            GameObject Level = Instantiate(Levels[Random.Range(0, Levels.Length - 1)]);
        }



    }

    /*private void Update()
    {
        foreach(GameObject T in Slidinggates)
        {
            Vector3 pos1 = new Vector3(-1, T.transform.position.y, T.transform.position.z);
            Vector3 pos2 = new Vector3(1, T.transform.position.y, T.transform.position.z);
            T.transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
        }

    }*/

    public void OnStartGame()
    {
        MainPlayer = GameObject.FindGameObjectWithTag("MainPlayer");
        Cam1.GetComponent<CinemachineVirtualCamera>().m_Follow = MainPlayer.transform;
        Cam2.GetComponent<CinemachineVirtualCamera>().m_Follow = MainPlayer.transform;
        MainPlayer.GetComponent<PlayerController>().enabled = true;
        StartScene.gameObject.SetActive(false);
        IngameScene.gameObject.SetActive(true);
        //MiniEnemy = GameObject.FindGameObjectsWithTag("MiniEnemy");
        //MiniEnemy[minienemycounter].gameObject.GetComponent<Animator>().SetBool("Run", true);
        //MiniEnemy[minienemycounter].gameObject.GetComponent<ModelMG>().enabled = true;
        minienemycounter++;
    }

    public void OnMakeRunEnemy()
    {
        MiniEnemy[minienemycounter].gameObject.GetComponent<ModelMG>().enabled = true;
        MiniEnemy[minienemycounter].gameObject.GetComponent<Animator>().SetBool("Run", true);
        minienemycounter++;
    }
    public void OnContinueGame()
    {
        WinScene.gameObject.SetActive(false);
        Joystick.gameObject.SetActive(true);
        StatusSlider.gameObject.SetActive(true);
        MainPlayer.GetComponent<SwerveInputSystem>().enabled = false;
        MainPlayer.GetComponent<SwerveMovement>().enabled = false;
        MainPlayer.GetComponent<PlayerIdleController>().enabled = true;
        MainPlayer.GetComponent<PlayerController>().enabled = false;
        Cam2.SetActive(true);
        Cam1.SetActive(false);
        EndLevel.gameObject.SetActive(true);
        Platfrom.gameObject.SetActive(false);
        HandHelper.gameObject.GetComponent<Animator>().SetBool("Joystick", true);
        print(HandHelper);
        PostWinScene.gameObject.SetActive(true);
    }

    public void OnPlayIdelGame()
    {
        PostWinScene.gameObject.SetActive(false);
    }

    public void OnGameFinishFinal()
    {
        FinalWinScene.gameObject.SetActive(true);
        MainPlayer.gameObject.GetComponent<PlayerIdleController>().enabled = false;
    }

    public void OnGameFinish(bool hasLost, int GemCount, int killcount)
    {
        if(hasLost)
        {
            FailScene.gameObject.SetActive(true);
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, LevelNumber.ToString());
        }
        else
        {
            LevelNumber = PlayerPrefs.GetInt("LevelNumber");
            LevelNumber = LevelNumber + 1;
            PlayerPrefs.SetInt("LevelNumber", LevelNumber);
            WinScene.gameObject.SetActive(true);
            TotalGem = PlayerPrefs.GetInt("TotalGem");
            TotalGem += (GemCount);
            EndMenuGemCountTxt.text = (GemCount).ToString();
            PlayerPrefs.SetInt("TotalGem", TotalGem);
            KillCount.text = killcount.ToString();
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, LevelNumber.ToString());
        }
    }

    public void OnReloadGameScene()
    {
        SceneManager.LoadSceneAsync("Preloader");
    }

    public void OnPressColor()
    {
        PlayerMaterial.color = ChibiColorList[ColorIndexTemp];
        DeathMT1.color = ChibiColorList[ColorIndexTemp];
        DeatMT2.color = ChibiColorList[ColorIndexTemp];
        if(ColorIndexTemp >= ChibiColorList.Length-1)
        {
            ColorIndexTemp = 0;
        }
        else
        {
            ColorIndexTemp++;
        }
        print(ColorIndexTemp);
    }

    #region SkinShopCodes

    public void OnOpenShop()
    {
        SkinShopUI.gameObject.SetActive(true);
        MainMenuUI.gameObject.SetActive(false);
        ItemDisplayLogic();
        int PurchaseCount = PlayerPrefs.GetInt("PurchaseCounter");
        for(int a = 0; a<= PurchaseCount;a++)
        {
            ItemButtons[a].gameObject.tag = "Purchased";
            ItemButtons[a].gameObject.GetComponent<Button>().enabled = true;
            ItemButtons[a].gameObject.GetComponent<Button>().image.sprite = ItemIcons[a];
        }
    }
    public void OnCloseShop()
    {
        SkinShopUI.gameObject.SetActive(false);
        MainMenuUI.gameObject.SetActive(true);
    }

    public void ItemDisplayLogic()
    {

        TotalMoney = PlayerPrefs.GetInt("TotalMoney");
        CoinShopTxt.text = TotalMoney.ToString();
        if(TotalMoney > 1000)
        {
            ItemButtonActivation(5);
        }
        else if(TotalMoney > 500)
        {
            ItemButtonActivation(5);
        }
        else if (TotalMoney > 200)
        {
            ItemButtonActivation(2);
        }

    }

    public void ItemButtonActivation(int lastindex)
    {
        for (int a = 0; a <= lastindex; a++)
        {
            ItemButtons[a].gameObject.GetComponent<Button>().enabled = true;
            ItemButtons[a].gameObject.GetComponent<Button>().image.sprite = ItemIcons[a];
        }
    }

    public void OnPurchaseItem(Button BTName)
    {
        if (!BTName.CompareTag("Purchased"))
        { 
            int pricetoreduce = BTName.GetComponent<ItemObject>().ItemPrice;
            BTName.tag = "Purchased";
            PurchaseCounter++;
            PlayerPrefs.SetInt("PurchaseCounter", PurchaseCounter);
            TotalMoney = PlayerPrefs.GetInt("TotalMoney");
            TotalMoney -= pricetoreduce;
            PlayerPrefs.SetInt("TotalMoney", TotalMoney);
            CoinShopTxt.text = TotalMoney.ToString();
        }
        // Instantiate respective player modle under player perent.
        GameObject NewPlayer = Instantiate(BTName.GetComponent<ItemObject>().Playermodel,new Vector3(0,0, -2.35f),Quaternion.Euler(0,180,0));
        DeathMT1.color = BTName.GetComponent<ItemObject>().PlayerColor;
        DeatMT2.color = BTName.GetComponent<ItemObject>().PlayerColor;
        Buff.color = BTName.GetComponent<ItemObject>().PlayerColor;
        Destroy(DefaultPlayer);
    }
    #endregion
}
