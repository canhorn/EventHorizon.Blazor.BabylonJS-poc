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
        public ClickCaptureProvider ClickCapture { get; set; } = null!;

        [Parameter]
        public TreeViewNodeData Node { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ContextMenuState ContextMenu { get; set; } = new ContextMenuState();

        protected bool IsParentNode => Node.Children?.Count > 0;


        protected override void OnInitialized()
        {
            if (Node.ContextMenu != null && Node.ContextMenu.Items.Count > 0)
            {
                ContextMenu.Enabled = true;
                ClickCapture.OnMouseClick(HandleCloseContextMenu);
                ClickCapture.OnContextMenu(HandleCloseContextMenu);
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
            return !string.IsNullOrWhiteSpace(
                Node.Href
            ) ? Node.Href : "javascript:;";
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

        protected Task HandleClickOfNode()
        {
            if (Node.IsDisabled)
            {
                return Task.CompletedTask;
            }
            if (!IsParentNode)
            {
                // TODO: Look into if this is necessary for href on parent node clicks
                //NavigationManager.NavigateTo(
                //    Node.Href
                //);
                return Task.CompletedTask;
            }
            Node.IsExpanded = !Node.IsExpanded;
            return Task.CompletedTask;
        }

        protected string GetNavLinkClass()
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
                        ClickCapture.OffMouseClick(ContextMenu.HandleCloseContextMenu);
                        ClickCapture.OffContextMenu(ContextMenu.HandleCloseContextMenu);
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
