using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class autoMoving : MonoBehaviour
{

    public string a;
    public GameObject Enable;
    public string FishUrl;
    //public GameObject board;
    public Text Ename;



    private GameObject camera;
    private GameObject fish;
    private Vector3 ScreenCenter;


    float speed = 10.0f;


    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        //fish = GameObject.FindWithTag("fish");
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

    }

    // Update is called once per frame
    void Update()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        RaycastHit hit = new RaycastHit();


        transform.Translate(Vector3.forward * speed * Time.deltaTime, camera.transform);

 
       if (Physics.Raycast(ray, out hit, 30.0f))
        {
            if (hit.collider.tag == "fish")
            {
                FishUrl = "dpdms126.cafe24.com/FishDB.php";
                a = hit.transform.name;
                Debug.Log(a);
               
                StartCoroutine(FSDBCo());     

                distance = Vector3.Distance(camera.transform.position, hit.transform.position);

                if (distance <= 10.0f)
                {
                    Enable.SetActive(true);
                    StartCoroutine(timer());
                    speed = 0;
                }
                    
                else
                {
                    speed = 10.0f;
                }
                    
            }
        }
	else { speed = 10.0f; }

    }
    IEnumerator FSDBCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("Fish_number", a);

        WWW webRequest = new WWW(FishUrl, form);
        yield return webRequest;

        Debug.Log(webRequest.text);
        //s = webRequest.text;
        content(webRequest.text);
    }
    void content(string s)
    {
        Input = s;
    }

    private string Input
    {
        get { return Ename.text; }
        set { Ename.text = value; }
    }

    IEnumerator timer()
    {

        yield return new WaitForSeconds(5);
        Enable.SetActive(false);

    }

}

        