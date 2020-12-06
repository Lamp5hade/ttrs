/*-
 * #%L
 * Codenjoy - it's a dojo-like platform from developers to developers.
 * %%
 * Copyright (C) 2018 Codenjoy
 * %%
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public
 * License along with this program.  If not, see
 * <http://www.gnu.org/licenses/gpl-3.0.html>.
 * #L%
 */

using System;
using System.Collections.Generic;

namespace TetrisClient
{
	/// <summary>
	/// В этом классе находится логика Вашего бота
	/// </summary>
	internal class YourSolver : AbstractSolver
	{
		public YourSolver(string server)
			: base(server)
		{
		}

		/// <summary>
		/// Этот метод вызывается каждый игровой тик
		/// </summary>
		protected internal override Command Get(Board board)
		{
			//board.GetAllAt
			// Код писать сюда!
			return FindPositions(board);

			// Команды можно комбинировать
			/*
			return Command.DOWN
				.Then(Command.SUICIDE);
			*/
		}

		private Command FindPositions(Board board)
	    {
			(int, int, int) bestPlace = (0, 0, 0);
			int minCost = int.MaxValue;
			for (int rot = 0; rot < Tetrimino.ROTATIONS; rot++)
			{
				for (int x = -2, costX = 0; x < board.Size + 2; x++, costX++)
				{
					for (int y = -2, costY = 0; y < board.Size + 2; y++, costY++)
					{
						if (!Tetrimino.IsElementCanMove(board, x, y, rot)) continue;
						var curCost = 0;
						var locField = new int[board.Size][];
						for (int xx = 0; xx < board.Size; xx++)
						{
							locField[xx] = new int[board.Size];
							for (int yy = 0; yy < board.Size; yy++)
							{
								locField[xx][yy] = Convert.ToInt32(!board.IsFree(xx, yy));
							}
						}
						for (int i1 = x - Tetrimino.AREA_SIZE / 2, i2 = 0; i1 <= x + Tetrimino.AREA_SIZE/2; i1++, i2++)
						{
							for (int j1 = y - Tetrimino.AREA_SIZE / 2, j2 = 0; j1 <= y + Tetrimino.AREA_SIZE / 2; j1++, j2++)
							{
								if (!Tetrimino.IsElementBlockValid(board.GetCurrentFigureType(), i2, j2, rot)) continue;
								locField[i1][j1] = 1;
								curCost += (/*costX + i2 + */costY - 2 + j2) 
									+ (j1 - 1 >= 0 ? (board.IsFree(i1, j1 - 1) && !Tetrimino.IsElementBlockValid(board.GetCurrentFigureType(), i2, j2 - 1, rot) ? 20 : 0) : 0)
									+ (j1 + 1 < board.Size ? (!board.IsFree(i1, j1 + 1) && !Tetrimino.IsElementBlockValid(board.GetCurrentFigureType(), i2, j2 + 1, rot) ? 1000 : 0) : 0);
							}
						}

						foreach(var line in Tetrimino.GetCompletedLines(locField))
                        {
							//Console.WriteLine(line);
							if(line >= board.Size * (2 / 3))
                            {
								curCost -= line * line;
								continue;
							}
							curCost -= line;
						}

						if (curCost >= minCost) continue;
						minCost = curCost;
						bestPlace = (x, y, rot);
					}
				}
			}
			var commandsList = new List<string>();

			Console.WriteLine(bestPlace);
            switch (bestPlace.Item3)
            {
				case 1:
					commandsList.Add(Command.ROTATE_CLOCKWISE_90.ToString());
					break;
				case 2:
					commandsList.Add(Command.ROTATE_CLOCKWISE_180.ToString());
					break;
				case 3:
					commandsList.Add(Command.ROTATE_CLOCKWISE_270.ToString());
					break;
            }
			if (board.GetCurrentFigurePoint().X < bestPlace.Item1) {
				for (int i = board.GetCurrentFigurePoint().X; i < bestPlace.Item1; i++)
                {
					commandsList.Add(Command.RIGHT.ToString());
				}
			}
			if (board.GetCurrentFigurePoint().X > bestPlace.Item1) {
				for (int i = board.GetCurrentFigurePoint().X; i > bestPlace.Item1; i--)
                {
					commandsList.Add(Command.LEFT.ToString());
				}
			}
			commandsList.Add(Command.DOWN.ToString());
			return new Command(commandsList);
		}
	}
}
