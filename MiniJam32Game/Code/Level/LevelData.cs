﻿using BPO.Minijam32.Level.Tile;
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

        //private List<Enemy>

        public LevelData(Minijam32 game)
        {
            this.ReInitializeTileData(level: 1);
        }

        public void DrawBelow(Minijam32 game, SpriteBatch batch)
        {
            for (int x = tileGrid.GetLength(0) - 1; x >= 0; x--)
                for (int y = tileGrid.GetLength(1) - 1; y >= 0; y--)
                {
                    TileDrawer.DrawTileAt(batch, tileGrid[x, y].type, new Point(x, y));
                }
        }

        public void DrawAbove(Minijam32 game, SpriteBatch batch)
        {
            //stub for tiles and things above player
        }

        public void Update(Minijam32 game)
        {
            //Bombs go boom

            //Enemy go places
        }

        private void DestroyTileAt(Point tileCoords)
        {
            tileGrid[tileCoords.X, tileCoords.Y].Destroy();
        }

        private void ReInitializeTileData(int level)
        {
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
        }
    }
}
