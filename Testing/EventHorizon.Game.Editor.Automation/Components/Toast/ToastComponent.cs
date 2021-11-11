namespace EventHorizon.Game.Editor.Automation.Components.Toast
{
    using Atata;
    using EventHorizon.Game.Editor.Automation.Core.Exceptions;

    public class ToastComponent<TNavigateTo>
        : Control<TNavigateTo>
            where TNavigateTo : PageObject<TNavigateTo>
    {
        private ControlList<ToastMessageDisplayItem, TNavigateTo> Messages { get; set; }

        public TNavigateTo WaitForMessage(
            string message
        )
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    var control = Messages.GetByXPathCondition(
                        $"//*[@data-test-selector=\"toast-message\"][. = '{message}']"
                    );
                    control.Should.Exist();
                    return Owner;
                }
                catch
                {
                }
                Go.To<TNavigateTo>(navigate: false).Wait(0.2);
            }

            throw new WaitForMessageException(
                $"Failed to find Message of '{message}'."
            );
        }

        [ControlDefinition("div", ContainingClass = "toast")]
        public class ToastMessageDisplayItem
            : Control<TNavigateTo>
        {
            [TestSelector("toast-header")]
            [WaitFor(AbsenceTimeout = 0.2, PresenceTimeout = 0.2)]
            public H3<TNavigateTo> Header { get; private set; }

            [TestSelector("toast-level")]
            [WaitFor(AbsenceTimeout = 0.2, PresenceTimeout = 0.2)]
            public Text<TNavigateTo> Level { get; private set; }

            [TestSelector("toast-message")]
            [WaitFor(AbsenceTimeout = 0.2, PresenceTimeout = 0.2)]
            public Text<TNavigateTo> Message { get; private set; }
        }
    }
}
