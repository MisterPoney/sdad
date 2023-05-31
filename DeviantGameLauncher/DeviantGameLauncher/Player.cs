using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviantGameLauncher
{
    public class Player
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("lastUpdated")]
        public long LastUpdated { get; set; }

        [BsonElement("pontos")]
        public int Pontos { get; set; }

        [BsonElement("level")]
        public int Level { get; set; }

        [BsonElement("kills")]
        public int Kills { get; set; }

        [BsonElement("itensCollected")]
        public ItensCollected ItensCollected { get; set; }

        [BsonElement("enemysKilled")]
        public EnemysKilled EnemysKilled { get; set; }

        [BsonElement("playerPosition")]
        public PlayerPosition PlayerPosition { get; set; }
    }

    public class ItensCollected
    {
        [BsonElement("keys")]
        public List<string> Keys { get; set; }

        [BsonElement("values")]
        public List<bool> Values { get; set; }
    }

    public class EnemysKilled
    {
        [BsonElement("keys")]
        public List<string> Keys { get; set; }

        [BsonElement("values")]
        public List<bool> Values { get; set; }
    }

    public class PlayerPosition
    {
        [BsonElement("x")]
        public double X { get; set; }

        [BsonElement("y")]
        public double Y { get; set; }

        [BsonElement("z")]
        public double Z { get; set; }
    }
}
