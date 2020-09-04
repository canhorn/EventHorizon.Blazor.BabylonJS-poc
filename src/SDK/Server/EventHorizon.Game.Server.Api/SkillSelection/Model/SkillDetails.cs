namespace EventHorizon.Game.Server.SkillSelection.Model
{
    using System;
    using System.Threading.Tasks;

    public class SkillDetails
    {
        public string SkillName { get; set; }
        public Func<Task> OnClick { get; set; }
    }
}
