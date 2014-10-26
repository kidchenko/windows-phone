using System;
using System.Collections.Generic;
using System.Text;

namespace TipCalculatorUniversalApp
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
