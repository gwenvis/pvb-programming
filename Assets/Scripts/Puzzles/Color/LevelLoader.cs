using System;
using System.Collections.Generic;
using System.Linq;
using DN.LevelSelect;
using DN.Service;
using UnityEngine;

namespace DN.Puzzle.Color
{
    /// <summary>
    /// Responsible for loading the level and passing the information to the player.
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private RectTransform levelView;
        [SerializeField] private Transform nodeQueueSpawn;
        [SerializeField] private Player player;

        [Header("Prefabs")] 
        [SerializeField] private Node nodePrefab;
        [SerializeField] private Line linePrefab;
        [SerializeField] private GameObject blueQueuePrefab;
        [SerializeField] private GameObject pinkQueuePrefab;
        [SerializeField] private GameObject yellowQueuePrefab;

        private Dictionary<NodeData, Node> spawnedNodes;
        private Dictionary<LineData, Line> spawnedLines;
        private List<GameObject> spawnedQueueueObjects;

        private ColorLevelData currentLevelData;

        protected void Awake()
        {
            var levelData = ServiceLocator.Locate<LevelMemoryService>().LevelData as ColorLevelData;
            
            if(levelData == null)
                throw new Exception("Wrong level data type...");
            
            LoadLevel(levelData);
        }

        protected void OnDestroy()
        {
            Clean();
        }

        public void Clean()
        {
            foreach (var node in spawnedNodes)
            {
                node.Key.Clean();
                Destroy(node.Value);
            }

            foreach (var line in spawnedLines)
            {
                line.Key.Clean();
                Destroy(line.Value);
            }

            spawnedQueueueObjects.ForEach(Destroy);
            spawnedQueueueObjects.Clear();

            spawnedNodes.Clear();
            spawnedLines.Clear();
        }

        public void Reset()
        {
            Clean();
            LoadLevel(currentLevelData);
        }

        public void LoadWithExistingData() => LoadLevel(currentLevelData);
        
        public void LoadLevel(ColorLevelData colorLevelData)
        {
            currentLevelData = colorLevelData;
            Vector3 ratio = GetSizeRatio(levelView.sizeDelta, colorLevelData.SavedSize);

            spawnedNodes = new Dictionary<NodeData, Node>();
            spawnedLines = new Dictionary<LineData, Line>();
            spawnedQueueueObjects = new List<GameObject>();
            
            foreach (NodeData node in colorLevelData.Nodes)
            {
                InstantiateNode(node, ratio);
            }

            foreach (LineData line in colorLevelData.Lines)
            {
                InstantiateLine(line, colorLevelData.Nodes);
            }

            foreach (LineColor queueItem in colorLevelData.DraggableNodes)
            {
                GameObject queueItemPrefab;
                switch (queueItem)
                {
                    case LineColor.Pink:
                        queueItemPrefab = pinkQueuePrefab;
                        break;
                    case LineColor.Yellow:
                        queueItemPrefab = yellowQueuePrefab;
                        break;
                    case LineColor.Blue:
                        queueItemPrefab = blueQueuePrefab;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                spawnedQueueueObjects.Add(Instantiate(queueItemPrefab, nodeQueueSpawn));
            }
            
            player.SetStartingNode(colorLevelData.Nodes.First(x=>x.IsStart).Owner, true);
        }

        private void InstantiateNode(NodeData data, Vector3 ratio)
        {
            Node node = Instantiate(nodePrefab, levelView);
            node.transform.localPosition = new Vector3(data.Position.x * ratio.x, data.Position.y * ratio.y,
                data.Position.z * ratio.z);

            node.InitializeNode(data);
            spawnedNodes.Add(data, node);
        }

        private void InstantiateLine(LineData data, IEnumerable<NodeData> nodes)
        {
            Line line = Instantiate(linePrefab, levelView);

            line.InitializeData(data);
            line.Data.SetOwner(line, nodes);
            line.AssignToParent();

            spawnedLines.Add(data, line);
        }

        private Vector3 GetSizeRatio(Vector3 actualSize, Vector3 savedSize) => new Vector3(actualSize.x / savedSize.x,
            actualSize.y / savedSize.y,
            actualSize.z / savedSize.y);
    }
}