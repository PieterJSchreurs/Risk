using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour, ISelectAble {

    public bool isSelected = false;
    private Renderer meshRenderer;
    private Player playerOwner;
    private string CountryName;
    private Color32 currentcolor;

    public void OnMouseExit()
    {
        meshRenderer.material.color = playerOwner.playerColor;
    }

    public void OnMouseEnter()
    {
        meshRenderer.material.color = playerOwner.playerHighLightColor;
    }

    public void Select()
    {

    }

    public void Deselect()
    {

    }

    // Use this for initialization
    void Awake () {
        meshRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isSelected == true)
        {
            
        }
	}

    public void SetPlayerOwner(Player player)
    {
        playerOwner = player;
        Debug.Log("trying to add " + player.username + " to land: " + CountryName);
        meshRenderer.material.color = playerOwner.playerColor;
    }

    public void SetName(string name)
    {
        CountryName = name;
    }

    public string GetName()
    {
        return CountryName;
    }
}
