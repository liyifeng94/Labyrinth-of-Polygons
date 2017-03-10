using UnityEngine;
using System.Collections;

public class TankTowerButton : MonoBehaviour {

    public static TankTowerButton Instance;
    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private TileEventHandler _tileEventHandler;
    private TowerController _towerController;
    private GameBoard _gameBoard;

    void Awake()
    {
        Instance = this;
    }

    public void setTowerEventHandler(TileEventHandler teh)
    {
        _tileEventHandler = teh;
        if (null == _gameBoard)
        {
            _buildCheckPanel = BuildCheckPanel.Instance; // used for set appear
            _towerInfoPanel = TowerInfoPanel.Instance; // used for set appear
            _towerController = TowerController.Instance; // used for check range
            _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem; // used for highlight
        }
    }

    public void TankTowerSelected()
    {
        _gameBoard.ClearHighlightTiles();
        _tileEventHandler.SetTowerIndex(0);
        _tileEventHandler.SetOperation(1);
        int[] towerInfo = new int[11];
        int range = _towerController.checkTowerInfo(0, towerInfo);
        // TODO: get display text
        //Debug.Log("Range is " + range + " " + _tileEventHandler.GridX + " " + _tileEventHandler.GridY);
        for (uint i = 0; i <= range; i++)
        {
            for (uint j = 0; j <= range; j++)
            {
                if (_tileEventHandler.GridX + i < 10 && _tileEventHandler.GridY + j < 20)
                {
                    _gameBoard.HighlightTileAt(_tileEventHandler.GridX + i, _tileEventHandler.GridY + j);
                }
                if (_tileEventHandler.GridX >= i && _tileEventHandler.GridY >= j)
                {
                    _gameBoard.HighlightTileAt(_tileEventHandler.GridX - i, _tileEventHandler.GridY - j);
                }
                if (i != 0 && j != 0)
                {
                    if (_tileEventHandler.GridX + i < 10 && _tileEventHandler.GridY >= j)
                    {
                        _gameBoard.HighlightTileAt(_tileEventHandler.GridX + i, _tileEventHandler.GridY - j);
                    }
                    if (_tileEventHandler.GridX >= i && _tileEventHandler.GridY + j < 20)
                    {
                        _gameBoard.HighlightTileAt(_tileEventHandler.GridX - i, _tileEventHandler.GridY + j);
                    }
                }
            }
        }
        _buildCheckPanel.Appear();
        _towerInfoPanel.SetTowerInfo(towerInfo);
        _towerInfoPanel.Appear();
    }
}
