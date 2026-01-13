using System.Numerics;
using Raylib_cs;

int MaxX = 20;
int MaxY = 20;

bool[,] IsAlive = new bool[MaxX, MaxY];
bool RunningLoop = true;

Raylib.InitWindow(MaxX*25,MaxY*25+60,"Game of Life");
Raylib.SetTargetFPS(60);

Vector2 LastMousePos=Raylib.GetMousePosition();
int FrameCount = 0;

if(!File.Exists("Save.txt"))
{
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
}
else
{
    StreamReader sr = new("Save.txt");
    for (int y=0;y<MaxY;y++)
    { 
        string line = sr.ReadLine();
        for (int x = 0; x<MaxX; x++)
        {
            if (line[x].ToString() == "o")
            {
                IsAlive[x,y]=true;
            }
        }
    }
    sr.Close();
}

while (!Raylib.WindowShouldClose())
{
    FrameCount++;
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    
    for (int y=0;y<MaxY;y++)//Draws the game
    { 
        for (int x = 0; x<MaxX; x++)
        {
            if (IsAlive[x,y])
            {
                Raylib.DrawRectangle(x*25,y*25,25,25,Color.White);
            }
        }
    }
    if (Raylib.IsKeyPressed(KeyboardKey.Tab))//The game automatically runs, toggleble with TAB
    {
        if (RunningLoop)
        {
            RunningLoop=false;
        }
        else
        {    
            RunningLoop=true;
        }

    }
    if (Raylib.IsKeyPressed(KeyboardKey.Space)||FrameCount%30==0&&RunningLoop||FrameCount%5==0&&RunningLoop&&Raylib.IsKeyDown(KeyboardKey.LeftShift)) //Runs the next generation
    {
        IsAlive=NextGeneration(IsAlive,MaxX,MaxY);
    }
    if (Raylib.IsMouseButtonDown(MouseButton.Left)&&LastMousePos!=Vector2.Create(Raylib.GetMouseX() / 25, Raylib.GetMouseY()/25)&&Raylib.GetMouseX()/25<MaxX&&Raylib.GetMouseX()>0&&Raylib.GetMouseY()/25<MaxY&&Raylib.GetMouseY()>0)//The user can click in order to toggle the square the mouse is on, hold down to toggle multiple squares
    {
        if (IsAlive[Raylib.GetMouseX()/25, Raylib.GetMouseY()/25]==false)
        {
            IsAlive[Raylib.GetMouseX()/25, Raylib.GetMouseY()/25]=true;
        }
        else
        {
            IsAlive[Raylib.GetMouseX()/25, Raylib.GetMouseY()/25]=false;
        }
        LastMousePos=Vector2.Create(Raylib.GetMouseX() / 25, Raylib.GetMouseY()/25); //Update what the last square was so we don't toggle it repeatedly
    }
    if(!Raylib.IsMouseButtonDown(MouseButton.Left))
    {
        LastMousePos=Vector2.Create(-5); // Set LastMousePos off-screen, otherwise you can't re-hold on the same square you ended on.
    }
    if(Raylib.IsKeyPressed(KeyboardKey.Enter))
    {
        StreamWriter Sw = new("Save.txt");
        for (int y = 0; y < MaxY; y++)
        {
            string TempString = null;
            for (int x = 0; x < MaxX; x++)
            {
                if (IsAlive[x,y])
                {
                    TempString+="o";
                }
                else
                {
                    TempString+=" ";
                }
            }
            Sw.WriteLine(TempString);
        }
        Sw.Close();
    }
    Raylib.DrawRectangle(0,25*MaxY,MaxX*25,60,Color.LightGray);
    Raylib.DrawText("Press Space to go to the next generation", 10, MaxY*25, 20, Color.Black);
    Raylib.DrawText("Press TAB to toggle generations", 10, MaxY*25+20, 20, Color.Black);
    Raylib.DrawText("Hold Left Shift to speed up generations", 10, MaxY*25+40, 20, Color.Black);



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
            if (NeighboursAlive==3||NeighboursAlive==2&&CurrentGeneration[x,y]) //Har sett själva (3 grannar eller 2 och levande) online, men kodade själv. Source: https://youtu.be/tPr5b_06GF4?t=390
            {
                NextGeneration[x,y]=true;
            }
        }
    }
    return NextGeneration;
}