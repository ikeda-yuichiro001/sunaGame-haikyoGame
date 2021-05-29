using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Logo : MonoBehaviour
{
    RawImage e;
    void Start()
    {
        e = GameObject.Find("Canvas/Image").GetComponent<RawImage>();
        Invoke("NEXT", 1.2f);
        e.color = new Color(1, 1, 1, 0);
    }
    void NEXT  () => SceneLoader.Load("Title");
    void Update() => e.color += new Color(0,0,0, Time.deltaTime);
}
