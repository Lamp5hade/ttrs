using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisClient
{
    class Tetrimino
    {
        public const int AREA_SIZE = 5;
        public const int ROTATIONS = 4;

        public static bool IsElementCanMove(Board board, int x, int y, int rotation){
            var element = board.GetCurrentFigureType();
            var elementBlocks = GetBlocksOrientation(element, rotation);
            for (int i1 = x - AREA_SIZE/2, i2 = 0; i1 <= x + AREA_SIZE / 2; i1++, i2++)
            {
                for (int j1 = y - AREA_SIZE / 2, j2 = 0; j1 <= y + AREA_SIZE / 2; j1++, j2++)
                {
                    //if (rotation == 1 && x == 1 && y == 0) Console.WriteLine($"{i1},{j1}, {IsElementBlockValid(element, i2, j2, rotation)}");
                    if(IsElementBlockValid(element, i2, j2, rotation))
                    {
                        //if (rotation == 1 && x == 1 && y == 0) Console.WriteLine($"{(i1 < 0 || i1 >= board.Size || j1 < 0 || j1 >= board.Size)}");
                        if (i1 < 0 || i1 >= board.Size || j1 < 0 || j1 >= board.Size) return false;
                        //if (rotation == 1 && x == 1 && y == 0) Console.WriteLine($"{!board.IsFree(i1, j1)}");
                        if (!board.IsFree(i1, j1)) return false;
                    }
                }
            }
            return true;
        }

        public static bool IsElementBlockValid(Element element, int x, int y, int rotation)
        {
            var elementBlocks = GetBlocksOrientation(element, rotation);
            if (x < 0 || x >= AREA_SIZE || y < 0 || y >= AREA_SIZE) return false;
            if (elementBlocks[AREA_SIZE - 1 - y, x] == 1) return true;
            return false;
        }

        public static List<int> GetCompletedLines(int[][] field)
        {
            var result = new List<int>();
            for (int y = 0; y < field.Length; y++)
            {
                bool isCompleted = true;
                for (int x = 0; x < field.Length; x++)
                {
                    if (field[x][y] != 1)
                    {
                        isCompleted = false;
                        break;
                    }
                }
                if (isCompleted)
                {
                    result.Add(y);
                }
            }
            return result;
        }

        public static int[,] GetBlocksOrientation(Element element, int rotation)
        {
            int[,] result = new int[AREA_SIZE, AREA_SIZE];
            switch (element)
            {
                case Element.YELLOW:
                    switch (rotation)
                    {
                        case 0:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 1:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 2:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 3:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                    }
                    break;
                case Element.BLUE:
                    switch (rotation)
                    {
                        case 0:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 } };
                            break;
                        case 1:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 1, 1, 1, 1, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 2:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 3:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 1, 1, 1, 1 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                    }
                    break;
                case Element.ORANGE:
                    switch (rotation)
                    {
                        case 0:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 1:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 1, 1, 1, 0 },
                                       { 0, 1, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 2:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 3:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 1, 0 },
                                       { 0, 1, 1, 1, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                    }
                    break;
                case Element.CYAN:
                    switch (rotation)
                    {
                        case 0:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 1:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 1, 0, 0, 0 },
                                       { 0, 1, 1, 1, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 2:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 3:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 1, 1, 1, 0 },
                                       { 0, 0, 0, 1, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                    }
                    break;
                case Element.GREEN:
                    switch (rotation)
                    {
                        case 0:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 1:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 0, 1, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 2:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 3:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 1, 0, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                    }
                    break;
                case Element.RED:
                    switch (rotation)
                    {
                        case 0:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 1:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 1, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 2:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 3:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 1, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                    }
                    break;
                case Element.PURPLE:
                    switch (rotation)
                    {
                        case 0:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 1, 1, 1, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 1:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 1, 1, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 2:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 0, 0, 0 },
                                       { 0, 1, 1, 1, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                        case 3:
                            result = new int[AREA_SIZE, AREA_SIZE]
                                     { { 0, 0, 0, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 1, 1, 0, 0 },
                                       { 0, 0, 1, 0, 0 },
                                       { 0, 0, 0, 0, 0 } };
                            break;
                    }
                    break;
            }
            return result;
        }
    }
}
