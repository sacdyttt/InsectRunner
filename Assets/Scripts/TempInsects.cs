using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TempInsects : MonoBehaviour
{
    public GameObject PInsect1,PInsect2,PInsect3;
    public  List<GameObject> Targets = new List<GameObject>();
    public GameObject Target;
    public GameObject[] Inswects;
    public GameObject Skeleton;
    public GameObject Bloodsplat;

    // Start is called before the first frame update
    void Start()
    {
       // Insect1();
       // Insect2();
       // Insect3();

        foreach(GameObject T in Inswects)
        {
            StartCoroutine(InsectMovement(T.transform));
        }
    }

    public void Insect1()
    {
        List<GameObject> Spiders = new List<GameObject>();

        for (int a = 0; a <= 30; a++)
        {
            Vector3 Pos = new Vector3(Random.Range(Target.transform.position.x - 2, Target.transform.position.x + 2), Target.transform.position.y, Random.Range(Target.transform.position.z - 2, Target.transform.position.z + 2));
            GameObject SpiderTemp = Instantiate(PInsect1, Pos, Quaternion.identity);
            Spiders.Add(SpiderTemp);
            StartCoroutine(InsectMovement(SpiderTemp.transform));
        }

    }

    public void Insect2()
    {
        List<GameObject> Spiders = new List<GameObject>();

        for (int a = 0; a <= 30; a++)
        {
            Vector3 Pos = new Vector3(Random.Range(Target.transform.position.x - 2, Target.transform.position.x + 2), Target.transform.position.y, Random.Range(Target.transform.position.z - 2, Target.transform.position.z + 2));
            GameObject SpiderTemp = Instantiate(PInsect2, Pos, Quaternion.identity);
            Spiders.Add(SpiderTemp);
            StartCoroutine(InsectMovement(SpiderTemp.transform));
        }

    }

    public void Insect3()
    {
        List<GameObject> Spiders = new List<GameObject>();

        for (int a = 0; a <= 30; a++)
        {
            Vector3 Pos = new Vector3(Random.Range(Target.transform.position.x - 2, Target.transform.position.x + 2), Target.transform.position.y, Random.Range(Target.transform.position.z - 2, Target.transform.position.z + 2));
            GameObject SpiderTemp = Instantiate(PInsect3, Pos, Quaternion.identity);
            Spiders.Add(SpiderTemp);
            StartCoroutine(InsectMovement(SpiderTemp.transform));
        }

    }

    IEnumerator InsectMovement(Transform Insect)
    {
        int a = 0;
        while(true)
        {
            if(a == 250)
            {
                StartCoroutine(Skeloton(Insect.gameObject));
            }
            a++;
            Vector3 pos = Targets[Random.Range(0,Targets.Count)].transform.position;
            Insect.gameObject.transform.LookAt(pos);
            Insect.gameObject.transform.DOMove(pos, Random.Range(10, 15));
            yield return new WaitForSeconds(Random.Range(100,300)* Time.deltaTime);
        }
    }

    IEnumerator Skeloton(GameObject other)
    {
        yield return new WaitForSeconds(1);
        GameObject SLeton = Instantiate(Skeleton, other.transform.position, Quaternion.identity);
        GameObject BoolSpalt = Instantiate(Bloodsplat, new Vector3(other.transform.position.x, 2, other.transform.position.z), Quaternion.identity);
        other.gameObject.SetActive(false);
    }

}
