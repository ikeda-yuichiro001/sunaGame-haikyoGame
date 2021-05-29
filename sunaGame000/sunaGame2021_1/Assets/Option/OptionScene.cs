using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    Sound_Se,
    Sound_Bgm,
    Sound_Vc,
    Sound_Ss,
    Bright
    Lang  //0 = jp,  1 = en,  2 = ch
    SightReturn;
    MouseSensivirity;
    Save
    Reload
    Deff
    Return
*/

public class OptionScene : MonoBehaviour
{
    public RawImage TitleText;
    [Range(0, 4)]
    public int Select, Lsel;
    public RawImage[] buttom;
    public enum Mode { BgmVol, SeVol, VcVol, SsVol, Bright, SightReturn, MouseSensivirity, Lang, B_Save, B_Reload, B_Deff, B_Return}
    public Mode mode;

    bool IsNext;
    public AudioSource SelectSE, NextSe;

    [Range(-1, 1)]
    public float c; //長押しをカウントして一定時間長押ししたら移動させる

    [Range(0.5f, 5)] public float sensivirity = 1.5f;

    public float _x;


    void Start()
    {
        OptionData.Read();
        IsNext = false;
        TitleText.texture = LangUI.GetImage("titleText");
        for (int v = 0; v < buttom.Length; v++)
        {
            buttom[v].texture = LangUI.GetImage("titieBtm" + v);
        }
    }

    void Update()
    {
        if (IsNext) return;

        //キー入力------------------------------------------------------------------------------------------------------------
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) c -= Time.deltaTime * sensivirity;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) c += Time.deltaTime * sensivirity;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) Select--;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) Select++;
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) c = 0;

        if (c < -1)
        {
            c += 1;
            Select--;
        }
        else if (c > 1)
        {
            c -= 1;
            Select++;
        }


        if (Select > Enum.GetNames(typeof(Mode)).Length) Select = 0;
        if (Select < 0) Select = Enum.GetNames(typeof(Mode)).Length;
        mode = (Mode)Enum.ToObject(typeof(Mode), Select);


        //-------------------------------------------------
        // Key入力 ----------------------------------------
        //-------------------------------------------------
        int xp = 0;

        if (Input.GetKeyDown(KeyCode.A)) xp--;

        if (Input.GetKeyDown(KeyCode.D)) xp++;

        if (Input.GetKey(KeyCode.A))
        {
            _x -= Time.deltaTime * sensivirity;
            if (_x < -1) { xp--; _x = 0; }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _x += Time.deltaTime * sensivirity;
            if (_x > 01) { xp++; _x = 0; }
        }
        else
            _x = 0;

        if (xp > 01) xp = 01;
        if (xp < -1) xp = -1;
        Debug.Log(":::" + xp);

        /*
        
        switch (mode)
        {
            case Mode.BgmVol :  break;           
            case Mode.ReDRT  :  break;
        
        }


        */

        xp = 0;
                

        
        //色の更新 ----------------------------------------------------------
        for (int c = 0; c < buttom.Length; c++)
        {
            buttom[c].color = c == Select ? Color.white : Color.gray;
        }

        //切り替わり時SEを鳴らす----------------------------------------------
        if (Lsel != Select || xp != 0)
        {
            SelectSE.Play();
            Lsel = Select;
        }
    }
}

 