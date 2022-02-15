using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Ekz
{
    public partial class Service
    {
        private string visiable;

        public string MyVisiable
        {
            get { return visiable; }
            set { visiable = value; }
        }

        private string line;

        public string MyLine
        {
            get { return line; }
            set { line = value; }
        }

        private decimal newcost;

        public decimal MyNewCost
        {
            get { return newcost; }
            set { newcost = value; }
        }

        private string textsale;

        public string MyTextSale
        {
            get { return "* Скидка " + (skidka * 100).ToString() + "%"; }
            set { textsale = value; }
        }

        private string green;

        public string MyGreen
        {
            get { return green; }
            set { green = value; }
        }

    }
}