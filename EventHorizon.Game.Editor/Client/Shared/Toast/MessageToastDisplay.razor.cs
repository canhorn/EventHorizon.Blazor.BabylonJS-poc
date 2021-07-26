namespace EventHorizon.Game.Editor.Client.Shared.Toast
{
    using System;
    using System.Threading.Tasks;
    using System.Timers;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using Microsoft.AspNetCore.Components;

    public class MessageToastDisplayModel
        : ComponentBase,
        IAsyncDisposable
    {
        [Parameter]
        public MessageModel Message { get; set; }
        [Parameter]
        public EventCallback<string> OnRemove { get; set; }

        public bool ShowMessage { get; set; } = true;
        public string MessageLevelStyle { get; set; } = string.Empty;

        private readonly Timer _countdown = new(5000);
        private readonly Timer _removeTimer = new(1000);

        protected override void OnInitialized()
        {
            SetupMessageState();

            _countdown.Elapsed += HideMessageCallback;
            _countdown.AutoReset = false;
            _removeTimer.Elapsed += HandleRemoveCallback;

            _countdown.Stop();
            _countdown.Start();
        }

        public ValueTask DisposeAsync()
        {
            _countdown.Dispose();
            _removeTimer.Dispose();

            return ValueTask.CompletedTask;
        }

        public void SetupMessageState()
        {
            ShowMessage = true;
            MessageLevelStyle = string.Empty;
            if (Message.Level == MessageLevel.Success)
            {
                MessageLevelStyle = "--success";
            }
            else if (Message.Level == MessageLevel.Warning)
            {
                MessageLevelStyle = "--warning";
            }
            else if (Message.Level == MessageLevel.Error)
            {
                MessageLevelStyle = "--error";
            }
        }

        public void HandleHide()
        {
            ShowMessage = false;
            _removeTimer.Stop();
            _removeTimer.Start();
        }

        private void HideMessageCallback(
            object _,
            ElapsedEventArgs __
        )
        {
            HandleHide();
            InvokeAsync(StateHasChanged);
        }

        private void HandleRemoveCallback(
            object _,
            ElapsedEventArgs __
        )
        {
            InvokeAsync(
                async () =>
                {
                    await OnRemove.InvokeAsync(
                        Message.Id
                    );
                }
            );
        }
    }
}
