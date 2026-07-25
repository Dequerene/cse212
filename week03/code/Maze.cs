using System;
using System.Collections.Generic;

/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// x and y are integers representing locations in the maze.
/// The boolean values represent valid directions.
///
/// If a direction is false, then there is a wall in that direction.
/// If a direction is true, then movement is allowed.
///
/// If there is a wall, throw an InvalidOperationException with the
/// message "Can't go that way!".
/// </summary>
public class Maze
{
    private readonly Dictionary<
        ValueTuple<int, int>,
        bool[]> _mazeMap;

    private int _currX = 1;
    private int _currY = 1;

    public Maze(
        Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    /// <summary>
    /// Check whether movement to the left is allowed.
    /// </summary>
    public void MoveLeft()
    {
        var validMovements =
            _mazeMap[(_currX, _currY)];

        if (!validMovements[0])
        {
            throw new InvalidOperationException(
                "Can't go that way!"
            );
        }

        _currX--;
    }

    /// <summary>
    /// Check whether movement to the right is allowed.
    /// </summary>
    public void MoveRight()
    {
        var validMovements =
            _mazeMap[(_currX, _currY)];

        if (!validMovements[1])
        {
            throw new InvalidOperationException(
                "Can't go that way!"
            );
        }

        _currX++;
    }

    /// <summary>
    /// Check whether movement upward is allowed.
    /// </summary>
    public void MoveUp()
    {
        var validMovements =
            _mazeMap[(_currX, _currY)];

        if (!validMovements[2])
        {
            throw new InvalidOperationException(
                "Can't go that way!"
            );
        }

        _currY--;
    }

    /// <summary>
    /// Check whether movement downward is allowed.
    /// </summary>
    public void MoveDown()
    {
        var validMovements =
            _mazeMap[(_currX, _currY)];

        if (!validMovements[3])
        {
            throw new InvalidOperationException(
                "Can't go that way!"
            );
        }

        _currY++;
    }

    public string GetStatus()
    {
        return $"Current location " +
               $"(x={_currX}, y={_currY})";
    }
}