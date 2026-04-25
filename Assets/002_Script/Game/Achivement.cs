using UnityEngine;
using UnityEngine.UI;

public class Achivement : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] Image[] AchiveArray;

    private void Awake()
    {
        if (AchiveArray == null) return;
        for(int i = 0; i < AchiveArray.Length; i++)
        {
            // PlayerPrefs.SetInt("Achive" + i, 0); 업적 초기화
            int temp = PlayerPrefs.GetInt("Achive" + i, 0);
            
            if (temp == 0)
            {
                AchiveArray[i].enabled = false;
            }
            else
            {
                AchiveArray[i].enabled = true;
            }

        }
    }
}
