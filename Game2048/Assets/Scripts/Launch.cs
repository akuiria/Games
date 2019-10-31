using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    void Awake()
    {
        MVC.RegisterModel(new GameModel());

        MVC.RegisterView(GameObject.FindObjectOfType<GameView>());

        MVC.RegisterView(GameObject.FindObjectOfType<GridCollectionView>());

        MVC.RegisterView(GameObject.FindObjectOfType<ScoreView>());

        MVC.RegisterController(EventNameCollection.NewGame, typeof(GameController));

        MVC.RegisterController(EventNameCollection.MoveTileModel, typeof(GameController));

        MVC.RegisterController(EventNameCollection.AddTileModel, typeof(GameController));
    }
}
