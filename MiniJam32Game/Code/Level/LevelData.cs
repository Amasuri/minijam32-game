using BPO.Minijam32.GraphicsBase;
using BPO.Minijam32.Level.Enemies;
using BPO.Minijam32.Level.Tile;
using BPO.Minijam32.Music;
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
        /// <summary>
        /// Bomb location and time left.
        /// </summary>
        public Dictionary<Point, float> plantedBombs { get; private set; }
        public TileData[,] tileGrid { get; private set; }
        public Point currentPlayerDefaultLocation { get; private set; }
        public List<EnemyAI> enemies { get; private set; }
        public Point TeleportPoint { get; private set; }
        public bool RedPlatePressed { get; private set; }
        public bool YellowPlatePressed { get; private set; }
        public bool BluePlatePressed { get; private set; }

        private List<Point> healDrops;

        private const float bombFuseTimerInMs = 3000f;
        private const int maxBombCount = 4;

        private readonly int maxLevelId;
        private const int enemyHealthDropChance = 33;
        private int currentLevelId;

        public LevelData(Minijam32 game)
        {
            maxLevelId = Convert.ToInt32(File.ReadAllLines("Code/Level/Layouts/last_level_id.txt")[0]);
            currentLevelId = 1;
            this.ReInitializeLevelData(level: currentLevelId);
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
                int bombFuseCh = (int)(this.plantedBombs[location]);

                if (bombFuseCh > 1000)
                {
                    if ((bombFuseCh / 100) % 5 == 0)
                        TileDrawer.DrawTileAt(batch, TileData.Type.BombOne, location);
                    else
                        TileDrawer.DrawTileAt(batch, TileData.Type.BombTwo, location);
                }
                else
                {
                    if ((bombFuseCh / 100) % 2 == 0)
                        TileDrawer.DrawTileAt(batch, TileData.Type.BombOne, location);
                    else
                        TileDrawer.DrawTileAt(batch, TileData.Type.BombTwo, location);
                }
            }

            //Enemies
            foreach (var enemy in this.enemies)
            {
                EnemyDrawer.DrawThisTypeAt(batch, enemy);
            }

            //Heal points
            foreach (var healPoint in this.healDrops)
            {
                TileDrawer.DrawTileAt(batch, TileData.Type.ButtonRed, healPoint);
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
            //If player at point, then advance
            if(PlayerDataManager.tilePosition == this.TeleportPoint)
            {
                if (currentLevelId >= maxLevelId)
                    return;
                currentLevelId++;
                this.ReInitializeLevelData(currentLevelId);
            }

            //Update logic for colored plates
            RedPlatePressed = false;
            YellowPlatePressed = false;
            BluePlatePressed = false;
            for (int x = 0; x < tileGrid.GetLength(0); x++)
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    if (tileGrid[x, y].type == TileData.Type.ButtonRed && new Point(x, y) == PlayerDataManager.tilePosition)
                    {
                        RedPlatePressed = true;
                        tileGrid[x, y].PressPlate();
                    }
                    if (tileGrid[x, y].type == TileData.Type.ButtonBlue && new Point(x, y) == PlayerDataManager.tilePosition)
                    {
                        BluePlatePressed = true;
                        tileGrid[x, y].PressPlate();
                    }
                    if (tileGrid[x, y].type == TileData.Type.ButtonYellow && new Point(x, y) == PlayerDataManager.tilePosition)
                    {
                        YellowPlatePressed = true;
                        tileGrid[x, y].PressPlate();
                    }
                }

            for (int x = 0; x < tileGrid.GetLength(0); x++)
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    if (RedPlatePressed && tileGrid[x, y].type == TileData.Type.ColorWallRed)
                    {
                        tileGrid[x, y-1].RemoveColoredWallTopping();
                        tileGrid[x, y].FlattenColoredWall();
                    }
                    if (BluePlatePressed && tileGrid[x, y].type == TileData.Type.ColorWallBlue)
                    {
                        tileGrid[x, y - 1].RemoveColoredWallTopping();
                        tileGrid[x, y].FlattenColoredWall();
                    }
                    if (YellowPlatePressed && tileGrid[x, y].type == TileData.Type.ColorWallYellow)
                    {
                        tileGrid[x, y - 1].RemoveColoredWallTopping();
                        tileGrid[x, y].FlattenColoredWall();
                    }
                }

            //Bombs go boom
            var bombsDeleteLocations = new List<Point> { };
            var iterCollection = new List<Point>( this.plantedBombs.Keys );
            foreach (var location in iterCollection)
            {
                this.plantedBombs[location] -= Minijam32.DeltaUpdate;
                if (this.plantedBombs[location] <= 0)
                {
                    bombsDeleteLocations.Add(location);

                    Animator.NewBombAnimation(location - new Point(1, 1));
                    SoundPlayer.PlaySound(SoundPlayer.Type.BombExplosion);

                    //TODO: boooooom tiles
                    this.tileGrid[location.X, location.Y].Destroy();
                    this.tileGrid[location.X-1, location.Y].Destroy();
                    this.tileGrid[location.X+1, location.Y].Destroy();
                    this.tileGrid[location.X, location.Y-1].Destroy();
                    this.tileGrid[location.X, location.Y+1].Destroy();
                }
            }

            //Enemy go places
            var enemyDeadList = new List<EnemyAI> { };
            foreach (var enemy in enemies)
            {
                //Update AI
                enemy.Update(game);

                //Check if enemy is at any exploding bombs
                foreach (var location in bombsDeleteLocations)
                {
                    if ((enemy.currentPos - location).ToVector2().Length() <= 1)
                        enemy.Damage();
                }

                //If enemy is dead, mark it
                if (enemy.isDead)
                    enemyDeadList.Add(enemy);

                //Check if enemy is touching the hero
                if (enemy.currentPos == PlayerDataManager.tilePosition)
                    PlayerDataManager.Damage();
            }

            //Heal point heals player. Then disapperear
            List<Point> removeHealDrops = new List<Point> { };
            foreach (var healPoint in this.healDrops)
            {
                if(healPoint == PlayerDataManager.tilePosition)
                {
                    PlayerDataManager.Heal();
                    removeHealDrops.Add(healPoint);
                }
            }

            //After we've done the job, let's remove the old junk
            foreach (var location in bombsDeleteLocations)
            {
                //To save cycles, we're checking for self-harm there for each explosion
                if ((PlayerDataManager.tilePosition - location).ToVector2().Length() <= 1)
                    PlayerDataManager.Damage();

                this.plantedBombs.Remove(location);
            }
            foreach (var enemy in enemyDeadList)
            {
                //On each dying enemy, there's a slight chance of spawning heal pts
                if (Minijam32.Rand.Next(100) <= enemyHealthDropChance)
                    this.healDrops.Add(enemy.currentPos);

                this.enemies.Remove(enemy);
            }
            foreach (var point in removeHealDrops)
            {
                if (healDrops.Contains(point))
                    healDrops.Remove(point);
            }
        }

        private void FindAndSetNextTeleportPoint()
        {
            for (int x = 0; x < tileGrid.GetLength(0); x++)
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    if(tileGrid[x, y].type == TileData.Type.NewLevelHole)
                    {
                        this.TeleportPoint = new Point(x, y);
                        return;
                    }
                }

            throw new Exception("No teleport point found. Level is not beatable.");
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

            //Reset player
            PlayerDataManager.ResetBeforeNewLevel(this.currentPlayerDefaultLocation);

            //Placeholder enemy data: later be like "enum_int pos_X pos_Y" in additional level file
            this.enemies = new List<EnemyAI> {};
            var enemyFile = File.ReadAllLines(String.Format("Code/Level/Layouts/level{0}.enemydata", level));
            for (int i = 1; i < enemyFile.Length; i++) //first line for help
            {
                var nums = enemyFile[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                this.enemies.Add
                (
                    new EnemyAI
                    (
                        (EnemyAI.Type)Convert.ToInt32(nums[0]),
                        new Point(Convert.ToInt32(nums[1]), Convert.ToInt32(nums[2]))
                    )
                );
            }

            //Reset bomb data
            this.plantedBombs = new Dictionary<Point, float> { };

            //New teleport point
            this.FindAndSetNextTeleportPoint();

            //Reset heal drops
            this.healDrops = new List<Point> { };
        }

        public void TryPlantBombAt(Point tilePosition)
        {
            if (this.plantedBombs.Count >= maxBombCount)
                return;

            if(!this.plantedBombs.ContainsKey(tilePosition))
                this.plantedBombs.Add(tilePosition, bombFuseTimerInMs);
        }

        /// <summary>
        /// Since bombs aren't tiles but entities, they need another kind of collision handling
        /// </summary>
        public bool IsBombAtThisPosition(Point location)
        {
            return this.plantedBombs.ContainsKey(location);
        }
    }
}
