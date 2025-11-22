using Raylib_cs;



bool[,] IsAlive = new bool[10,10];

Raylib.InitWindow(250,250,"Game of Life");
Raylib.SetTargetFPS(60);


for (int y=0;y<10;y++)
{ 
    for (int x = 0; x<10; x++)
    {
        if (Random.Shared.Next(2)==0)
        {
            IsAlive[x,y]=true;
        }
    }
}



while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);

    for (int y=0;y<10;y++)
    { 
        for (int x = 0; x<10; x++)
        {
            if (IsAlive[x,y])
            {
                Raylib.DrawRectangle(x*25,y*25,25,25,Color.White);
            }
        }
    }


    Raylib.EndDrawing();
}

