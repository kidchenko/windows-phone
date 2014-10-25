using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipCalculator
{
    public class Tip
    {
        private readonly double _tipPercentage;

        public decimal BillAmount { get; private set; }

        public decimal TipAmount => BillAmount * (decimal)_tipPercentage;

        public decimal TotalAmount => BillAmount + TipAmount;

        public Tip(decimal billAmount, double tipPercentage)
        {
            BillAmount = billAmount;
            _tipPercentage = tipPercentage;
        }
    }
}
