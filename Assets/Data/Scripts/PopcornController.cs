using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornController : MonoBehaviour
{

    private bool isPoped;
    private bool isScored;
    private bool isReadyToDestroy;
    private float timer;
    private int score;
    [Header("Popcorn Status")]
    public float cookLevel;
    public float wellDoneLevel;
    public float overCookLevel;
    public float burnedLevel;
    public GameObject scoreText;
    
    private GameObject popcornModel;
    private Renderer popcornRenderer;
    private MeshFilter popcornMesh;
    [Header("Popcorn color")]
    public Color rawColor;
    public Color wellDoneColor;
    public Color overCookColor;
    public Color burnedColor;
    [Header("Popcorn Model")]
    public Mesh rawMesh;
    public Mesh wellDoneMesh;

    // Start is called before the first frame update
    void Start()
    {
        isScored = false;
        isPoped = false;
        isReadyToDestroy = false;
        timer = 0;
        cookLevel = 0;
        popcornModel = this.transform.GetChild(0).gameObject;
        popcornRenderer = popcornModel.GetComponent<Renderer>();
        popcornMesh = popcornModel.GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCooking();

        if (isReadyToDestroy == true)
        {
            DestructionTimer();
        }
    }

    void OnTriggerEnter()
    {
        if (isPoped == false)
        {
            CheckPopcorn(cookLevel);
        }
        else if (isPoped == true)
        {
            ConvertToPoint(cookLevel);
        }
    }

    void StartCooking()
    {
        if (isPoped == false)
        {
            cookLevel += Time.deltaTime;
            ChangePopcornColor(cookLevel);

            if (cookLevel >= burnedLevel)
            {
                //This Popcorn is Burned
            }
        }
    }

    void ChangePopcornColor(float status)
    {
        if (status >= wellDoneLevel && status < overCookLevel)
        {
            //Change popcorn to WELLDONE Color
            popcornRenderer.material.color = wellDoneColor;
        }
        else if (status < wellDoneLevel)
        {
            //Change popcorn to RAW Color
            popcornRenderer.material.color = rawColor;
        }
        else if (status >= overCookLevel && status < burnedLevel)
        {
            //Change popcorn to OVERCOOK Color
            popcornRenderer.material.color = overCookColor;
        }
        else if (status >= burnedLevel)
        {
            //Change popcorn to BURNED Color
            popcornRenderer.material.color = burnedColor;
        }
    }

    void CheckPopcorn(float timer)
    {
        if (timer >= wellDoneLevel && timer < overCookLevel)
        {
            //Popcorn Success and change to WELLDONE popcorn Model.
            popcornMesh.mesh = wellDoneMesh;
            isPoped = true;
        }
        else if (timer < wellDoneLevel)
        {
            //Popcorn Failed and keep the current model and color.
            popcornMesh.mesh = wellDoneMesh;
            isPoped = true;
        }
        else if (timer >= overCookLevel && timer < burnedLevel)
        {
            //Popcorn Success and change to OVERCOOK popcorn Model.
            popcornMesh.mesh = wellDoneMesh;
            isPoped = true;
        }
        else if (timer >= burnedLevel)
        {
            //Popcorn Failed and change to Burned popcorn Model.
            popcornMesh.mesh = wellDoneMesh;
            isPoped = true;
        }
    }

    void ConvertToPoint(float status)
    {
        if (isScored == false)
        {
            if (status >= wellDoneLevel && status < overCookLevel)
            {
                //Count score when WELLDONE
                float a = Mathf.Abs(status - overCookLevel);
                float b = overCookLevel - wellDoneLevel;
                float c = (a * 200) / b;
                score = (int)c;
                GameManager.Instance.scoreWelldone++;
            }
            else if (status < wellDoneLevel)
            {
                //Count score when UNDERCOOK
                float a = Mathf.Abs(status - wellDoneLevel);
                float b = wellDoneLevel;
                float c = (a * 200) / b;
                score = -(int)c;
                GameManager.Instance.scoreUndercook++;
            }
            else if (status >= overCookLevel && status < burnedLevel)
            {
                //Count score when OVERCOOK
                float a = Mathf.Abs(status - burnedLevel);
                float b = burnedLevel - overCookLevel;
                float c = (a * 100) / b;
                score = (int)c;
                GameManager.Instance.scoreOvercook++;
            }
            else if (status >= burnedLevel)
            {
                //Count score when BURNED
                float a = Mathf.Abs(status - burnedLevel);
                float c = (a * 100);
                score = -(int)c;
                GameManager.Instance.scoreBurned++;
            }
            isScored = true;
            GameManager.Instance.score += score;
            //set the Floating text above popcorn to show score
            TextMesh textText = scoreText.GetComponent<TextMesh>();
            textText.text = score.ToString();
            isReadyToDestroy = true;
        }
    }

    void DestructionTimer()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            Destroy(this.gameObject);
        }
    }
}
