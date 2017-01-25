using PayLab_BP.Payments.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PayLab_BP.Lots.Dto
{
    public class LotDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public IEnumerable<PaymentDto> Payments { get; set; } 
    }
}
