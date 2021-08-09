using Entitas;
using System;

namespace Hello.Entity
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Entitas.
        }
    }

    public static class GetEntity
    {
        public static GameEntity CreateRedGem(this GameContext context, Vector3 position)
        {
            var entity = context.CreateEntity();
            entity.isGameBoardElement = true;
            entity.isMovable = true;
            entity.AddPosition(position);
            entity.AddAsset("RedGem");
            entity.isInteractive = true;
            return entity;
        }
    }
}
