using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungWook.LUIS.PTT.Entities
{
    public class Intent
    {
        public string name { get; set; }
    }

    public class Entity
    {
        public string name { get; set; }
        //public List<string> children { get; set; }
    }

    public class ModelFeature
    {
        public string name { get; set; }
        public bool mode { get; set; }
        public string words { get; set; }
        public bool activated { get; set; }
    }

    public class RegexFeature
    {
        public string name { get; set; }
        public string pattern { get; set; }
        public bool activated { get; set; }
    }

    public class Entity2
    {
        public string entity { get; set; }
        public int startPos { get; set; }
        public int endPos { get; set; }
    }

    public class Utterance
    {
        public string text { get; set; }
        public string intent { get; set; }
        public List<Entity2> entities { get; set; }
    }

    public class LuisEntity
    {
        public string luis_schema_version { get; set; }
        public string versionId { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string culture { get; set; }
        public List<Intent> intents { get; set; }
        public List<Entity> entities { get; set; }
        public List<object> composites { get; set; }
        public List<object> closedLists { get; set; }
        public List<object> bing_entities { get; set; }
        public List<object> actions { get; set; }
        public List<ModelFeature> model_features { get; set; }
        public List<RegexFeature> regex_features { get; set; }
        public List<Utterance> utterances { get; set; }
    }
}
