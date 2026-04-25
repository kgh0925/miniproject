using UnityEngine;

public static class AchiveCheck
{
    public static void Achive(string stageName, StageClearData data, out int[] isClear)
    {
        isClear = new int[2];
        switch (stageName)
        {
            case "Stage_One":
                if (data.MinTime < 1 || (data.MinTime == 1 && data.SecTime == 0))
                {
                    isClear[0] = 1;
                }
                if (data.CollisionCount <= 0)
                {
                    isClear[1] = 1;
                }
                break;
            case "Stage_Two":
                if (data.MinTime < 3 || (data.MinTime == 3 && data.SecTime <= 0))
                {
                    isClear[0] = 1;
                }
                if (data.CollisionCount <= 0)
                {
                    isClear[1] = 1;
                }
                break;
            case "Stage_Three":
                if (data.MinTime < 5 || (data.MinTime == 5 && data.SecTime <= 0))
                {
                    isClear[0] = 1;
                }
                if (data.CollisionCount <= 0)
                {
                    isClear[1] = 1;
                }
                break;

        }
    }
}
