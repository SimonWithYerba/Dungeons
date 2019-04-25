﻿using System;
using System.Drawing;

namespace Dungeons
{
    class Wizard : Enemy
    {
        private const int minHealth = 1;
        private const int maxDamage = 5;
        private const int hitPointsOfLifeAtTheBeginning = 20;

        public Wizard(Game game, Point location) : base(game, location, hitPointsOfLifeAtTheBeginning)
        {
                
        }

        public override void Move(Random random)
        {
            if(HitPoints >= minHealth)
            {
                if(random.Next(1,6) == 1)
                {
                    location = Move((Direction)random.Next(4), game.Boundaries);
                }
                else
                {
                    location = Move(FindPlayerDirection(game.PlayerLocation),game.Boundaries);
                }
                if (NearPlayer())
                {
                    game.HitPlayer(maxDamage, random);
                }
            }
        }
    }
}
