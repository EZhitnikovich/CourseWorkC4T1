﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c4t1.Model;

namespace c4t1
{
    public class PathNode
    {
        // Координаты точки на карте.
        public Point Position { get; set; }
        // Длина пути от старта (G).
        public int PathLengthFromStart { get; set; }
        // Точка, из которой пришли в эту точку.
        public PathNode CameFrom { get; set; }
        // Примерное расстояние до цели (H).
        public int HeuristicEstimatePathLength { get; set; }
        // Ожидаемое полное расстояние до цели (F).
        public int EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
            }
        }
    }

    internal class PathFinder
    {
        //public static List<Point> FindPath(Cell[,] field, Point start, Point goal)
        //{
        //    return new List<Point>();
        //}

        public static List<Point> FindPath(Cell[,] field, Point start, Point goal)
        {
            // Шаг 1.
            var closedSet = new Collection<PathNode>();
            var openSet = new Collection<PathNode>();

            // Шаг 2.
            PathNode startNode = new PathNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = field[start.X, start.Y].Weight,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal, field)
            };

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // Шаг 3.
                var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();
                // Шаг 4.
                if (currentNode.Position == goal)
                    return GetPathForNode(currentNode);
                // Шаг 5.
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                // Шаг 6.
                foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
                {
                    // Шаг 7.
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                      node.Position == neighbourNode.Position);
                    // Шаг 8.
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        // Шаг 9.
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            // Шаг 10.
            return new List<Point>();
        }
        private static int GetHeuristicPathLength(Point from, Point to, Cell[,] field)
        {
            return Math.Abs(field[from.X, from.Y].Weight - field[to.X, to.Y].Weight);
        }

        private static Collection<PathNode> GetNeighbours(PathNode pathNode, Point goal, Cell[,] field)
        {
            var result = new Collection<PathNode>();

            // Соседними точками являются соседние по стороне клетки.
            Point[] neighbourPoints = new Point[4];
            neighbourPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
            neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
            neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
            neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);

            foreach (var point in neighbourPoints)
            {
                // Проверяем, что не вышли за границы карты.
                if (point.X < 0 || point.X >= field.GetLength(0))
                    continue;
                if (point.Y < 0 || point.Y >= field.GetLength(1))
                    continue;
                // Проверяем, что по клетке можно ходить.
                //if ((field[point.X, point.Y].Weight != 0) && (field[point.X, point.Y].Weight != 1))
                //    continue;
                // Заполняем данные для точки маршрута.
                var neighbourNode = new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart +
                    GetDistanceBetweenNeighbours(field, point),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal, field)
                };
                result.Add(neighbourNode);
            }
            return result;
        }

        private static List<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }

        private static int GetDistanceBetweenNeighbours(Cell[,] field, Point point)
        {
            return field[point.X, point.Y].Weight;
        }
    }
}