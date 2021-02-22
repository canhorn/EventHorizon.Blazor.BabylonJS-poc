namespace EventHorizon.Game.Client.Systems.Scripting.TESTING_SCRIPTS
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Scripting.Data;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;
    using Microsoft.Extensions.Logging;

    public class CreateTreeScript
    {
        public CreateTreeScript()
        {

        }

        public Task Run(ScriptData Data)
        {
            /**
 * Description: This script will create a Tree Mesh.
 *
 * "Services" Provided by Script system to help with external to script access.
 * $services: {
 *   i18n: I18nService;
 *   logger: ILogger;
 *   eventService: IEventService;
 *   commandService: ICommandService;
 *   babylonjs: BABYLONJS;
 * };
 *
 * $utils: {
 *  isObjectDefined(obj: any): bool;
 *  createEvent(event: string, data?: any): IEvent;
 * }
 *
 * This the internal "state" of the script, only accessible by the script.
 * $state: {
 * };
 *
 * This is data passed to the script from the outside.
 * $data: {
 *  id: string;
 *  scene: Scene;
 *  branchSize: number;
 *  trunkSize: number;
 *  radius: number;
 * };
 */
            try
            {
                var id = Data.Get<string>("id");
                var scene = Data.Get<Scene>("scene");
                var config = Data.Get<ClientAssetScriptConfig>("config");
                var resolve = Data.Get<Action<Mesh>>("resolve");
                //var branchSize = Data.Get<int>("branchSize");
                //var trunkSize = Data.Get<int>("trunkSize");
                //var radius = Data.Get<int>("radius");

                // TODO: pull from $state or Asset store
                var leafMaterial = new StandardMaterial(
                    $"leafMaterial-${id}",
                    scene
                );
                leafMaterial.diffuseColor = new Color3(
                    0.5m,
                    1,
                    0.5m
                );

                // TODO: pull from $state or Asset store
                var woodMaterial = new StandardMaterial(
                    $"woodMaterial-${id}",
                    scene
                );
                woodMaterial.diffuseColor = new Color3(
                    0.4m,
                    0.2m,
                    0.0m
                );
                //woodMaterial.specularColor = new BABYLON.Color3(0.4, 0.2, 0.0);
                //woodMaterial.emissiveColor = new BABYLON.Color3(0.4, 0.2, 0.0);
                //woodMaterial.ambientColor = new BABYLON.Color3(0.4, 0.2, 0.0);

                if (resolve == null)
                {
                    throw new GameException(
                        "invalid_create_script",
                        "Resolve is invalid"
                    );
                }
                resolve(
                    CreateQuickTreeGenerator(
                        id,
                        scene,
                        config.GetInt("branchSize"),
                        config.GetInt("trunkSize"),
                        config.GetInt("radius"),
                        leafMaterial,
                        woodMaterial
                    )
                );
            }
            catch (Exception ex)
            {
                GameServiceProvider.GetService<ILoggerFactory>().CreateLogger("testin")
                    .LogError(ex, "Error {Message}", ex.Message);
                throw;
            }
            return Task.CompletedTask;
        }
        public Mesh CreateQuickTreeGenerator(
            string id,
            Scene scene,
            int sizeBranch,
            int sizeTrunk,
            int radius,
            StandardMaterial leafMaterial,
            StandardMaterial woodMaterial
        )
        {


            var leaves = new Mesh($"leaves-${id}", scene);

            var vertexData = VertexData.CreateSphere(
                new
                {
                    segments = 2,
                    diameter = sizeBranch,
                }
            );

            vertexData.applyToMesh(leaves, true);

            var positions = leaves.getVerticesData(
                VertexBuffer.PositionKind
            );
            var indices = leaves.getIndices().Select(a => (int)a).ToArray();
            var numberOfPoints = positions.Length / 3;

            var map = new List<PointVertices>();

            for (var i = 0; i < numberOfPoints; i++)
            {
                var point = new Vector3(
                    positions[i * 3],
                    positions[i * 3 + 1],
                    positions[i * 3 + 2]
                );

                var found = false;
                foreach (var array in map)
                {
                    if (found)
                    {
                        break;
                    }
                    var p0 = array.Point;
                    if (p0 != null && (p0.equals(point) || p0.subtract(point).lengthSquared() < 0.01m))
                    {
                        array.Vertices.Add(i * 3);
                        found = true;
                    }
                }
                if (!found)
                {
                    var array = new PointVertices
                    {
                        Point = point
                    };
                    array.Vertices.Add(i * 3);
                    map.Add(array);
                }
            }

            foreach (var array in map)
            {
                var index = 0;
                var min = -sizeBranch / 10.0;
                var maxRandom = sizeBranch / 10.0;
                var rx = RandomNumber(min, maxRandom);
                var ry = RandomNumber(min, maxRandom);
                var rz = RandomNumber(min, maxRandom);

                for (index = 0; index < array.Vertices.Count; index++)
                {
                    var i = array.Vertices[index];
                    positions[i] = positions[i] + rx;
                    positions[i + 1] = positions[i + 1] + ry;
                    positions[i + 2] = positions[i + 2] + rz;
                }
            };

            leaves.setVerticesData(VertexBuffer.PositionKind, positions);
            var normals = ComputeNormals(positions, indices);
            leaves.setVerticesData(VertexBuffer.NormalKind, normals);
            leaves.convertToFlatShadedMesh();

            leaves.material = leafMaterial;
            leaves.position.y = sizeTrunk + sizeBranch / 2 - 2;

            var trunk = Mesh.CreateCylinder(
                "trunk",
                sizeTrunk,
                radius - 2 < 1 ? 1 : radius - 2,
                radius,
                10,
                2,
                scene
            );

            trunk.position.y = sizeBranch / 2 + 2 - sizeTrunk / 2;

            trunk.material = woodMaterial;
            trunk.convertToFlatShadedMesh();


            var tree = new Mesh($"tree-{id}", scene);
            tree.addChild(leaves);
            tree.addChild(trunk);

            tree.setEnabled(false);

            return tree;
        }

        private readonly static Random random = new System.Random();
        public decimal RandomNumber(double min, double max)
        {
            if (min == max)
            {
                return (decimal)min;
            }
            return (decimal)(random.NextDouble() * (max - min) + min);
        }

        public class PointVertices
        {
            [property: MaybeNull]
            public Vector3 Point { get; set; }
            public IList<int> Vertices { get; } = new List<int>();
        }


        /**
         * Compute normals for given positions and indices
         * @param positions an array of vertex positions, [...., x, y, z, ......]
         * @param indices an array of indices in groups of three for each triangular facet, [...., i, j, k, ......]
         * @param normals an array of vertex normals, [...., x, y, z, ......]
         * @param options an object used to set the following optional parameters for the TorusKnot, optional
          * * facetNormals : optional array of facet normals (vector3)
          * * facetPositions : optional array of facet positions (vector3)
          * * facetPartitioning : optional partitioning array. facetPositions is required for facetPartitioning computation
          * * ratio : optional partitioning ratio / bounding box, required for facetPartitioning computation
          * * bInfo : optional bounding info, required for facetPartitioning computation
          * * bbSize : optional bounding box size data, required for facetPartitioning computation
          * * subDiv : optional partitioning data about subdivsions on  each axis (int), required for facetPartitioning computation
          * * useRightHandedSystem: optional boolean to for right handed system computation
          * * depthSort : optional boolean to enable the facet depth sort computation
          * * distanceTo : optional Vector3 to compute the facet depth from this location
          * * depthSortedFacets : optional array of depthSortedFacets to store the facet distances from the reference location
         */
        public static decimal[] ComputeNormals(
            decimal[] positions,
            int[] indices
        //options?: {
        //facetNormals ?: any, facetPositions ?: any, facetPartitioning ?: any, ratio ?: number, bInfo ?: any, bbSize ?: Vector3, subDiv ?: any,
        //useRightHandedSystem ?: boolean, depthSort ?: boolean, distanceTo ?: Vector3, depthSortedFacets ?: any
        //}
        )
        {

            var normals = new List<double>(positions.Length);
            // temporary scalar variables
            var index = 0;                      // facet index
            var p1p2x = 0.0d;                    // p1p2 vector x coordinate
            var p1p2y = 0.0d;                    // p1p2 vector y coordinate
            var p1p2z = 0.0d;                    // p1p2 vector z coordinate
            var p3p2x = 0.0d;                    // p3p2 vector x coordinate
            var p3p2y = 0.0d;                    // p3p2 vector y coordinate
            var p3p2z = 0.0d;                    // p3p2 vector z coordinate
            var faceNormalx = 0.0;              // facet normal x coordinate
            var faceNormaly = 0.0;              // facet normal y coordinate
            var faceNormalz = 0.0;              // facet normal z coordinate
            var length = 0.0;                   // facet normal length before normalization
            var v1x = 0;                        // vector1 x index in the positions array
            var v1y = 0;                        // vector1 y index in the positions array
            var v1z = 0;                        // vector1 z index in the positions array
            var v2x = 0;                        // vector2 x index in the positions array
            var v2y = 0;                        // vector2 y index in the positions array
            var v2z = 0;                        // vector2 z index in the positions array
            var v3x = 0;                        // vector3 x index in the positions array
            var v3y = 0;                        // vector3 y index in the positions array
            var v3z = 0;                        // vector3 z index in the positions array
            var faceNormalSign = 1;

            // reset the normals
            for (index = 0; index < positions.Length; index++)
            {
                normals.Add(0.0d);
            }

            // Loop : 1 indice triplet = 1 facet
            var nbFaces = (indices.Length / 3) | 0;
            for (index = 0; index < nbFaces; index++)
            {

                // get the indexes of the coordinates of each vertex of the facet
                v1x = indices[index * 3] * 3;
                v1y = v1x + 1;
                v1z = v1x + 2;
                v2x = indices[index * 3 + 1] * 3;
                v2y = v2x + 1;
                v2z = v2x + 2;
                v3x = indices[index * 3 + 2] * 3;
                v3y = v3x + 1;
                v3z = v3x + 2;

                p1p2x = (double)(positions[v1x] - positions[v2x]);          // compute two vectors per facet : p1p2 and p3p2
                p1p2y = (double)(positions[v1y] - positions[v2y]);
                p1p2z = (double)(positions[v1z] - positions[v2z]);

                p3p2x = (double)(positions[v3x] - positions[v2x]);
                p3p2y = (double)(positions[v3y] - positions[v2y]);
                p3p2z = (double)(positions[v3z] - positions[v2z]);

                // compute the face normal with the cross product
                faceNormalx = faceNormalSign * (p1p2y * p3p2z - p1p2z * p3p2y);
                faceNormaly = faceNormalSign * (p1p2z * p3p2x - p1p2x * p3p2z);
                faceNormalz = faceNormalSign * (p1p2x * p3p2y - p1p2y * p3p2x);
                // normalize this normal and store it in the array facetData
                length = System.Math.Sqrt(faceNormalx * faceNormalx + faceNormaly * faceNormaly + faceNormalz * faceNormalz);
                length = (length == 0) ? 1.0 : length;
                faceNormalx /= length;
                faceNormaly /= length;
                faceNormalz /= length;

                // compute the normals anyway
                normals[v1x] += faceNormalx;                         // accumulate all the normals per face
                normals[v1y] += faceNormaly;
                normals[v1z] += faceNormalz;
                normals[v2x] += faceNormalx;
                normals[v2y] += faceNormaly;
                normals[v2z] += faceNormalz;
                normals[v3x] += faceNormalx;
                normals[v3y] += faceNormaly;
                normals[v3z] += faceNormalz;
            }
            // last normalization of each normal
            for (index = 0; index < normals.Count / 3; index++)
            {
                faceNormalx = normals[index * 3];
                faceNormaly = normals[index * 3 + 1];
                faceNormalz = normals[index * 3 + 2];

                length = System.Math.Sqrt(faceNormalx * faceNormalx + faceNormaly * faceNormaly + faceNormalz * faceNormalz);
                length = (length == 0) ? 1.0 : length;
                faceNormalx /= length;
                faceNormaly /= length;
                faceNormalz /= length;

                normals[index * 3] = faceNormalx;
                normals[index * 3 + 1] = faceNormaly;
                normals[index * 3 + 2] = faceNormalz;
            }
            return normals.Select(a => (decimal)a).ToArray();
        }
    }
}
