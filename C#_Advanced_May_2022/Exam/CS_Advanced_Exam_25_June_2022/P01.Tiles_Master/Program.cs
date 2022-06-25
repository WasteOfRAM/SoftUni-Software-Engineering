using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Tiles_Master
{
    internal class Program
    {
        static void Main()
        {
            var whiteTiles = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
            var greyTiles = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));

            var tileLocations = new Dictionary<int, Tile>
            {
                {40, new Tile("Sink", 0) },
                {50, new Tile("Oven", 0) },
                {60, new Tile("Countertop", 0) },
                {70, new Tile("Wall", 0) }
            };

            var floorTiles = new Tile("Floor", 0);

            while (whiteTiles.Count > 0 && greyTiles.Count > 0)
            {
                int whiteTile = whiteTiles.Pop();
                int greyTile = greyTiles.Dequeue();

                if (whiteTile == greyTile)
                {
                    int tilesArea = whiteTile + greyTile;

                    if (tileLocations.ContainsKey(tilesArea))
                        tileLocations[tilesArea].Count++;
                    else
                        floorTiles.Count++;

                    continue;
                }

                whiteTile /= 2;
                whiteTiles.Push(whiteTile);

                greyTiles.Enqueue(greyTile);
            }

            var tilesCount = tileLocations.Where(t => t.Value.Count > 0).ToDictionary(k => k.Value.Location, v => v.Value.Count);
            if (floorTiles.Count > 0)
                tilesCount.Add(floorTiles.Location, floorTiles.Count);

            string whiteTilesLeft = whiteTiles.Count == 0 ? "none" : string.Join(", ", whiteTiles);
            Console.WriteLine($"White tiles left: {whiteTilesLeft}");
            string greyTilesLeft = greyTiles.Count == 0 ? "none" : string.Join(", ", greyTiles);
            Console.WriteLine($"Grey tiles left: {greyTilesLeft}");

            foreach (var (location, count) in tilesCount.OrderByDescending(t => t.Value).ThenBy(t => t.Key))
            {
                Console.WriteLine($"{location}: {count}");
            }
        }
    }

    class Tile
    {
        public Tile(string location, int count)
        {
            Location = location;
            Count = count;
        }

        public string Location { get; set; }
        public int Count { get; set; }
    }
}
