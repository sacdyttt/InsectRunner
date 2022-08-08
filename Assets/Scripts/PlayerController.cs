using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public int CurrentInddexOfChara;
    public List<Transform> CharaList = new List<Transform>();
    public GameObject Chara;
    public GameObject DeathPRT;
    public TextMeshProUGUI KillCountTxt;
    public int KillCounter;
    public GameObject GemSPark;
    public TextMeshProUGUI PlayerCounter;
    public AudioSource MyAudioSource;
    public AudioClip Splat, Collection;
    public GameObject Spider;
    private bool IsCharaDeactivated;
    public GameObject Bloodsplat, Skeleton;
    public Transform Endpoint;
    public GameObject Cam2;
    private bool CanStillRun;
    public GameObject BrokenGlass;
    public  List<GameObject> MiniEnemiesList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform Chara in CharaList)
        {
            if (Chara.transform.GetComponent<Animator>())
            {
                Chara.transform.GetComponent<Animator>().SetBool("Run", true);
            }
        }
        Endpoint = GameObject.FindGameObjectWithTag("End").transform;
        GameObject[] MiniEnemies = GameObject.FindGameObjectsWithTag("MiniEnemy");
        foreach(GameObject T in MiniEnemies)
        {
            MiniEnemiesList.Add(T);
        }
        StartCoroutine(MiniEnemyController());
    }





    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);


    }

    IEnumerator MiniEnemyController()
    {
        while(true)
        {
            foreach(GameObject T in MiniEnemiesList)
            {
                Vector3 Distance = transform.position - T.transform.position;
                //print(Distance + T.name);
                if(Distance.z < 5)
                {
                    T.transform.rotation = Quaternion.Euler(0, 180, 0);
                    T.gameObject.GetComponent<Animator>().SetBool("Idle", false);
                    T.gameObject.GetComponent<Animator>().SetBool("Run", true);
                    T.gameObject.GetComponent<ModelMG>().enabled = true;
                    T.gameObject.transform.GetChild(T.gameObject.transform.childCount - 1).gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if(Distance.z > 15)
                {
                    T.transform.rotation = Quaternion.Euler(0, 0, 0);
                    T.gameObject.GetComponent<Animator>().SetBool("Idle", true);
                    StartCoroutine(LateCaller(T));
                    T.gameObject.GetComponent<ModelMG>().enabled = false;
                    T.gameObject.transform.GetChild(T.gameObject.transform.childCount - 1).gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            yield return new WaitForSeconds(0.1f * Time.deltaTime);
        }
    }

    IEnumerator LateCaller(GameObject T)
    {
        yield return new WaitForSeconds(1f * Time.deltaTime);
        T.gameObject.GetComponent<Animator>().SetBool("Run", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            other.gameObject.tag = "Untagged";
            if (CurrentInddexOfChara <= 240)
            {
               
                other.gameObject.transform.DOMoveY(-1, 0.5f).SetEase(Ease.Flash).OnComplete(() => { other.gameObject.SetActive(false); other.gameObject.transform.parent.transform.gameObject.SetActive(false); });
                other.gameObject.tag = "Untagged";
                int value;
                int.TryParse(other.gameObject.name, out value);
                for (int a = 0; a <= CurrentInddexOfChara; a++)
                {
                    if (!CharaList[a].gameObject.activeSelf)
                    {
                        CharaList[a].gameObject.SetActive(true);
                        CharaList[a].gameObject.GetComponent<Animator>().SetBool("Run", true);
                        value--;
                    }
                }
                StartCoroutine(ActivateCharas(CurrentInddexOfChara, CurrentInddexOfChara += value));
            }
            MyAudioSource.PlayOneShot(Collection);
        }

        else if (other.gameObject.CompareTag("MGate"))
        {
            other.gameObject.tag = "Untagged";
            if (CurrentInddexOfChara <= 80)
            {
                other.gameObject.transform.DOMoveY(-3, 1f).SetEase(Ease.Flash).OnComplete(() => { other.gameObject.SetActive(false); other.gameObject.transform.parent.transform.gameObject.SetActive(false); });
                other.gameObject.tag = "Untagged";
                int value;
                int.TryParse(other.gameObject.name, out value);
                int TwmpMultiplierOFCurrentIndexer = value;
                value *= CurrentInddexOfChara;
                for (int a = 0; a <= CurrentInddexOfChara; a++)
                {
                    if (!CharaList[a].gameObject.activeSelf)
                    {
                        CharaList[a].gameObject.SetActive(true);
                        CharaList[a].gameObject.GetComponent<Animator>().SetBool("Run", true);
                        value--;
                    }
                }
                StartCoroutine(ActivateCharas(CurrentInddexOfChara, value));
                CurrentInddexOfChara *= TwmpMultiplierOFCurrentIndexer;
            }
            MyAudioSource.PlayOneShot(Collection);
        }
        

        else if (other.gameObject.CompareTag("MiniEnemy"))
        {
            if (other.gameObject.GetComponent<ModelMG>().MyValue > CurrentInddexOfChara)
            {
                other.gameObject.transform.GetChild(other.gameObject.transform.childCount - 1).gameObject.SetActive(false);
                print("Try a diff effect! Like make human play kill animation and kill insects one by one and then Game Fails");
                MiniEnemiesList.Remove(other.gameObject);
                other.gameObject.GetComponent<ModelMG>().speed = 0;
                other.gameObject.GetComponent<Animator>().SetBool("Tread", true);
                other.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                int tempindexofchara = CurrentInddexOfChara;
                CurrentInddexOfChara = 0;
                Speed = 0;
                List<GameObject> Spiders = new List<GameObject>();
                List<GameObject> Targets = new List<GameObject>();
                List<Vector3> Positions = new List<Vector3>();
                foreach (Transform T in CharaList)
                {
                    Positions.Add(T.transform.position);
                }
                
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                PlayerCounter.gameObject.transform.parent.gameObject.SetActive(false);

                int index = 0;
                foreach (Transform T in other.transform)
                {
                    if (T.CompareTag("FailPoint"))
                    {
                        Targets.Add(T.gameObject);
                    }
                }
                for (int a = 0; a <= tempindexofchara; a++)
                {
                    Vector3 Pos = CharaList[a].position;
                    GameObject SpiderTemp = Instantiate(Spider, Pos, Quaternion.identity);
                    SpiderTemp.GetComponent<Animator>().SetBool("Run", true);
                    Spiders.Add(SpiderTemp);
                    Destroy(SpiderTemp, 5);
                }
                foreach (GameObject T in Spiders)
                {
                    if (index == Targets.Count)
                    {
                        index = 0;
                    }
                    T.gameObject.transform.LookAt(Targets[index].transform);
                    T.gameObject.transform.DOMove(Targets[index].transform.position, Random.Range(1, 1)).OnComplete(() => { T.gameObject.AddComponent<Rigidbody>();});
                    index++;
                }
                for (int a = 1; a <= CharaList.Count - 1; a++)
                {
                    CharaList[a].gameObject.SetActive(false);
  
                }
                StartCoroutine(EnumareteDeathPRT(Spiders));
                PlayerCounter.text = "0";

            }
            else
            {
                other.gameObject.transform.GetChild(other.gameObject.transform.childCount - 1).gameObject.SetActive(false);
                MiniEnemiesList.Remove(other.gameObject);
                other.gameObject.GetComponent<ModelMG>().speed = 0;
                other.gameObject.GetComponent<Animator>().SetBool("Scare", true);
                other.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                int tempindexofchara = CurrentInddexOfChara;
                CurrentInddexOfChara = 0;
                Speed = 0;
                List<GameObject> Spiders = new List<GameObject>();
                List<GameObject> Targets = new List<GameObject>();
                List<Vector3> Positions = new List<Vector3>();
                foreach(Transform T in CharaList)
                {
                    Positions.Add(T.transform.position);
                }
                StartCoroutine(DeactivateChara(1, CharaList.Count - 1, false, other.gameObject));
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                PlayerCounter.gameObject.transform.parent.gameObject.SetActive(false);

                int index = 0;
                foreach (Transform T in other.transform)
                {
                    if (T.CompareTag("Point"))
                    {
                        Targets.Add(T.gameObject);
                    }
                }
                for (int a = 0; a <= tempindexofchara; a++)
                {
                    Vector3 Pos = CharaList[a].position;
                    GameObject SpiderTemp = Instantiate(Spider, Pos, Quaternion.identity);
                    SpiderTemp.GetComponent<Animator>().SetBool("Run", true);
                    Spiders.Add(SpiderTemp);
                    //Destroy(SpiderTemp, 5);
                }
                foreach (GameObject T in Spiders)
                {
                    if (index == Targets.Count)
                    {
                        index = 0;
                    }
                    T.gameObject.transform.LookAt(Targets[index].transform);
                    T.gameObject.transform.DOMove(Targets[index].transform.position, Random.Range(2, 2)).OnComplete(() => { T.gameObject.AddComponent<Rigidbody>(); });
                    index++;
                }
            }
        }

        else if(other.gameObject.CompareTag("Glass"))
        {
            other.gameObject.SetActive(false);
            GameObject BrokenGlas = Instantiate(BrokenGlass, other.transform.position, Quaternion.identity);
            foreach(Transform T in BrokenGlas.transform)
            {
                T.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
            }
            Destroy(BrokenGlas, 5);
        }

        else if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            //GameObject BrokenGlas = Instantiate(BrokenGlass, other.transform.position, Quaternion.identity);
            //Destroy(BrokenGlas, 5);
        }

        else if ((other.gameObject.CompareTag("FinalEnemy")))
        {
            CurrentInddexOfChara = 0;
            Speed = 0;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            PlayerCounter.gameObject.transform.parent.gameObject.SetActive(false);
            StartCoroutine(ScalePlayer());

        }

    }

    IEnumerator ScalePlayer()
    {
        bool IsScaling = true;
        Vector3 Scale = new Vector3(0, 0, 0);
        int a = 1;
        while (IsScaling)
        {

            Transform T = CharaList[a];
            T.DOMove(transform.position, 1).OnComplete(() => { T.gameObject.SetActive(false); });
            CharaList[0].gameObject.transform.DOScale(Scale, 0.5f);
            Scale += new Vector3(0.002f, 0.002f, 0.002f);
            print(Scale);
            if (a == CharaList.Count - 1)
            {
                print("Scaled!!");
                StartCoroutine(RunAtEnd( Scale.x * 10));
                IsScaling = false;
            }
            a++;
            yield return new WaitForSeconds(1f * Time.deltaTime);
        }
    }


    IEnumerator ActivateCharas(int start, int finish)
    {
        while (start <= finish)
        {
            PlayerCounter.text = start.ToString();
            CharaList[start].gameObject.SetActive(true);
            if (CharaList[start].GetComponent<BoxCollider>())
            { CharaList[start].GetComponent<BoxCollider>().enabled = true; }
            if (CharaList[start].transform.GetComponent<Animator>())
            { CharaList[start].transform.GetComponent<Animator>().SetBool("Run", true); }
            start++;
            yield return new WaitForSeconds(0.1f * Time.deltaTime);
        }

    }

    public void KillChara(Vector3 Pos)
    {
        CurrentInddexOfChara--;
        PlayerCounter.text = CurrentInddexOfChara.ToString();
        GameObject Death = Instantiate(DeathPRT, Pos, Quaternion.identity);
        Destroy(Death, 3);
    }

    /*IEnumerator KillChara(int StartIndex, int StopIndex)
    {
        while (StartIndex >= StopIndex)
        {
            PlayerCounter.text = StartIndex.ToString();
            StartIndex--;
            yield return new WaitForSeconds(0.8f * Time.deltaTime);
        }
    }*/


    IEnumerator DeactivateChara(int start, int finish, bool IsFinalEnemy, GameObject MiniEnemy)
    {

    
            StartCoroutine(RunBack(MiniEnemy, false));
            for (int a = 1; a <= CharaList.Count - 1; a++)
            {
                CharaList[a].gameObject.SetActive(false);
            }
            PlayerCounter.text = "0";
            IsCharaDeactivated = true;

       

        yield return new WaitForSeconds(0.8f * Time.deltaTime);

    }


    IEnumerator RunBack(GameObject MiniEnemy, bool IsFinalEnemy)
    {
        yield return new WaitForSeconds(2f);
        GameObject SLeton = Instantiate(Skeleton, MiniEnemy.transform.position, Quaternion.identity);
        GameObject BoolSpalt = Instantiate(Bloodsplat, new Vector3(MiniEnemy.transform.position.x, 2, MiniEnemy.transform.position.z), Quaternion.identity);
        MiniEnemy.gameObject.SetActive(false);
   

        yield return new WaitForSeconds(0.5f);
        if (!IsFinalEnemy)
        {
            KillCounter++;
            KillCountTxt.text = KillCounter.ToString();
            Speed = 5.5f;
            transform.GetChild(0).GetComponent<Animator>().SetBool("Attack", false);
            PlayerCounter.gameObject.transform.parent.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);

    }



    IEnumerator RunAtEnd( float Timer)
    {
        Cam2.gameObject.SetActive(true);
        CanStillRun = true;
        yield return new WaitForSeconds(3);
        // transform.DOMove(EndPoint.position, Timer);
        Vector3 pos = transform.position;
        StartCoroutine(RunOnEndPlatform());
        CharaList[0].gameObject.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), Timer).OnComplete(() => {  CanStillRun = false; float Distance = Vector3.Distance(pos, transform.position); OnGameFinish(Distance); });
    }

    IEnumerator RunOnEndPlatform()
    {


        while(CanStillRun)
        {
            transform.Translate(Vector3.forward * 0.2f);
            yield return new WaitForSeconds(5f * Time.deltaTime);
        }
    }

    public void OnGameFinish(float Distance)
    {

        GameMG.FindObjectOfType<GameMG>().OnGameFinish(false,(int) Distance,KillCounter);
    }


    IEnumerator EnumareteDeathPRT(List<GameObject> Spiders)
    {
        yield return new WaitForSeconds(2);
        int a = Spiders.Count;
        print(a);
        while(a > 1)
        {
            a--;
            GameObject Death = Instantiate(DeathPRT, Spiders[a].transform.position, Quaternion.identity);
            Destroy(Spiders[a]);
            Destroy(Death, 3);            
            yield return new WaitForSeconds(5 * Time.deltaTime);
        }
        yield return new WaitForSeconds(1);
        GameObject Deathprt = Instantiate(DeathPRT, CharaList[0].transform.position, Quaternion.identity);
        Destroy(CharaList[0].gameObject);
        GameMG.FindObjectOfType<GameMG>().OnGameFinish(true, 0,KillCounter);
    }

    public void OnCheckIfPlayerDead()
    {
        if (CurrentInddexOfChara <= 0)
        {
            Speed = 0;
            GameMG.FindObjectOfType<GameMG>().OnGameFinish(true, 0,KillCounter);
        }
    }

    public void OnDiamondHit(Vector3 Pos)
    {
       // DiamondCount.text = DiamondCounter.ToString();
        GameObject T = Instantiate(GemSPark, Pos, Quaternion.identity);
        Destroy(T, 2);
    }

    private bool IsSplatPlayed;
    public void PlaySplatSound()
    {
        if (!IsSplatPlayed)
        { MyAudioSource.PlayOneShot(Splat); IsSplatPlayed = true; Invoke("SetSplatFalse", 3); }
    }

    public void SetSplatFalse()
    {
        IsSplatPlayed = false;
    }
}
