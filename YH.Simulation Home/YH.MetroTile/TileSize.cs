using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.MetroTile
{

    public class TileSize_Small : TileSize
    {
        public TileSize_Small() 
            : base()
        {
            Width = 150;            
            Height  = 75;
        }

        //public TileSize tileSize;
    }

    public class TileSize_Medium : TileSize
    {
        public TileSize_Medium() 
            : base()
        {
            Width = 150;
            Height = 150;
        }

        //public TileSize tileSize;
    }

    public class TileSize_Large : TileSize
    {
        public TileSize_Large() 
            : base()
        {
            Width = 306;
            Height = 150;
        }

        //public TileSize tileSize;
    }

    public class TileSize_X_Large : TileSize
    {
        public TileSize_X_Large() 
            : base()
        {
            Width = 306;
            Height = 306;
        }
    }

    public class TileSize
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width
        {
            get;
            set;
        }

        /// <summary>
        /// 高度
        /// </summary>
        public double Height
        {
            get;
            set;
        }

        public TileSize()
        {

        }

       
    }
}
