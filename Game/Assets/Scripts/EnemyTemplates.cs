using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplates : MonoBehaviour
{
    [SerializeField]

    public List<GameObject> eSP_DemonDepths;
    public List<GameObject> eSP_DemonBasic;
    public List<GameObject> eSP_Neutral;
    public List<GameObject> eSP_AngelBasic;
    public List<GameObject> eSP_AngelHeights;

    public GameObject GetTemplateOfLevel(int level)
    {
        switch (level)
        {
            case -2:
                return eSP_DemonDepths[Random.Range(0, eSP_DemonDepths.Count)];
            case -1:
                return eSP_DemonBasic[Random.Range(0, eSP_DemonBasic.Count)];
            case 0:
                return eSP_Neutral[Random.Range(0, eSP_Neutral.Count)];
            case 1:
                return eSP_AngelBasic[Random.Range(0, eSP_AngelBasic.Count)];
            case 2:
                return eSP_AngelHeights[Random.Range(0, eSP_AngelHeights.Count)];
            default:
                return null;
        }
    }
}
