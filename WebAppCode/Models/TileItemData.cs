using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppCode.Models
{
    public class TileItemData
    {
        public string Id { get; set; }
        public string Caption { get; set; }
        public int Amount { get; set; }
        public string ImageSrc { get; set; }
        public string Href { get; set; }
    }
}