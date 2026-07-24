using System.Linq;

namespace Picklr.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new();

        public int Count => Items.Count;

        public decimal Total => Items.Sum(i => i.Fee);

        public void Add(CartItem item)
        {
            Items.Add(item);
        }

        public void Remove(int programId)
        {
            var item = Items.FirstOrDefault(i => i.ProgramID == programId);

            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}