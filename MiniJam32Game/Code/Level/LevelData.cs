using BPO.Minijam32.Level.Tile;
using BPO.Minijam32.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.Level
{
    public class LevelData
    {
        public TileData[,] tileGrid { get; private set; }
        public Point currentPlayerDefaultLocation { get; private set; }
        public List<Enemy> enemies { get; private set; }

        private const float bombFuseTimerInMs = 3000f;

        /// <summary>
        /// Bomb location and time left.
        /// </summary>
        public Dictionary<Point, float> plantedBombs;

        //private List<Enemy>

        public LevelData(Minijam32 game)
        {
            this.ReInitializeLevelData(level: 1);
        }

        public void DrawBelow(Minijam32 game, SpriteBatch batch)
        {
            //Tiles
            for (int x = tileGrid.GetLength(0) - 1; x >= 0; x--)
                for (int y = tileGrid.GetLength(1) - 1; y >= 0; y--)
                {
                    TileDrawer.DrawTileAt(batch, tileGrid[x, y].type, new Point(x, y));
                }

            //Bombs
            foreach (var location in this.plantedBombs.Keys)
            {
                TileDrawer.DrawTileAt(batch, TileData.Type.FloorWaterStillSimple, location);
            }

            //Enemies
            foreach (var enemy in this.enemies)
            {
                enemy.DrawAt(batch);
            }
        }

        public void DrawAbove(Minijam32 game, SpriteBatch batch)
        {
            for (int x = tileGrid.GetLength(0) - 1; x >= 0; x--)
                for (int y = tileGrid.GetLength(1) - 1; y >= 0; y--)
                {
                    TileDrawer.DrawTileRoofingAt(batch, tileGrid[x, y].type, new Point(x, y));
                }
        }

        public void Update(Minijam32 game)
        {
            //Bombs go boom
            var bombsDeleteLocations = new List<Point> { };
            var iterCollection = new List<Point>( this.plantedBombs.Keys );
            foreach (var location in iterCollection)
            {
                this.plantedBombs[location] -= Minijam32.DeltaUpdate;
                if (this.plantedBombs[location] <= 0)
                {
                    bombsDeleteLocations.Add(location);

                    //TODO: boooooom tiles
                }
            }

            //Enemy go places
            var enemyDeadList = new List<Enemy> { };
            foreach (var enemy in enemies)
            {
                //Update AI
                enemy.Update(game);

                //Check if enemy is at any exploding bombs
                foreach (var location in bombsDeleteLocations)
                {
                    if ((enemy.currentPos - location).ToVector2().Length() < 2)
                        enemy.Damage();
                }

                //If enemy is dead, mark it
                if (enemy.isDead)
                    enemyDeadList.Add(enemy);

                //Check if enemy is touching the hero
                if (enemy.currentPos == PlayerDataManager.tilePosition)
                    PlayerDataManager.Die();
            }

            //After we've done the job, let's remove the old shit
            foreach (var location in bombsDeleteLocations)
            {
                this.plantedBombs.Remove(location);
            }
            foreach (var enemy in enemyDeadList)
            {
                this.enemies.Remove(enemy);
            }
        }

        private void DestroyTileAt(Point tileCoords)
        {
            tileGrid[tileCoords.X, tileCoords.Y].Destroy();
        }

        private void ReInitializeLevelData(int level)
        {
            //All things tiles related
            var file = File.ReadAllLines(String.Format("Code/Level/Layouts/level{0}.leveldata", level));

            int baseCount = (int)'@'; //it's like zero for all the enums, so '@' = 0 = TileData.Type.FloorDirt. For all corellations look up ascii tables starting from '@'

            tileGrid = new TileData[file[0].Length, file.Length];

            for (int x = 0; x < file[0].Length; x++)
                for (int y = 0; y < file.Length; y++)
                {
                    char symb = file[y][x];
                    int correspondingType = symb - baseCount;
                    tileGrid[x, y] = new TileData((TileData.Type) correspondingType);
                }

            //Extra data like player hp and other things
            file = File.ReadAllLines(String.Format("Code/Level/Layouts/level{0}.extradata", level));
            var plLocData = file[0].Replace("hero pos: ", "").Split( new string[]{ " " }, StringSplitOptions.RemoveEmptyEntries);
            this.currentPlayerDefaultLocation = new Point(Convert.ToInt32( plLocData[0] ), Convert.ToInt32 ( plLocData[1] ));

            //Placeholder enemy data: later be like "enum_int pos_X pos_Y" in additional level file
            this.enemies = new List<Enemy>
            {
                new Enemy(Enemy.Type.SomeMook, new Point(3, 3)),
                new Enemy(Enemy.Type.SomeOtherMook, new Point(5, 10)),
            };

            //Reset bomb data
            this.plantedBombs = new Dictionary<Point, float> { };
        }

        public void TryPlantBombAt(Point tilePosition)
        {
            if(!this.plantedBombs.ContainsKey(tilePosition))
                this.plantedBombs.Add(tilePosition, bombFuseTimerInMs);
        }
    }
}
