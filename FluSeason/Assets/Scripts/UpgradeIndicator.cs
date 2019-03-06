using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeIndicator : MonoBehaviour
{
    GameObject upgrade = null;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (upgrade == null)
        {
            upgrade = GameObject.FindGameObjectWithTag("FastSingleUpgrade");
        }
        if (upgrade == null)
        {
            upgrade = GameObject.FindGameObjectWithTag("SpreadUpgrade");
        }
        if (upgrade == null)
        {
            upgrade = GameObject.FindGameObjectWithTag("StreamUpgrade");
        }
        if (upgrade == null)
        {
            upgrade = GameObject.FindGameObjectWithTag("OPUpgrade");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(upgrade == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            float angle = Mathf.Atan2(upgrade.transform.position.y - this.transform.position.y, upgrade.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;

            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }
}
