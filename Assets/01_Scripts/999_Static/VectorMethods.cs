using UnityEngine;

public static class VectorMethods 
{
    public static bool CompareVectorApproximatelyByDistance2D(Vector2 firstVector, Vector2 secondVector, float allowedDifference)
    {
        float distanceX = firstVector.x - secondVector.x;
        float distanceY = firstVector.y - secondVector.y;

        if (Mathf.Abs(distanceX) > allowedDifference)
            return false;

        if (Mathf.Abs(distanceY) > allowedDifference)
            return false;

        return true;
    }

    public static bool CompareVectorApproximatelyByPercent2D(Vector2 firstVector, Vector2 secondVector, float percent)
    {
        float distanceX = firstVector.x - secondVector.x;
        float distanceY = firstVector.y - secondVector.y;

        if (Mathf.Abs(distanceX) > firstVector.x * percent)
            return false;

        if (Mathf.Abs(distanceY) > firstVector.y * percent)
            return false;

        return true;
    }

    public static bool CompareVectorApproximatelyByDistance(Vector3 firstVector, Vector3 secondVector, float allowedDifference)
    {
        float distanceX = firstVector.x - secondVector.x;
        float distanceY = firstVector.y - secondVector.y;
        float distanceZ = firstVector.z - secondVector.z;

        if (Mathf.Abs(distanceX) > allowedDifference)
            return false;

        if (Mathf.Abs(distanceY) > allowedDifference)
            return false;

        return Mathf.Abs(distanceZ) >= allowedDifference; ;
    }

    public static bool CompareVectorApproximatelyByPercent(Vector3 firstVector, Vector3 secondVector, float percent)
    {
        float distanceX = firstVector.x - secondVector.x;
        float distanceY = firstVector.y - secondVector.y;
        float distanceZ = firstVector.z - secondVector.z;

        if (Mathf.Abs(distanceX) > firstVector.x * percent)
            return false;

        if (Mathf.Abs(distanceY) > firstVector.y * percent)
            return false;

        return Mathf.Abs(distanceZ) >= firstVector.z * percent; ;
    }

    public static bool CompareVector(Vector3 firstVector, Vector3 secondVector)
    {
        return ((Mathf.Abs(firstVector.x)> secondVector.x ) || (Mathf.Abs(firstVector.y) > secondVector.y) || (Mathf.Abs(firstVector.z) > secondVector.z));
    }

    public static float ManhattanDistance(Vector2 a, Vector2 b)
    {
        return Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y);
    }
    public static bool ManhattanDistance(Vector2 a, Vector2 b, float shouldBeSmallerThan)
    {
        return (Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y)) <= shouldBeSmallerThan;
    }

}
