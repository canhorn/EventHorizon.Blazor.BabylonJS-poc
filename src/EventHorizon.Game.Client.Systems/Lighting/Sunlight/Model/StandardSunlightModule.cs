namespace EventHorizon.Game.Client.Systems.Lighting.Sunlight.Model;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Entity.Model;
using EventHorizon.Game.Client.Engine.Entity.Vector3Math;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.Lighting.Api;
using EventHorizon.Game.Client.Systems.Lighting.Sunlight.Api;

public class StandardSunlightModule : ModuleEntityBase, SunlightModule
{
    private readonly Queue<IVector3> _lightMovementVectorList = new Queue<IVector3>();
    private readonly ILightEntity _entity;
    private readonly bool _inverse;
    private readonly Action<IVector3, decimal> _onLightMoved;

    public override int Priority => 0;

    public StandardSunlightModule(
        ILightEntity entity,
        bool inverse,
        Action<IVector3, decimal> onLightMoved
    )
    {
        _entity = entity;
        _inverse = inverse;
        _onLightMoved = onLightMoved;
    }

    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override Task Update()
    {
        RunSunlightMovement();
        return Task.CompletedTask;
    }

    public override Task Dispose()
    {
        return Task.CompletedTask;
    }

    private void RunSunlightMovement()
    {
        if (_lightMovementVectorList.TryDequeue(out var lightMovementVector))
        {
            var intensity = lightMovementVector.Y / (100 * 50);
            _onLightMoved(lightMovementVector, intensity);
        }
        else
        {
            var lightVectorList = MoveSunLight();
            foreach (var lightVector in lightVectorList)
            {
                _lightMovementVectorList.Enqueue(lightVector);
            }
        }
    }

    private IEnumerable<IVector3> MoveSunLight()
    {
        var multiplier = 100;
        if (_inverse)
        {
            multiplier = -100;
        }
        return Curve3.CreateCatmullRomSpline(
            new IVector3[]
            {
                new StandardVector3(10 * multiplier, 2 * multiplier, multiplier * 10),
                new StandardVector3(5 * multiplier, 15 * multiplier, multiplier * 10),
                new StandardVector3(0 * multiplier, 30 * multiplier, multiplier * 10),
                new StandardVector3(-5 * multiplier, 15 * multiplier, multiplier * 10),
                new StandardVector3(-10 * multiplier, 2 * multiplier, multiplier * 10),

                // new Vector3(5 * multiplier, 25 * multiplier, -25 * multiplier),
                // new Vector3(0 * multiplier, 30 * multiplier, -25 * multiplier),
                // new Vector3(-5 * multiplier, 25 * multiplier, -25 * multiplier),
                // new Vector3(-10 * multiplier, 2 * multiplier, -25 * multiplier),
                // new Vector3(-10 * multiplier, 7 * multiplier, -25 * multiplier),
                // new Vector3(-5 * multiplier, 7 * multiplier, -25 * multiplier),
                // new Vector3(0 * multiplier, 7 * multiplier, -25 * multiplier),
                // new Vector3(5 * multiplier, 7 * multiplier, -25 * multiplier),
                // new Vector3(10 * multiplier, 7 * multiplier, -25 * multiplier),
            },
            720,
            true
        );
    }

    public static class Curve3
    {
        /**
         * Returns a Curve3 object along a CatmullRom Spline curve :
         * @param points (array of Vector3) the points the spline must pass through. At least, four points required
         * @param nbPoints (integer) the wanted number of points between each curve control points
         * @param closed (boolean) optional with default false, when true forms a closed loop from the points
         * @returns the created Curve3
         */
        public static IEnumerable<IVector3> CreateCatmullRomSpline(
            IVector3[] points,
            int nbPoints,
            bool closed = false
        )
        {
            var catmullRom = new List<IVector3>();
            var step = 1.0m / nbPoints;
            var amount = 0.0m;
            if (closed)
            {
                var pointsCount = points.Length;
                for (var i = 0; i < pointsCount; i++)
                {
                    amount = 0;
                    for (var c = 0; c < nbPoints; c++)
                    {
                        catmullRom.Add(
                            Vector3Math.CatmullRom(
                                points[i % pointsCount],
                                points[(i + 1) % pointsCount],
                                points[(i + 2) % pointsCount],
                                points[(i + 3) % pointsCount],
                                amount
                            )
                        );
                        amount += step;
                    }
                }
                catmullRom.Add(catmullRom[0]);
            }
            else
            {
                var totalPoints = new List<IVector3> { points[0].Clone() };
                totalPoints.AddRange(points);
                totalPoints.Add(points[^1].Clone());
                int i;
                for (i = 0; i < totalPoints.Count - 3; i++)
                {
                    amount = 0;
                    for (var c = 0; c < nbPoints; c++)
                    {
                        catmullRom.Add(
                            Vector3Math.CatmullRom(
                                totalPoints[i],
                                totalPoints[i + 1],
                                totalPoints[i + 2],
                                totalPoints[i + 3],
                                amount
                            )
                        );
                        amount += step;
                    }
                }
                i--;
                catmullRom.Add(
                    Vector3Math.CatmullRom(
                        totalPoints[i],
                        totalPoints[i + 1],
                        totalPoints[i + 2],
                        totalPoints[i + 3],
                        amount
                    )
                );
            }
            return catmullRom;
        }
    }
}
