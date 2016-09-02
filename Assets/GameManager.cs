using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ruihanyang.Game
{

    public class GameManager : MonoBehaviour
    {
        static public GameManager Instance = null;

        #region 预置体

        [SerializeField]
        private GameObject tilePrefab;

        [SerializeField]
        private GameObject playerPrefab;

        #endregion

        // Player 初始出生地
        [SerializeField]
        private Transform playerSpawnPosition;

        // 当前场上最多存在的 Tile 数量
        [SerializeField]
        private int currentMaxTileCount = 10;

        // Tile 开始的位置
        private Vector3 tileStartPosition = Vector3.zero;
        // Tile 当前的位置
        private Vector3 tileActualPosition = Vector3.zero;

        // 记录游戏时间
        private float gameTime = 0f;

        // 玩家引用
        private Player player;
        // 方块引用
        private List<Tile> tiles = new List<Tile>();

        #region 回调函数

        void Awake()
        {
            // 单例模式
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Init();
        }

        void Update()
        {
            if (player != null)
            {
                // 销毁走过的 Tile
                if (player.traveledDistance > (player.traveledTileCount + 1) * 1.0f)
                {
                    // 当前玩家走过的 Tile 数量 +1
                    player.traveledTileCount++;

                    // 第一个方块消失
                    tiles[0].Disapear();
                    tiles.RemoveAt(0);
                }
            }

            // 构建新的 Tile
            if (tiles.Count < currentMaxTileCount)
            {
                BuildTile();
            }
        }

        #endregion

        #region 自定义私有函数

        void Init()
        {
            tileActualPosition = tileStartPosition;

            // 生成初始 Tile
            BuildInitTile();

            // 初始化玩家
            StartCoroutine(InitPlayer());
        }

        void BuildTile()
        {
            GameObject _temp = Instantiate(tilePrefab, tileActualPosition, Quaternion.identity) as GameObject;

            _temp.name = "Tile";

            tiles.Add(_temp.GetComponent<Tile>());

            CalNextTilePos();
        }

        void CalNextTilePos()
        {
            int _rnd = Random.Range(0, 100);

            if (_rnd < 50)
            {
                tileActualPosition += new Vector3(1f, 0f, 0f);
            }
            else
            {
                tileActualPosition += new Vector3(0f, 0f, 1f);
            }
        }

        void BuildInitTile()
        {
            for (int i = 0; i < (5 > currentMaxTileCount ? currentMaxTileCount : 5); i++)
            {
                GameObject _temp = Instantiate(tilePrefab, tileActualPosition, Quaternion.identity) as GameObject;

                _temp.name = "Tile";

                tiles.Add(_temp.GetComponent<Tile>());

                tileActualPosition += new Vector3(1f, 0f, 0f);
            }
        }

        IEnumerator InitPlayer()
        {
            yield return new WaitForSeconds(0.8f);

            GameObject _temp = Instantiate(playerPrefab, playerSpawnPosition.position, Quaternion.identity) as GameObject;

            _temp.name = "Player";

            player = _temp.GetComponent<Player>();

            player.Init(tiles[1].transform.position);
        }

        #endregion
    }
}
