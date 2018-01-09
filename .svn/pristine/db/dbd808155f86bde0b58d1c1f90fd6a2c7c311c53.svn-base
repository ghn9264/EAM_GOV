using System.Collections.Generic;

namespace Eam.Web.Portal._Comm
{
    public class TreeNodeBase<TKey>
    {
        public TreeNodeBase()
        {
            pId = default(TKey);
            open = true;
            children = new List<TreeNodeBase<TKey>>();
        }

        public TKey id { get; set; }
        public string name { get; set; }

        public TKey pId { get; set; }
        public string pName { get; set; }

        public bool open { get; set; }

        public bool isParent { get; set; }

        public virtual List<TreeNodeBase<TKey>> children { get; set; }

    }
}