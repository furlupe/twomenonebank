using System.Net.Sockets;

namespace Bank.Core.Domain.Events;

public class Transfer : DomainEvent
{
    public long Value => Withdrawal.Value;
    public string Comment { get; protected set; }
    public Withdrawal Withdrawal { get; protected set; }
    public Deposit Deposit { get; protected set; }
}
