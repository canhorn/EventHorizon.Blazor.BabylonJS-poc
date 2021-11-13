namespace EventHorizon.Game.Editor.Automation.EntityEditor.Pages
{
    using Atata;

    using EventHorizon.Game.Editor.Automation.Components.Toolbar;
    using EventHorizon.Game.Editor.Automation.Layout;
using EventHorizon.Game.Editor.Automation.ZoneCommands.Components.ClientEntity;

    using _ = EntityEditorPage;

    [Url("/zone/entity")]
    public class EntityEditorPage : MainLayoutPage<_>
    {
        public H1<_> Header { get; private set; }

        [TestSelector("client-entity-list__header")]
        public H2<_> ClientEntityListHeader
        {
            get;
            private set;
        }
        [TestSelector("client-entity__toolbar")]
        public StandardToolbarComponent<
            _,
            StandardToolbarButtonComponent<_>
        > ClientEntityToolbar { get; private set; }
        [TestSelector("client-entity__list")]
        public ClientEntityListComponent<_> ClientEntityList { get; private set; }

        [TestSelector("agent-entity-list__header")]
        public H2<_> AgentEntityListHeader
        {
            get;
            private set;
        }
        [TestSelector("agent-entity__toolbar")]
        public StandardToolbarComponent<
            _,
            StandardToolbarButtonComponent<_>
        > AgentEntityToolbar
        { get; private set; }
    }
}
