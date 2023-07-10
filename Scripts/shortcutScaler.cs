using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shortcutScaler : MonoBehaviour
{
    [SerializeField] RectTransform topPoint, lowPoint;
    [SerializeField] float spacer;
    [SerializeField] RectTransform[] shortcutObjects;

    // Update is called once per frame
    void Update()
    {
        float totalLength = Vector2.Distance(lowPoint.position, topPoint.position);

        for (int i = 0; i < shortcutObjects.Length; i++)
        {
            float formula = topPoint.position.y - (totalLength * (spacer * i));

            shortcutObjects[i].position = new Vector3(75, formula, 0);
        }
    }
}//EndScript