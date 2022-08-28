using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUsable
{
    public ItemData Data { get; }
    public void Set();
    public void Unset();
}
