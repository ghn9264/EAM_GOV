using System;
using System.Collections.Generic;
using EAM.Data.Domain;

namespace EAM.Data.Services.Dto
{
    public class ScrapDto
    {
        public ScrapDto()
        {
            ScrapStatus = new Dictionary<string, string>();
        }
        public int ScrapId { get; set; }
        public string ScrapExaminePerson { get; set; }
        public DateTime ScrapExamineDate { get; set; }
        public string ScrapMome { get; set; }
        public Dictionary<string, string> ScrapStatus { get; set; }
    }
}