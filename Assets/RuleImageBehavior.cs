using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleImageBehavior : MonoBehaviour
{
    // "ou" prefab
    [SerializeField] private GameObject orPrefab;

    // "et" prefab
    [SerializeField] private GameObject andPrefab;

    // image prefab
    [SerializeField] private GameObject imagePrefab;

    // positions
    [SerializeField] private List<float> top_y;
    [SerializeField] private List<float> left_x;
    [SerializeField] private List<float> y_offset;
    [SerializeField] private List<float> x_offset;

    [SerializeField] private List<float> scales;


    private Image bg_image;

    // Start is called before the first frame update
    void Start()
    {
        bg_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRuleImage(List<List<string>> rule)
    {
        bg_image.enabled = true;

        //lists of assets
        List<Sprite> assets = new List<Sprite>();
        assets.AddRange(GameManager.Instance.sprite_fillings);
        assets.AddRange(GameManager.Instance.sprite_toppings);
        assets.AddRange(GameManager.Instance.sprite_plates);

        // count number of rule set not empty
        int line_count = 0;
        foreach (List<string> line in rule)
        {
            if (line.Count > 0)
            {
                line_count++;
            }
        }

        // set the y position
        float y = top_y[line_count - 1];

        // for each line in the rule
        for (int line = 0; line < rule.Count; line++)
        {
            // if the line is empty skip
            if (rule[line].Count == 0) continue;

            // count number of elements in the line
            int element_count = rule[line].Count;

            // set the x position
            float x = left_x[element_count - 1];

            // for each element in the line
            for (int i = 0; i < rule[line].Count; i++)
            {
                // spawn the element
                GameObject new_image = Instantiate(imagePrefab, transform);
                new_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                Debug.Log("searching for" + rule[line][i]);
                new_image.GetComponent<Image>().sprite = assets.Find(sprite => sprite.name == rule[line][i]);
                x += x_offset[element_count - 1];

                // if not the last element in the line
                if (i < rule[line].Count - 1)
                {
                    // spawn the "ou" prefab
                    GameObject new_or = Instantiate(orPrefab, transform);
                    new_or.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                    x += x_offset[element_count - 1];
                }
            }
            // y offset
            y -= y_offset[line_count - 1];

            // if not the last line
            if (line + 1 < rule.Count && rule[line + 1].Count > 0)
            {
                // spawn the "et" prefab
                GameObject new_and = Instantiate(andPrefab, transform);
                new_and.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, y);
                y -= y_offset[line_count - 1];
            }
        }

    }

    public void EraseRuleImage()
    {
        bg_image.enabled = false;
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child == transform) continue;
            Destroy(child.gameObject);
        }
    }
}
