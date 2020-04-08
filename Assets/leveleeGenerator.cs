using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveleeGenerator : MonoBehaviour
{
    public int minPowerNeeded = 2;
    // 0 is air
    // 1 is power
    // 2 is power activated by butone
    // 3 is button
    // 4 is ending
    // 5 is start
    // 6 is solid wall
	public int[][] Level { get; } = {
		new[] { 0, 0, 0, 0, 2, 0, },
		new[] { 0, 0, 1, 0, 0, 0, },
		new[] { 0, 0, 6, 0, 0, 0, },
		new[] { 5, 0, 6, 0, 0, 4, },
		new[] { 0, 0, 0, 0, 0, 0, },
		new[] { 0, 6, 0, 3, 0, 0, },
		new[] { 0, 6, 0, 0, 0, 0, }
	};

    public Sprite[] TextureFiles => textureFiles;

    [SerializeField] private Sprite[] textureFiles;
    [SerializeField] private Transform generateStartingPos;

    protected void Start()
    {
        for (int x = 0; x < Level.GetLength(0); x++)
        {
            for (int y = 0; y < Level[x].GetLength(0); y++)
            {
                GameObject gameObject = new GameObject($"level tile ({x} {y})");
                var sprite = gameObject.AddComponent<SpriteRenderer>();
                Sprite spriteee = textureFiles[Level[x][y]];
                sprite.sprite = spriteee;
                gameObject.transform.position = new Vector3(
                    0.5f + y + generateStartingPos.position.x,
                    0.5f + -x + generateStartingPos.position.y,
                    1.0f);
                gameObject.transform.localScale = new Vector3(100 / spriteee.rect.width, 100 / spriteee.rect.height, 1);
            }
        }
    }

    public Vector2 GetStartingPosition(out int x, out int y)
    {
        for (int i = 0; i < Level.Length; i++)
        {
            for (int j = 0; j < Level[i].Length; j++)
            {
                if(Level[i][j] == 5)
                {
                    x = i; y = j;
                    return GetPositionOnGrid(i, j);
                }
            }
        }
        x = y = 0;
        return Vector2.zero;
    }

    public Vector2 GetPositionOnGrid(int x, int y) => 
        new Vector2(0.5f + y + generateStartingPos.position.x, 0.5f + -x + generateStartingPos.position.y);
}
