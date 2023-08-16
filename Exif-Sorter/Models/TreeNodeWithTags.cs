using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exif_Sorter.Models;

namespace Exif_Sorter.Models
{
    [Serializable()]
    public class TreeNodeWithTags
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public ImageEntryObject Tag { get; set; }
        public List<TreeNodeWithTags> Nodes { get; set; }
    }
}
