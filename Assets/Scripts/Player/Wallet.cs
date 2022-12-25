///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

public class Wallet
{
    private         int                 sum         = 0;
    public          int                 Sum => sum;

    public          Wallet()
    {
        this.sum = 100;
    }

    public          Wallet(int sum)
    {
        this.sum = sum;
    }

    public          void                PutMoney(int sum)
    {
        this.sum += sum;
    }

    public          bool                TakeMoney(int sum)
    {
        if(this.sum - sum >= 0)
        {
            this.sum -= sum;
            return true;
        }
        else
        {
            return false;
        }
    }
}
