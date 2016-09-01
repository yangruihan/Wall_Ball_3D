using UnityEngine;
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

        private Player player;
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

        #endregion

        #region 自定义私有函数

        void Init()
        {
            tileActualPosition = tileStartPosition;

            // 生成初始 Tile
            BuildInitTile();

            // 初始化玩家
            InitPlayer();
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

        void InitPlayer()
        {
            GameObject _temp = Instantiate(playerPrefab, playerSpawnPosition.position, Quaternion.identity) as GameObject;

            _temp.name = "Player";

            player = _temp.GetComponent<Player>();

            player.Init(tiles[1].transform.position);
        }

        #endregion
    }
}
