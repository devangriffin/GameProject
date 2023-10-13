﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystemExample
{
    public class Firework : ParticleSystem
    {
        Color[] colors = new Color[]
        {
            Color.Gold,
            Color.Red,
            Color.Crimson,
            Color.Orange,
            Color.OrangeRed,
            Color.Yellow,
            Color.YellowGreen,
            Color.SandyBrown
        };

        Color color;

        public Firework(Game game, int maxExplosions) : base(game, maxExplosions * 25) { }

        protected override void InitializeConstants()
        {
            textureFilename = "circle";

            minNumParticles = 30;
            maxNumParticles = 40;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);
            var lifetime = RandomHelper.NextFloat(0.5f, 1f);
            var acceleration = -velocity / lifetime;
            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);
            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);
            var scale = RandomHelper.NextFloat(4, 6);

            p.Initialize(where, velocity, acceleration, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;
            particle.Scale = .1f + .25f * normalizedLifetime;
        }

        public void PlaceFirework(Vector2 where)
        {
            color = colors[RandomHelper.Next(colors.Length)];
            AddParticles(where);
        }
    }
}