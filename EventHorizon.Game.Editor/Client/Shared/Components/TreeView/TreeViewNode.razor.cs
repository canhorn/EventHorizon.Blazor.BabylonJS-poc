namespace EventHorizon.Game.Editor.Client.Shared.Components.TreeView
{
    using EventHorizon.Game.Editor.Client.Shared.ClickCapture;
    using EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model;

    using Microsoft.AspNetCore.Components;

    using System;
    using System.Threading.Tasks;

    public class TreeViewNodeModel
        : ComponentBase,
          IDisposable
    {
        [CascadingParameter]
        public ClickCaptureProvider ClickCapture { get; set; } =
            null!;

        [Parameter]
        public TreeViewNodeData Node { get; set; } = null!;
        [Parameter]
        public EventCallback<TreeViewNodeData> OnChanged { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; } =
            null!;

        public ContextMenuState ContextMenu { get; set; } =
            new ContextMenuState();

        protected bool IsParentNode =>
            Node.Children?.Count > 0;

        protected override void OnInitialized()
        {
            CheckAndSetupContextMenu();
        }

        protected override void OnParametersSet()
        {
            CheckAndSetupContextMenu();
        }

        private void CheckAndSetupContextMenu()
        {
            if (
                Node.ContextMenu != null
                && Node.ContextMenu.Items.Count > 0
            )
            {
                ContextMenu.Enabled = true;
                ClickCapture.OnMouseClick(
                    HandleCloseContextMenu
                );
                ClickCapture.OnContextMenu(
                    HandleCloseContextMenu
                );
            }
        }

        protected void HandleContextMenu()
        {
            ClickCapture.TriggerEvents();
            ContextMenu.HandleContextMenu();
        }

        protected void HandleCloseContextMenu()
        {
            ContextMenu.HandleCloseContextMenu();
            InvokeAsync(StateHasChanged);
        }

        protected string GetNodeHref()
        {
            return !string.IsNullOrWhiteSpace(Node.Href)
              ? Node.Href
              : "javascript:;";
        }

        protected string GetIconClass()
        {
            return Node.IconCssClass;
        }

        protected string GetExpandedIconClass()
        {
            if (Node.IsExpanded)
            {
                return "oi oi-minus"; // TODO: Move this to [Parameter]
            }
            return "oi oi-plus"; // TODO: Move this to [Parameter]
        }

        protected string GetAriaExpanded()
        {
            return Node.IsExpanded.ToLower();
        }

        protected async Task HandleClickOfNode()
        {
            if (Node.IsDisabled)
            {
                return;
            }
            Node.IsExpanded = !Node.IsExpanded;
            await OnChanged.InvokeAsync(Node);
        }

        protected static string GetNavLinkClass()
        {
            return $"tree-view__node-link --clickable";
        }

        #region IDisposable Support
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (ContextMenu.Enabled)
                    {
                        ClickCapture.OffMouseClick(
                            ContextMenu.HandleCloseContextMenu
                        );
                        ClickCapture.OffContextMenu(
                            ContextMenu.HandleCloseContextMenu
                        );
                    }
                }

                _disposedValue = true;
            }
        }

        ~TreeViewNodeModel()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
    public class ContextMenuState
    {
        public bool Enabled { get; set; } = false;
        public bool Show { get; set; } = false;

        public void HandleContextMenu()
        {
            if (Enabled)
            {
                Show = true;
            }
        }

        public void HandleCloseContextMenu()
        {
            if (Enabled)
            {
                Show = false;
            }
        }
    }
}
