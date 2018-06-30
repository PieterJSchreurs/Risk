using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public GameObject Panel;
    public GameObject playerInputprefab;
    private int amountOfPlayers = 2;

    public List<string> PlayerNames = new List<string>();


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetAllInputs()
    {
        Transform[] allChildren = Panel.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.tag == "Name")
            {
                Text input = child.GetComponent<Text>();
                PlayerNames.Add(input.text);
            }
        }
    }

    public void AddPlayer()
    {
        if (amountOfPlayers < 6)
        {
            Vector3 inputPostion = new Vector3(330, 760 + (-90 * (amountOfPlayers - 2)), 0);
            GameObject playerInput = Instantiate(playerInputprefab, inputPostion, Quaternion.Euler(0, 0, 0));
            playerInput.transform.SetParent(Panel.transform, false);

            playerInput.transform.position = inputPostion;
            amountOfPlayers++;
        }
    }

    public void StartLevel()
    {
        PlayerTurnHandler.amountOfPlayers = amountOfPlayers;
        GetAllInputs();
        SceneManager.LoadScene("LevelOne");
    }
}
