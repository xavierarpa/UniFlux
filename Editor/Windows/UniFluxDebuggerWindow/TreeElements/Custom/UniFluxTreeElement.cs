using System;
using UnityEngine;

namespace UniFlux.Editor
{
    [Serializable]
    internal class UniFluxTreeElement : TreeElement
    {
        public Texture Icon { get; }

        public UniFluxTreeElement(
            string name,
            int depth,
            int id,
            Texture icon
            ) : base(name, depth, id)
        {
            Icon = icon;
        }
    }
}