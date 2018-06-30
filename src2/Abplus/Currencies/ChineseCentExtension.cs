namespace Abp.Currencies
{
    public static class ChineseCentExtension
    {
        public static decimal ToYuan(this int moneyInCent)
        {
            return moneyInCent / 100m;
        }

        public static string ToYuanString(this int moneyInCent)
        {
            return $"{moneyInCent.ToYuan().ToString("0.00")}元";
        }
    }
}
