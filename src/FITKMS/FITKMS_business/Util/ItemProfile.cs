using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FITKMS_business.Data;

namespace FITKMS_business.Util
{
    public class ItemProfile
    {
        public ItemProfile(int id, List<Tagovi> tags)
        {
            ID = id;
            ItemTags = new Dictionary<Tagovi, double>();
            foreach (Tagovi item in tags)
            {
                ItemTags.Add(item, 0.00);   
            }
        }

        public int ID { get; set; }
        public Dictionary<Tagovi, double> ItemTags { get; set; }

        public bool TagExists(Tagovi tag)
        {
            foreach (var item in ItemTags)
            {
                if (item.Key.TagID == tag.TagID)
                    return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            ItemProfile it = (ItemProfile)obj;
            if (it == null)
                return false;

            return this.ID == it.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
