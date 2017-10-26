using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Structure.Utils
{
   public  class UnderlineEffect : RoutingEffect
    {
        public const string EffectNamespace = "Example";

        public UnderlineEffect() : base($"{EffectNamespace}.{nameof(UnderlineEffect)}")
        {
        }
    }
}
