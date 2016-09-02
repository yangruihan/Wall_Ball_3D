using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Ruihanyang.Game
{

    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            Ready,
            Run,
            GameOver
        }

        static public GameManager Instance = null;

        #region 预置体

        [SerializeField]
        private GameObject tilePrefab;

        [SerializeField]
        private GameObject playerPrefab;

        #endregion

        #region UI组件

        [SerializeField]
        private Text timeText;
        [SerializeField]
        private Text scoreText;

        #endregion

        public GameState gameState = GameState.Ready;

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
                // 当前 Tile 1s 后消失
                if (player.transform.position.x > tiles[0].transform.position.x + 0.5f
                    || player.transform.position.z > tiles[0].transform.position.z + 0.5f)
                {
                    // 当前玩家走过的 Tile 数量 +1
                    player.traveledTileCount++;

                    // 得分
                    player.AddScore(1);

                    // 第一个方块消失
                    StartCoroutine(WaitSecondsForTileDisapear(tiles[0]));

                    tiles.RemoveAt(0);
                }
            }

            // 构建新的 Tile
            if (tiles.Count < currentMaxTileCount)
            {
                BuildTile();
            }

            UpdateUI();

            if (IsGameOver())
            {
                Destroy(player.gameObject);

                gameState = GameState.GameOver;
            }

            if (gameState == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
            {
                Init();
            }
        }

        void FixedUpdate()
        {
            gameTime += Time.fixedDeltaTime;
        }

        #endregion

        #region 自定义私有函数

        void Init()
        {
            tileActualPosition = tileStartPosition;

            // 清理已存在 Tile
            ClearTiles();

            // 生成初始 Tile
            BuildInitTile();

            // 初始化玩家
            StartCoroutine(InitPlayer());

            gameState = GameState.Run;
        }

        void ClearTiles()
        {
            tiles.Clear();

            GameObject[] _tileObjs = GameObject.FindGameObjectsWithTag("Tile");

            foreach (var _tile in _tileObjs)
            {
                Destroy(_tile);
            }
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

            if (player != null && player.traveledTime > 20f)
            {
                _rnd = Random.Range(0, 100);

                if (_rnd < 40)
                {
                    //if (_rnd < 20)
                    //{
                        tileActualPosition += new Vector3(0f, 1f, 0f);
                    //}
                    //else
                    //{
                    //    tileActualPosition += new Vector3(0f, -1f, 0f);
                    //}
                }
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

            if (player == null)
            {
                GameObject _temp = Instantiate(playerPrefab, playerSpawnPosition.position, Quaternion.identity) as GameObject;

                _temp.name = "Player";

                player = _temp.GetComponent<Player>();

                player.Init(tiles[1].transform.position);
            }
            else
            {
                player.transform.position = playerSpawnPosition.position;

                player.Init(tiles[1].transform.position);
            }
        }

        void UpdateUI()
        {
            if (player == null) return;

            if (timeText != null)
            {
                timeText.text = string.Format("{0:F2}s", player.traveledTime);
            }

            if (scoreText != null)
            {
                scoreText.text = player.score + "";
            }
        }

        IEnumerator WaitSecondsForTileDisapear(Tile _tile)
        {
            yield return new WaitForSeconds(0.3f);

            _tile.Disapear();
        }

        bool IsGameOver()
        {
            if (player == null) return false;

            if (player.transform.position.y <= tiles[0].transform.position.y - 20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
