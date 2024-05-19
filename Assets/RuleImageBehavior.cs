using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRuleImage(List<List<string>> rule) {
        GameObject new_image_set = new GameObject();
        new_image_set.transform.parent = transform;

        // count number of rule set not empty
        int line_count = 0;
        foreach (List<string> line in rule) {
            if (line.Count > 0) {
                line_count++;
            }
        }

        // set the y position
        float y = top_y[line_count - 1];

        // for each line in the rule
        for (int line = 0; line < rule.Count; line++) {
            // count number of elements in the line
            int element_count = rule[line].Count;

            // set the x position
            float x = left_x[element_count - 1];

            // for each element in the line
            for (int i = 0; i < rule[line].Count; i++) {
                // spawn the element
                GameObject new_image = Instantiate(imagePrefab, new_image_set.transform);
                new_image.transform.position = new Vector3(x, y, 0);
                x += x_offset[element_count - 1];

                // if not the last element in the line
                if (i < rule[line].Count - 1) {
                    // spawn the "ou" prefab
                    GameObject new_or = Instantiate(orPrefab, new_image_set.transform);
                    new_or.transform.position = new Vector3(x, y, 0);
                    x += x_offset[element_count - 1];
                }
            }
            // y offset
            y -= y_offset[line_count - 1];

            // if not the last line
            if (line < rule.Count - 1) {
                // spawn the "et" prefab
                GameObject new_and = Instantiate(andPrefab, new_image_set.transform);
                new_and.transform.position = new Vector3(0, y, 0);
                y -= y_offset[line_count - 1];
            }
        }

    }

    public void EraseRuleImage()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
    }
}
