using System;
using System.Collections.Generic;
using EAM.Data.Domain;

namespace EAM.Data.Services.Dto
{
    public class ReturnDto
    {
        public ReturnDto()
        {
            AssetsStatus = new Dictionary<string, string>();
        }
        public int BorrowId { get; set; }
        public string ReturnPerson { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReturnMome { get; set; }
        public Dictionary<string, string> AssetsStatus { get; set; }
    }
}