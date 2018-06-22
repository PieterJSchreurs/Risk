using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour, ISelectAble
{

    private bool _isSelected = false;
    private Renderer _meshRenderer;
    private Player _playerOwner;
    public string countryName;
    private Color32 _currentColor;
    private CountryHandler _countryHandler;
    private GameObject _map;
    private Army _currentArmyOnCountry = new Army();

    void Awake()
    {
        _meshRenderer = GetComponent<SpriteRenderer>();
        _map = GameObject.FindGameObjectWithTag("Map");
        _countryHandler = _map.GetComponent<CountryHandler>();
        _currentArmyOnCountry.amountOfSoldiers = 100;
      
    }

    public void Start()
    {
        _playerOwner = PlayerTurnHandler.GetCurrentPlayer();
        _meshRenderer.material.color = _playerOwner.playerColor;
    }

    public Army getArmy()
    {
        return _currentArmyOnCountry;
    }

    public void OnMouseExit()
    {
        NormalColor();
    }

    public void OnMouseEnter()
    {
        SelectHighLightColor();
    }

    public void Select()
    {
        _isSelected = true;
        _countryHandler.SetSelectedCountry(this);
        _meshRenderer.material.color = _playerOwner.playerHighLightColor;
    }

    public void Deselect()
    {
        _isSelected = false;
        NormalColor();
    }

    private void SelectHighLightColor()
    {
        if (_isSelected == false)
        {
            _meshRenderer.material.color = _playerOwner.playerHighLightColor;
        }
    }

    private void NormalColor()
    {
        if (_isSelected == false)
        {
            _meshRenderer.material.color = _playerOwner.playerColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public Player GetPlayerOwner()
    {
        return _playerOwner;
    }

    public void SetPlayerOwner(Player player)
    {
        _playerOwner = player;
        _meshRenderer.material.color = _playerOwner.playerColor;
    }

    public void SetName(string name)
    {
        countryName = name;
    }

    public string GetName()
    {
        return countryName;
    }
}
