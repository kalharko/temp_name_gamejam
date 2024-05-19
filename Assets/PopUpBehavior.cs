using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpBehavior : MonoBehaviour
{

    // rules sprites
    [SerializeField] private List<Image> toppings;
    [SerializeField] private List<Image> fillings;
    [SerializeField] private List<Image> plates;

    // Arrows
    [SerializeField] private List<GameObject> arrows;

    // title
    [SerializeField] private GameObject title;

    // description
    [SerializeField] private GameObject description;

    // PopUp Screen
    [SerializeField] private GameObject popUpScreen;

    // Aliens
    [SerializeField] private List<GameObject> aliens;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPopUp(int table_index)
    {
        popUpScreen.SetActive(true);
        arrows[table_index].SetActive(true);
        aliens[table_index].SetActive(true);
    }

    public void ClosePopUp()
    {
        popUpScreen.SetActive(false);
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
        foreach (GameObject alien in aliens)
        {
            alien.SetActive(false);
        }

        title.SetActive(false);
        description.SetActive(false);

        foreach (Image topping in toppings)
        {
            topping.enabled = false;
        }
        foreach (Image filling in fillings)
        {
            filling.enabled = false;
        }
        foreach (Image plate in plates)
        {
            plate.enabled = false;
        }

        // Resume time
        StartCoroutine(ResumeTime());
    }

    // Coroutine pour animer la reprise du temps
    public IEnumerator ResumeTime()
    {
        while (Time.timeScale + 0.05 < 1.0f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1.0f, 0.1f);
            // Wait 0.01 second
            yield return new WaitForSeconds(0.041f);
        }
        Time.timeScale = 1.0f;
    }
}
