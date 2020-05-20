using DN.LevelSelect;
using DN.Service;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Puzzle
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class SetAnimalImage : MonoBehaviour
	{
		[SerializeField]private GameObject[] animals;

		private void Start()
		{
			print("man");
			animals = new GameObject[transform.childCount];
			for (int i = 0; i < transform.childCount; i++)
			{
				animals[i] = transform.GetChild(i).gameObject;
				if(animals[i].name == ServiceLocator.Locate<LevelMemoryService>().SelectedAnimal.ToString())
				{
					print(animals[i].name);
					animals[i].SetActive(true);
				}
			}
		}
	}
}
