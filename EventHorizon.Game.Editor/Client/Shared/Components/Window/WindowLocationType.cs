namespace EventHorizon.Game.Editor.Client.Shared.Components.Window
{
    using System;

    public struct WindowLocationType
    {
        private const int StandardWidthPadding = 5; // 2px Boarder (left-right)
        private const int StandardHeightPadding = 27; // 2px Boarder (top-bottom) + 22px header

        public static readonly WindowLocationType None = string.Empty;
        public static readonly WindowLocationType TopLeft = new(
            "TopLeft",
            true,
            (viewableArea) => (viewableArea.InnerWidth / 3 * 0, viewableArea.InnerHeight / 3 * 0)
        );
        public static readonly WindowLocationType TopCenter = new(
            "TopCenter",
            true,
            (viewableArea) => (viewableArea.InnerWidth / 3 * 1, viewableArea.InnerHeight / 3 * 0)
        );
        public static readonly WindowLocationType TopRight = new(
            "TopRight",
            true,
            (viewableArea) => (viewableArea.InnerWidth / 3 * 2, viewableArea.InnerHeight / 3 * 0)
        );
        public static readonly WindowLocationType MiddleLeft = new(
            "MiddleLeft",
            true,
            (viewableArea) => (viewableArea.InnerWidth / 3 * 0, viewableArea.InnerHeight / 3 * 1)
        );
        public static readonly WindowLocationType MiddleCenter = new(
            "MiddleCenter",
            true,
            (viewableArea) => (viewableArea.InnerWidth / 3 * 1, viewableArea.InnerHeight / 3 * 1)
        );
        public static readonly WindowLocationType MiddleRight = new(
            "MiddleRight",
            true,
            (viewableArea) => (viewableArea.InnerWidth / 3 * 2, viewableArea.InnerHeight / 3 * 1)
        );
        public static readonly WindowLocationType BottomLeft = new(
            "BottomLeft",
            true,
            (viewableArea) => (viewableArea.InnerWidth / 3 * 0, viewableArea.InnerHeight / 3 * 2)
        );
        public static readonly WindowLocationType BottomCenter = new(
            "BottomCenter",
            true,
            (viewableArea) => (viewableArea.InnerWidth / 3 * 1, viewableArea.InnerHeight / 3 * 2)
        );
        public static readonly WindowLocationType BottomRight = new(
            "BottomRight",
            true,
            (viewableArea) => (viewableArea.InnerWidth / 3 * 2, viewableArea.InnerHeight / 3 * 2)
        );

        public WindowLocationType(
            string location,
            bool gridLocation,
            Func<ViewableArea, (int PosX, int PosY)> calculatePosition
        )
        {
            Location = location;
            GridLocation = gridLocation;
            CalculatePosition = calculatePosition;
        }

        public string Location { get; }
        public bool GridLocation { get; }
        public Func<ViewableArea, (int PosX, int PosY)> CalculatePosition { get; }

        private static (int PosX, int PosY) CorrectPostionForSize(
            ViewableArea viewableArea,
            (double widthSize, double heightSize) size,
            (int PosX, int PosY) position
        )
        {
            var posX = position.PosX;
            var posY = position.PosY;

            if (size.widthSize + StandardWidthPadding + posX > viewableArea.InnerWidth)
            {
                posX = viewableArea.InnerWidth - (int)size.widthSize - StandardWidthPadding;
            }

            if (size.heightSize + StandardHeightPadding + posY > viewableArea.InnerHeight)
            {
                posY = viewableArea.InnerHeight - (int)size.heightSize - StandardHeightPadding;
            }

            if (posX < 0)
            {
                posX = 0;
            }

            if (posY < 0)
            {
                posY = 0;
            }

            return (posX, posY);
        }

        public (int PosX, int PosY) Resolve(
            ViewableArea viewableArea,
            (double widthSize, double heightSize) size
        )
        {
            return CorrectPostionForSize(
                viewableArea,
                size,
                CalculatePosition(
                    viewableArea
                )
            );
        }

        public static implicit operator WindowLocationType(
            string location
        ) => new(
            location,
            false,
            viewableArea =>
            {
                if (location.Contains(
                    "x",
                    StringComparison.InvariantCultureIgnoreCase
                ))
                {
                    var splitLocation = location
                        .ToLowerInvariant()
                        .Split(
                            "x"
                        );
                    if (splitLocation.Length == 2)
                    {
                        var validPosX = int.TryParse(
                            splitLocation[0],
                            out var posX
                        );
                        var validPosY = int.TryParse(
                            splitLocation[1],
                            out var posY
                        );

                        if (validPosX && validPosY)
                        {
                            return (posX, posY);
                        }
                    }
                }
                return (0, 0);
            }
        );
    }
}
