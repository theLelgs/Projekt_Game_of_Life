using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

int MaxX = 20;
int MaxY = 20;

bool[,] IsAlive = new bool[MaxX*25, MaxY*25];

Raylib.InitWindow(MaxX*25,MaxY*25,"Game of Life");
Raylib.SetTargetFPS(60);

int FrameCount = 0;


for (int y=0;y<MaxY;y++)
{ 
    for (int x = 0; x<MaxX; x++)
    {
        if (Random.Shared.Next(2)==0)
        {
            IsAlive[x,y]=true;
        }
    }
}



while (!Raylib.WindowShouldClose())
{
    FrameCount++;
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);

    for (int y=0;y<MaxY;y++)
    { 
        for (int x = 0; x<MaxX; x++)
        {
            if (IsAlive[x,y])
            {
                Raylib.DrawRectangle(x*25,y*25,25,25,Color.White);
            }
        }
    }

    // while(Raylib.GetKeyPressed()==0);
    if (Raylib.IsKeyPressed(KeyboardKey.Space))
    {
        IsAlive=NextGeneration(IsAlive,MaxX,MaxY);
    }


    if (Raylib.IsMouseButtonPressed(MouseButton.Left)&&Raylib.GetMouseX()/25<MaxX&&Raylib.GetMouseX()>0&&Raylib.GetMouseY()/25<MaxY&&Raylib.GetMouseY()>0)
    {
        if (IsAlive[Raylib.GetMouseX()/25, Raylib.GetMouseY()/25]==false)
        {
            IsAlive[Raylib.GetMouseX()/25, Raylib.GetMouseY()/25]=true;
        }
        else
        {
            IsAlive[Raylib.GetMouseX()/25, Raylib.GetMouseY()/25]=false;
        }
    }
    


    

    Raylib.EndDrawing();
}





static bool[,] NextGeneration(bool[,] CurrentGeneration, int MaxX, int MaxY)
{
    bool[,] NextGeneration = new bool[MaxX,MaxY];
    List<(int,int)> SurroundingSquares = [(1,0),(1,1),(0,1),(-1,1),(-1,0),(-1,-1),(0,-1),(1,-1)];
    for (int y = 0; y<MaxY; y++)
    {
        for (int x = 0; x<MaxX;x++)
        {
            int NeighboursAlive = 0;
            foreach ((int,int) PositionOffset in SurroundingSquares)
            {
                if (x+PositionOffset.Item1>=0&&x+PositionOffset.Item1<MaxX&&y+PositionOffset.Item2>=2&&y+PositionOffset.Item2<MaxY)
                {
                    if (CurrentGeneration[x+PositionOffset.Item1, y+PositionOffset.Item2]==true)
                    {
                        NeighboursAlive++;
                    }
                }                
            }
            Console.WriteLine($"X={x}, y={y}. Neighbours = {NeighboursAlive}. Alive: {CurrentGeneration[x,y]}");
            if (NeighboursAlive==3||NeighboursAlive==2&&CurrentGeneration[x,y])
            {
                NextGeneration[x,y]=true;
            }
        }
    }
    return NextGeneration;
}