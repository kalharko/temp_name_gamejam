using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpBehavior : MonoBehaviour
{

    // rules image
    [SerializeField] private RuleImageBehavior ruleImage;

    // Arrows
    [SerializeField] private List<GameObject> arrows;

    // title
    [SerializeField] private TextMeshProUGUI title;

    // description
    [SerializeField] private TextMeshProUGUI description;

    // flavor text
    [SerializeField] private TextMeshProUGUI flavorText;

    // PopUp Screen
    [SerializeField] private GameObject popUpScreen;

    // Background
    [SerializeField] private GameObject background;

    // Aliens
    [SerializeField] private List<GameObject> zones;

    // Flavor text options
    [SerializeField] private List<string> flavorTextOptions;

    // Start is called before the first frame update
    void Start()
    {
        // Hide all elements
        background.SetActive(false);
        popUpScreen.SetActive(false);
        title.text = "";
        description.text = "";
        flavorText.text = "";
        ruleImage.EraseRuleImage();
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
        foreach (GameObject zone in zones)
        {
            zone.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPopUp(int table_index)
    {
        background.SetActive(true);
        popUpScreen.SetActive(true);
        arrows[table_index].SetActive(true);
        zones[table_index].SetActive(true);
        title.text = "Nouvelle commande !";

        // set flavor text
        flavorText.transform.gameObject.SetActive(true);
        flavorText.text = flavorTextOptions[Random.Range(0, flavorTextOptions.Count)];

        // set description
        List<List<string>> rule = GameManager.Instance.GetRuleAtIndex(table_index + 1);
        string descriptionText = "Un sushi avec ";
        foreach (List<string> rule_part in rule)
        {
            if (rule_part.Count == 0) continue;
            
            for (int i = 0; i < rule_part.Count; i++)
            {
                descriptionText += rule_part[i] + " ou ";
            }
            // Remove last " ou "
            descriptionText = descriptionText.Substring(0, descriptionText.Length - 4);
            descriptionText += " et ";
        }
        // Remove last " et "
        descriptionText = descriptionText.Substring(0, descriptionText.Length - 4);
        descriptionText += ", please <3";
        description.transform.gameObject.SetActive(true);
        description.text = descriptionText;

        // set rules images
        ruleImage.SetRuleImage(rule);

        // Stop time
        StartCoroutine(StopTime());
    }

    public void ClosePopUp()
    {
        popUpScreen.SetActive(false);
        title.text = "";
        description.text = "";
        flavorText.text = "";
        ruleImage.EraseRuleImage();
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
        foreach (GameObject zone in zones)
        {
            zone.SetActive(false);
        }
        
        background.SetActive(false);

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
    
    // Coroutine pour animer l'arrêt du temps
    public IEnumerator StopTime()
    {
        while (Time.timeScale - 0.05 > 0.0f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.0f, 0.1f);
            // Wait 0.01 second
            yield return new WaitForSeconds(0.041f);
        }
        Time.timeScale = 0.0f;
    }
}
