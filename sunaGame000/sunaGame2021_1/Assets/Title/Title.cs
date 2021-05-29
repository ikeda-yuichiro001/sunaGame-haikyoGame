using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Texture2D ee;
    public RawImage TitleText;
    [Range(0,4)]
    public int Select, Lsel;
    public RawImage[] buttom;
    public enum Mode { NewGame, Continue, Option, Strategy, ExitGame }
    public Mode mode;
    bool IsNext;
    public AudioSource SelectSE, NextSe;

    [Range(-1, 1)]
    public float c; //長押しをカウントして一定時間長押ししたら移動させる

    [Range(0.5f,5)] public float sensivirity = 1.5f;




    void Start()
    {
        OptionData.Read();
        IsNext = false; 
        TitleText.texture = LangUI.GetImage("titleText");
        for (int v = 0; v < buttom.Length; v++)
        {
             buttom[v].texture = LangUI.GetImage("titieBtm_" + v);
        }
        ee = LangUI.GetImage("titieBtm0");
    }

    void Update()
    {
        if (IsNext) return;

        //キー入力------------------------------------------------------------------------------------------------------------
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow  )) c -= Time.deltaTime * sensivirity;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) c += Time.deltaTime * sensivirity;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow  )) Select--; 
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


        if (Select > 4) Select = 0;
        if (Select < 0) Select = 4;

        switch (Select)
        {
            case 0: mode = Mode.NewGame; break;
            case 1: mode = Mode.Continue; break;
            case 2: mode = Mode.Option; break;
            case 3: mode = Mode.Strategy; break;
            case 4: mode = Mode.ExitGame; break;
            default: mode = Mode.NewGame; Select = Lsel = 0; break;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            switch (mode)
            {
                case Mode.NewGame : SceneLoader.Load("New"); IsNext = true; break;
                case Mode.Continue: SceneLoader.Load("Continue"); IsNext = true; break;
                case Mode.Option  : SceneLoader.Load("Option"); IsNext = true; break;
                case Mode.Strategy: System.Diagnostics.Process.Start("http://sunanoniwa.xsrv.jp/index.html"); break;
                case Mode.ExitGame: Application.Quit(); break;
            }
            if (mode != Mode.Strategy && mode != Mode.ExitGame) NextSe.Play();
            
        }

        //色の更新 ----------------------------------------------------------
        for (int c = 0; c < buttom.Length; c++)
        {
            buttom[c].color = c == Select ? Color.white : Color.gray;
        }

        //切り替わり時SEを鳴らす----------------------------------------------
        if (Lsel != Select)
        {
            SelectSE.Play();
            Lsel = Select;
        }
    }
     
}
