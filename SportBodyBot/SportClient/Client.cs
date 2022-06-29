using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBodyBot.SportClient
{
    public class Client
    {
        public bool _water { get; set; } = false;
        public string weight { get; set; }
        public int water1 { get; set; }
        public bool _drunkWater { get; set; } = false;
        public string count { get; set; }
        public int total { get; set; }
        public  string equip { get; set; }
        public string choise { get; set; }
        public string nameofexercise { get; set; }
        //public string[] exercises = new string[] { };
        public string exercisename { get; set; }
        public bool _gifshow { get; set; } = false;
        public bool calories { get; set; } = false;
        public string time { get; set; }
        public bool bodypart { get; set; } = false;
        public bool target { get; set; } = false;
        public bool _listremove { get; set; }
        public string delexercise { get; set; }

    }
}
