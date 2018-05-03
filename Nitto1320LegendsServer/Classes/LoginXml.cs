using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nitto1320LegendsServer.Classes
{
    [Serializable]
    public class LoginXml
    {
        /// <summary>
        /// Background Color
        /// </summary>
        [XmlAttribute]
        public string bg { get; set; }

        /// <summary>
        /// Selected Car
        /// </summary>
        [XmlAttribute]
        public int dc { get; set; }

        /// <summary>
        /// Account Id
        /// </summary>
        [XmlAttribute]
        public string i { get; set; }

        /// <summary>
        /// Instant Mail?
        /// </summary>
        [XmlAttribute]
        public string im { get; set; }

        /// <summary>
        /// Location Id
        /// </summary>
        [XmlAttribute]
        public int lid { get; set; }

        /// <summary>
        /// Money
        /// </summary>
        [XmlAttribute]
        public float m { get; set; }

        /// <summary>
        /// Points
        /// </summary>
        [XmlAttribute]
        public int p { get; set; }

        /// <summary>
        /// Street Cred
        /// </summary>
        [XmlAttribute]
        public int sc { get; set; }

        /// <summary>
        /// Team Id
        /// </summary>
        [XmlAttribute]
        public int ti { get; set; }

        /// <summary>
        /// Team Role
        /// </summary>
        [XmlAttribute]
        public int tr { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [XmlAttribute]
        public string u { get; set; }
    }
}
