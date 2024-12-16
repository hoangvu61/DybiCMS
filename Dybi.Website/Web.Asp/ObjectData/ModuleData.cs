using System.Collections.Generic;

namespace Web.Asp.ObjectData
{
    using Model.SeedWork;
    using System;

    [Serializable]
    public class ModuleData
    {
        public string Position { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SkinName { get; set; }
        public string SkinPath { get; set; }
        public IList<ModuleParamData> Params { get; set; }
        public ModuleSkin Skin { get; set; }

        public ModuleData()
        {
            Params = new List<ModuleParamData>();
        }
    }
}
