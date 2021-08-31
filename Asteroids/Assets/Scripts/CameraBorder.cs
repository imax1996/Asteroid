using UnityEngine;

public static class CameraBorder
{
    public static Vector2 GetCoordBorderCamera(Camera camera)
    {
        float borderX = camera.transform.position.x + camera.orthographicSize * camera.aspect;
        float borderY = camera.transform.position.y + camera.orthographicSize;

        Vector2 border = new Vector2(borderX, borderY);

        return border;
    }

    public static Vector2 GetRandomCoordOnBorder(Camera camera)
    {
        Vector2 coordBorderCamera = GetCoordBorderCamera(camera);

        bool isXAxis = Mathf.RoundToInt(Random.value) == 1 ? true : false;
        Vector2 randomCoord = coordBorderCamera;
        if (isXAxis)
        {
            randomCoord = new Vector2(Random.Range(-coordBorderCamera.x, coordBorderCamera.x), randomCoord.y);
        }
        else
        {
            randomCoord = new Vector2(randomCoord.x, Random.Range(-coordBorderCamera.y, coordBorderCamera.y));
        }
        return randomCoord;
    }

    public static Vector2 GetRandomCoordOnBorder(Vector2 coordBorderCamera)
    {
        bool isXAxis = Mathf.RoundToInt(Random.value) == 1 ? true : false;
        Vector2 randomCoord = coordBorderCamera;
        if (isXAxis)
        {
            randomCoord = new Vector2(Random.Range(-coordBorderCamera.x, coordBorderCamera.x), randomCoord.y * Mathf.Sign(Random.Range(-1f, 1f)));
        }
        else
        {
            randomCoord = new Vector2(randomCoord.x * Mathf.Sign(Random.Range(-1f, 1f)), Random.Range(-coordBorderCamera.y, coordBorderCamera.y));
        }
        return randomCoord;
    }
}
