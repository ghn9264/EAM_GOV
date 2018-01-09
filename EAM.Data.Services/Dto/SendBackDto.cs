using System;
using System.Collections.Generic;
using EAM.Data.Domain;

namespace EAM.Data.Services.Dto
{
    public class SendBackDto
    {
        public SendBackDto()
        {
            AssetsStatus = new Dictionary<string, string>();
        }
        public int AquireId { get; set; }
        public string SendBackPerson { get; set; }
        public DateTime SendBackDate { get; set; }
        public string SendBackMemo { get; set; }
        public Dictionary<string, string> AssetsStatus { get; set; }
    }
}