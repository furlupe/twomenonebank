using System.ComponentModel.DataAnnotations;

namespace Bank.Idempotency
{
    public class ActionDescriptor
    {
        public string Name { get; set; }
        [Key]
        public Guid IdempotencyKey { get; set; }
    }
}
