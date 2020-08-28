using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    #region Initializers
    [Header("Floor Variables")]
    public float floorSpeed;
    [HideInInspector]
    public FloorMove floors;

    [Header("Characters")]
    public float jumpForce;
    public float clickRange;
    public float fallMultiplier;
    public List<Vector2> charpositions = new List<Vector2>();
    public List<GameObject> playersLeft = new List<GameObject>();
    public Transform playersParent;
    [HideInInspector]
    public int numberOfPlayers;

    #region States

        [HideInInspector]
        public bool started;
        [HideInInspector]
        public bool won;
        [HideInInspector]
        public bool lost;
        [Header("States")]
        public bool stop;
        public float timeToStop;

    #endregion

    [Header("UI")]
    public GameObject winButton;
    public GameObject loseButton;
    public Slider slider;
    private float t;
    private float changeByFloorSpeed;
    private int floorNum;
    private float timeToFinish;
    private float timeSinceStart;
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        changeByFloorSpeed = 10 / floorSpeed;
        floors = FindObjectOfType<FloorMove>();
        floorNum = GameObject.FindGameObjectsWithTag("Floor").Length;
        timeSinceStart = 0;
        timeToFinish = changeByFloorSpeed * (floorNum - 1);
        t = 0;
        
        //add children
        for (int i = 0; i < playersParent.childCount; i++)
        {
            playersLeft.Add(playersParent.GetChild(i).gameObject);
            Animator playerAnim = playersParent.GetChild(i).GetComponent<Animator>();
            int whichIdle = Random.Range(1, 3);
            playerAnim.SetInteger("Idle", whichIdle);
        }
        numberOfPlayers = playersLeft.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(started&& timeSinceStart<=timeToFinish)
        {
            timeSinceStart += Time.deltaTime;
            slider.value = timeSinceStart / timeToFinish;
        }

        if(stop && t <= timeToStop)
        {
            t += Time.deltaTime;
            UpdateFloorToStop(t / timeToStop);
        }
    }
    public void UpdateFloorToStop(float progress)
    {
        float changingspeed = Mathf.SmoothStep(floorSpeed, 0, progress);
        floors.floorspeed = changingspeed;
    }

    public void GameStart()
    {
        started = true;
        foreach (GameObject Player in playersLeft)
        {
            Animator playerAnim = Player.GetComponent<Animator>();
            playerAnim.SetTrigger("run");
        }

        floors.floorspeed = floorSpeed;
        floors.start = true;
    }

    public void Lose()
    {
        GetComponent<Animator>().SetTrigger("Lose");
        stop = true;
        loseButton.SetActive(true);
        Debug.Log("Lost");
    }

    public void Win()
    {
        GetComponent<Animator>().SetTrigger("Win");
        stop = true;
        foreach (GameObject Player in playersLeft)
        {
            Animator playerAnim = Player.GetComponent<Animator>();
            int whichWin = Random.Range(1, 4);
            playerAnim.SetInteger("Win", whichWin);
        }
        Debug.Log("Won");
        winButton.SetActive(true);
    }

    public void Restart()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    public void OnePlayerLess()
    {
        numberOfPlayers--;
        if(numberOfPlayers<1)
        {
            Lose();
        }
    }

    
}
