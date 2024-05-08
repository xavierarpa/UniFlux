using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniFlux.Editor
{
    [Serializable]
    internal class UniFluxTreeElement : TreeElement
    {
        public readonly Func<string> Resolutions;
        public Texture Icon { get; }
        public string[] Contracts { get; }
        public string ResolutionType { get; }
        public List<CallSite> Callsite { get; }
        public string Kind { get; }

        public UniFluxTreeElement(
            string name,
            int depth,
            int id,
            Texture icon,
            Func<string> resolutions,
            string[] contracts,
            string resolutionType,
            List<CallSite> callsite,
            string kind
            ) : base(name, depth, id)
        {
            Resolutions = resolutions;
            ResolutionType = resolutionType;
            Contracts = contracts;
            Icon = icon;
            Callsite = callsite;
            Kind = kind;
        }
    }
}