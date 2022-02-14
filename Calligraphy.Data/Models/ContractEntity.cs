using System;
using System.ComponentModel.DataAnnotations;

namespace Calligraphy.Data.Models
{
    public class ContractEntity
    {
        [Key] public int ContractId { get; set; }

        public double FinalCost { get; set; }
        public double DownPayment { get; set; }
        public DateTime DateCommissioned { get; set; }
        public DateTime EndDate { get; set; }
        public bool HasSignature { get; set; }
        public bool IsFinished { get; set; }
    }
}