using UnityEngine;

public class LevelCreator : MonoBehaviour
{
	public Transform generateStartingPos;
	[SerializeField] private Sprite[] textureFiles;
	[SerializeField] private GameObject player;
	private int maxBlocksNeeded = 6;

	// 0 is air
	// 1 is path
	// 2 ending
	// 3 is start
	public int[][] Level { get; } = {
		new[] { 0, 0, 0, 0, 1, 2, },
		new[] { 0, 0, 1, 1, 1, 0, },
		new[] { 0, 0, 0, 1, 0, 0, },
		new[] { 3, 1, 1, 1, 0, 0, },
		new[] { 0, 0, 0, 0, 0, 0, },
		new[] { 0, 0, 0, 0, 0, 0, },
		new[] { 0, 0, 0, 0, 0, 0, }
	};



	void Start()
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
					0.5f + y + generateStartingPos.position.y,
					0.5f - x + generateStartingPos.position.x,
					1.0f);
				if (Level[x][y] == 3)
				{
					GameObject p = Instantiate(player);
					p.GetComponent<Player>().SetPlayer(x,y, gameObject.transform.position, this);
					GetComponent<PlayLevel>().player = p;
				}
				gameObject.transform.localScale = new Vector3(100 / spriteee.rect.width, 100 / spriteee.rect.height, 1);
			}
		}
	}
}
