using Raylib_cs;



bool[,] IsAlive = new bool[10,10];

Raylib.InitWindow(250,250,"Game of Life");
Raylib.SetTargetFPS(60);


for (int y=0;y<10;y++)
{ 
    for (int x = 0; x<10; x++)
    {
        Console.CursorLeft=x;
        Console.CursorTop=y;
        if (Random.Shared.Next(2)==0)
        {
            IsAlive[x,y]=true;
        }
        if (IsAlive[x,y]==true)
        {
            Console.Write("E");
        }
    }
}


Console.ReadLine();
