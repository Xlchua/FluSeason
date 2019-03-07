using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    private List<Animator> animatorList;

    private List<GameObject> textObjects;

    public float waitTime;

    public static AnimatorScript instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animatorList = new List<Animator>();
        
        foreach (Transform child in transform)
        {
            animatorList.Add(child.GetComponentInChildren<Animator>());
        }
    }

    IEnumerator Animate()
    {
        foreach(Animator a in animatorList)
        {
            a.SetTrigger("DoAnimation");
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void ToggleText(bool toggle)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(toggle);
        }
    }

    public void StartAnimation()
    {
        ToggleText(true);
        StartCoroutine("Animate");
    }

    public void StopAnimation()
    {
        StopCoroutine("Animate");
        ToggleText(false);
    }
}
