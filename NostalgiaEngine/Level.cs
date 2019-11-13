using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NostalgiaEngine
{
    public class NostalgiaLevel : IDisposable
    {
        private List<NostalgiaEntity> nostalgiaEntities = new List<NostalgiaEntity>();

        public Room[] Rooms { get; private set; }

        public NostalgiaLevel()
        {

        }

        public virtual void Start()
        {
            Room r = new Room();

            r.Points = new Vector2[] { new Vector2(10, 10), new Vector2(500, 500), new Vector2(500, 10) };

            Rooms = new Room[] { r };

            CreateEntity<NostalgiaPlayer>();
        }

        public virtual void Update(float deltaTime)
        {
            foreach (var entity in nostalgiaEntities)
            {
                entity.Update(deltaTime);
            }
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            foreach (var entity in nostalgiaEntities)
            {
                entity.Render(spriteBatch);
            }
        }

        public virtual void End()
        {
            foreach (var entity in nostalgiaEntities)
            {
                entity.Dispose();
            }

            nostalgiaEntities.Clear();
        }





        public T CreateEntity<T>()
            where T : NostalgiaEntity, new()
        {
            T newEnt = new T();

            nostalgiaEntities.Add(newEnt);

            newEnt.Level = this;

            newEnt.Start();

            return newEnt;
        }

        public T[] GetEntities<T>()
            where T : NostalgiaEntity
        {
            return (from NostalgiaEntity enity in nostalgiaEntities
                   where enity is T
                    select (T)enity).ToArray();
        }

        public T GetEntity<T>()
            where T : NostalgiaEntity
        {
            T[] ents = GetEntities<T>();

            if (ents.Length > 0)
                return ents[0];
            else
                return null;
        }

        public bool RemoveEntity(NostalgiaEntity entity)
        {
            if(nostalgiaEntities.Contains(entity))
            {
                nostalgiaEntities.Remove(entity);

                entity.Dispose();

                return true;
            }

            return true;
        }




        public void Dispose()
        {
            End();
        }
    }
}
