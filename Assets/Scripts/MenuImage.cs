using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class MenuImage : MonoBehaviour
{


    // When clicking start Button the application must start
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    //public void OnStartButton() {



    //    SceneManager.LoadScene(1);

        

    //}


    // When clicking quit button the app must quit
    //public void QuitGame()
    //{

      //  Application.Quit();
    //}
    //public void OnQuitbutton() {

}
    //    Application.Quit();

  //  }
//}
//[ExecuteInEditMode]
//public class SC_BackgroundScaler : MonoBehaviour
//{
//    Image backgroundImage;
//    RectTransform rt;
//    float ratio;

    // Start is called before the first frame update
//    void Start()
//    {
//        backgroundImage = GetComponent<Image>();
//        rt = backgroundImage.rectTransform;
//        ratio = backgroundImage.sprite.bounds.size.x / backgroundImage.sprite.bounds.size.y;
//    }

    // Update is called once per frame
//    void Update()
//    {
//        if (!rt)
//            return;

        //Scale image proportionally to fit the screen dimensions, while preserving aspect ratio
//        if(Screen.height * ratio >= Screen.width)
//        {
//            rt.sizeDelta = new Vector2(Screen.height * ratio, Screen.height);
//        }
//        else
//        {
//            rt.sizeDelta = new Vector2(Screen.width, Screen.width / ratio);
//        }
        // Put the play button menu at the center of the screen
//        rt.anchoredPosition = new Vector2(0, 0);

        // Put the quit game button at the bottom of the screen
//        rt.anchoredPosition = new Vector2(0, -rt.sizeDelta.y / 2);
//    }
//}