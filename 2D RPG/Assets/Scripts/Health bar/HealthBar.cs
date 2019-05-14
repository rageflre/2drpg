using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    GameObject incompleteObject;
    [SerializeField]
    GameObject progressionObject;
    [SerializeField]
    RectTransform parentTrans;

    private void Start()
    {
        parentTrans = transform.GetComponent<RectTransform>();

        incompleteObject.GetComponent<RectTransform>().sizeDelta = new Vector2((parentTrans.sizeDelta.x - 2), (parentTrans.sizeDelta.y - 2));
    }

    public void SetFloat(float value)
    {
        float x = Mathf.Lerp(0, parentTrans.sizeDelta.x, value / 100);

        progressionObject.GetComponent<RectTransform>().sizeDelta = new Vector2(x, (parentTrans.sizeDelta.y - 2));
    }

    float progress = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            progress++;
            SetFloat(progress);
        }
    }
}
