using System.ComponentModel;

namespace RentService.Core.Enums
{
    public enum RentPlan
    {
        [Description("7 dias com um custo de R$30,00 por dia")]
        Seven = 7,

        [Description("15 dias com um custo de R$28,00 por dia")]
        Fifteen = 15,

        [Description("30 dias com um custo de R$22,00 por dia")]
        Thirty = 30,

        [Description("45 dias com um custo de R$20,00 por dia")]
        FortyFive = 45,

        [Description("50 dias com um custo de R$18,00 por dia")]
        Fifty = 50
    }
}
