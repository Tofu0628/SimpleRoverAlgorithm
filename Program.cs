public enum Direction
{
    North,
    East,
    South,
    West
}

public class Rover
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction CurrentDirection { get; set; }
    public int BoundaryX { get; set; }
    public int BoundaryY { get; set; }

    public event Action<string> ReachedBoundary;

    public Rover(int x, int y, Direction direction, int boundaryX, int boundaryY)
    {
        X = x;
        Y = y;
        CurrentDirection = direction;
        BoundaryX = boundaryX;
        BoundaryY = boundaryY;
    }

    private void CheckBoundary()
    {
        if (X < -BoundaryX)
        {
            X = -BoundaryX;
            ReachedBoundary?.Invoke(" ======= Reached Boundary at left! ======= ");
        }
        else if (Y < -BoundaryY)
        {
            Y = -BoundaryY;
            ReachedBoundary?.Invoke(" ======= Reached Boundary at back ======= ");
        }
        else if (X >= BoundaryX)
        {
            X = BoundaryX;
            ReachedBoundary?.Invoke(" ======= Reached Boundary at right! ======= ");
        }
        else if (Y >= BoundaryY)
        {
            Y = BoundaryY;
            ReachedBoundary?.Invoke(" ======= Reached Boundary at front! ======= ");
        }
    }

    public void MoveForward()
    {
        switch (CurrentDirection)
        {
            case Direction.North:
                Y++;
                break;
            case Direction.East:
                X++;
                break;
            case Direction.South:
                Y--;
                break;
            case Direction.West:
                X--;
                break;
        }
        CheckBoundary();
    }

    public void MoveBackward()
    {
        switch (CurrentDirection)
        {
            case Direction.North:
                Y--;
                break;
            case Direction.East:
                X--;
                break;
            case Direction.South:
                Y++;
                break;
            case Direction.West:
                X++;
                break;
        }
        CheckBoundary();
    }

    public void TurnLeft()
    {
        CurrentDirection = (Direction)(((int)CurrentDirection + 3) % 4);
    }

    public void TurnRight()
    {
        CurrentDirection = (Direction)(((int)CurrentDirection + 1) % 4);
    }
}

public class Program
{
    public static void Main()
    {
        int boundaryX = 10; // 你可以根据需要修改初始边界
        int boundaryY = 10;

        Rover rover = new Rover(0, 0, Direction.North, boundaryX, boundaryY);
        List<string> boundaryMessages = new List<string>();

        
        rover.ReachedBoundary += (message) =>
        {
            boundaryMessages.Clear();
            boundaryMessages.Add(message);
        };

        while (true)
        {

            // 显示之前的边界事件消息
            foreach (var message in boundaryMessages)
            {
                Console.WriteLine(message);
            }

            Console.WriteLine($"Rover current position : ({rover.X}, {rover.Y}), Direction: {rover.CurrentDirection}\n ");
            Console.WriteLine("Enter instructions (or 'q' to quit): ");
            string instructions = Console.ReadLine().ToLower(); // Convert to lowercase

            if (instructions == "q")
            {
                break;
            }
            Console.Clear();
            foreach (char instruction in instructions)
            {
                switch (instruction)
                {
                    case 'f':
                        rover.MoveForward();
                        break;
                    case 'b':
                        rover.MoveBackward();
                        break;
                    case 'l':
                        rover.TurnLeft();
                        break;
                    case 'r':
                        rover.TurnRight();
                        break;
                    default: Console.WriteLine("Please input valid input.");
                        break;
                }
            }
            Console.WriteLine($"Final Position: ({rover.X}, {rover.Y}), Direction: {rover.CurrentDirection}\n");
        }
    }
}
