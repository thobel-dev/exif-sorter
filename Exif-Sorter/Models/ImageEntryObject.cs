using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exif_Sorter.Models
{
    [Serializable()]
    public class ImageEntryObject
    {
        public ImageEntryObject(string aufnahmedatum, string dateiname, string elternordner, string zielordner, string neuerZielordner, long dateigroesse, string onlyFilename)
        {
            Aufnahmedatum = aufnahmedatum;
            Dateiname = dateiname;
            Elternordner = elternordner;
            Zielordner = zielordner;
            NeuerZielordner = neuerZielordner;
            Dateigroesse = dateigroesse;
            OnlyFilename = onlyFilename;
        }

        public string Aufnahmedatum { get; set; }
        public string Dateiname { get; set; }
        public string Elternordner { get; set; }
        public string Zielordner { get; set; }
        public string NeuerZielordner { get; set; }
        public long Dateigroesse { get; set; }
        public string OnlyFilename { get; set; }

    }
}
