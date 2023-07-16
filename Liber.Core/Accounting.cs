namespace Liber;

internal static class Accounting
{
    public static void DebitCredit(decimal balance, out decimal debit, out decimal credit)
    {
        if (balance < 0)
        {
            debit = 0;
            credit = -balance;
        }
        else
        {
            debit = balance;
            credit = 0;
        }
    }
}
