﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace noaPippi
{
    //TODO: 固定された表示領域と動的な表示領域のクラスを派生させたい
    //TODO: クラスの多態性の実装で、継承を使うのがフィールドで型を持つのより何が優れているのか調べる
    class VirtualViewport
    {
        public double X { get; private set; }
        public double Y { get; private set; }
    }
}