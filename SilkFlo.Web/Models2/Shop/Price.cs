namespace SilkFlo.Web.Models.Shop
{
    public partial class Price
    {
        public decimal? AmountOther { get; set; }

        public int DiscountPercent { get; set; }

        public bool IsDiscounted => DiscountPercent > 0;

        public decimal AmountDiscounted
        { 
            get
            {
               var amount = Amount ?? 0;
               if(IsDiscounted)
                    return amount - amount / 100 * DiscountPercent;

               return amount;
            }
        }


        private decimal? _monthlyAmount;
        public decimal MonthlyAmount
        {
            get
            {
                //if(_monthlyAmount != null)
                //    return _monthlyAmount.Value;

                _monthlyAmount = Amount ?? 0;
                if (PeriodId == Data.Core.Enumerators.Period.Annual.ToString())
                    return _monthlyAmount.Value / 12;

                return _monthlyAmount.Value;

                //return Amount??0;
            }

            set => _monthlyAmount = value;
        }



        private decimal? _yearlyAmount;
        public decimal YearlyAmount
        {
            get
            {
                if (_yearlyAmount != null)
                    return _yearlyAmount.Value;

                _yearlyAmount = Amount ?? 0;

                return _yearlyAmount.Value;
            }

            set => _yearlyAmount = value;
        }

        public decimal MonthlyAmountDiscounted
        {
            get
            {
                var amount = MonthlyAmount;

                if (IsDiscounted)
                    return amount - amount / 100 * DiscountPercent;

                return amount;
            }
        }

        public string ReferrerCode { get; set; }
    }
}