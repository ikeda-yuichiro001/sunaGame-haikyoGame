using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioHandle : MonoBehaviour
{
    public enum AudioType { Se, Bgm, Vc, Ss }
    public AudioType type;
    AudioType Lt;

    void Start() => UPDATE(); 
    void Update() { if (type != Lt) UPDATE(); }

    void UPDATE()
    {
        GetComponent<AudioSource>().volume =  
            type == AudioType.Bgm ?
            OptionData.Sound_Bgm :
            (
                type == AudioType.Se ?
                OptionData.Sound_Se :
                (
                    type == AudioType.Vc ?
                    OptionData.Sound_Vc :
                    OptionData.Sound_Ss
                )
            );

        Lt = type;
    }
     
}

//データ追加時にはsave, Load, SetDef～ 関数の3か所に処理を追加
public static class OptionData
{
    public static float Sound_Se, Sound_Bgm, Sound_Vc, Sound_Ss, Bright;
    public static int Lang;  //0 = jp,  1 = en,  2 = ch
    public static bool SightReturn;
    public static float MouseSensivirity;
    // Save, Reload, Deff, Return
    public static void Save()
    {
        //データ形成
        string x = 
            (((int)(Sound_Se * 100)).ToString()).PadLeft(5, ' ') +
            (((int)(Sound_Bgm * 100)).ToString()).PadLeft(5, ' ') +
            (((int)(Sound_Vc * 100)).ToString()).PadLeft(5, ' ') +
            (((int)(Sound_Ss * 100)).ToString()).PadLeft(5, ' ') +
            (((int)(Bright * 100)).ToString()).PadLeft(5, ' ') +
            (Lang.ToString()).PadLeft(5, ' ') +
            ((SightReturn ? "1" : "0")).PadLeft(5, ' ') +
             (((int)(MouseSensivirity * 100)).ToString()).PadLeft(5, ' ');
        
        //書き込み
        SaveSystem.SAVE("option", x);
    }

    public static void Read()
    {
        string dat = SaveSystem.LOAD("option", "NONE");
        if (dat == "NONE")
        {
            SetDEFAULT();
        }
        else
        {
            if (dat.Length == 00) SetDEFAULT(true);
            if (dat.Length >= 05) Sound_Se  = int.Parse(dat.Substring(00, 5)) * 0.01f;
            if (dat.Length >= 10) Sound_Bgm = int.Parse(dat.Substring(05, 5)) * 0.01f;
            if (dat.Length >= 15) Sound_Vc  = int.Parse(dat.Substring(10, 5)) * 0.01f;
            if (dat.Length >= 20) Sound_Ss  = int.Parse(dat.Substring(15, 5)) * 0.01f;
            if (dat.Length >= 25) Bright    = int.Parse(dat.Substring(20, 5)) * 0.01f;
            if (dat.Length >= 30) Lang      = int.Parse(dat.Substring(25, 5));
            if (dat.Length >= 35) SightReturn = dat.Substring(30, 5).Trim() == "1";
            if (dat.Length >= 40) MouseSensivirity = int.Parse(dat.Substring(35, 5)) * 0.01f;
        }
    }

    public static void SetDEFAULT(bool IsSave = true)
    {
        Sound_Se = Sound_Bgm = Sound_Vc = Sound_Ss = 0.6f;
        Bright = 0.9f;
        MouseSensivirity = 1;
        Lang = 0;
        SightReturn = false;
        if (IsSave) Save();
    }

}



public class SaveSystem
{
    static string getPath
    {
        get
        {
            string ee = Application.dataPath.Replace("/", @"\") + @"\" + (Application.isEditor ? @"[$DebugSaveData$]\" : @"\");
            if (Application.isEditor && !Directory.Exists(ee)) Directory.CreateDirectory(ee);
            return ee;
        }
    }
    public static void SAVE(string LP, string data)
    { 
        string ee = getPath + LP; 
        if (!File.Exists(ee)) File.Create(ee).Dispose();
        File.WriteAllText(ee, data);
    }

    public static string LOAD(string LP, string NonFiles, bool NoneTimeCreate = true)
    {
        string ee = getPath + LP;
        if (File.Exists(ee)) return File.ReadAllText(ee);
        if (NoneTimeCreate) File.Create(ee).Dispose();
        return NonFiles; 
    }

}

public class LangUI
{
    public static Texture2D GetImage(string name)
    {
        string x = "UI/Langs/" + name + "_" + (OptionData.Lang == 0 ? "jp" : (OptionData.Lang == 2 ? "en" : "cn"));
        Debug.Log(x);
        return Resources.Load(x) as Texture2D;
    }
}