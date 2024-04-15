namespace EventHorizon.Game.Editor.Client.Wizard;

using System.Collections.Generic;
using System.Text.Json;
using Bunit;
using EventHorizon.Game.Client.Engine.Input.Model;
using EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;
using EventHorizon.Game.Editor.Client.Shared.Properties;
using EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;
using EventHorizon.Zone.Systems.Wizard.Model;
using FluentAssertions;
using Xunit;

public class WizardStepFormInputTests
{
    [Fact]
    public void ParseMapOfPlayerInputItemsFromWizardData()
    {
        // Given
        var data = new WizardData
        {
            ["playerInput:keyInputMap:w:key"] = "w",
            ["playerInput:keyInputMap:w:type"] = "PlayerMove",
            ["playerInput:keyInputMap:w:@Comment.pressed"] = "MoveDirection.Forward",
            ["playerInput:keyInputMap:w:pressed"] = "4",
            ["playerInput:keyInputMap:w:@Comment.released"] = "MoveDirection.Stop",
            ["playerInput:keyInputMap:w:released"] = "0",
            ["playerInput:keyInputMap:a:key"] = "a",
            ["playerInput:keyInputMap:a:type"] = "PlayerMove",
            ["playerInput:keyInputMap:a:@Comment.pressed"] = "MoveDirection.Left",
            ["playerInput:keyInputMap:a:pressed"] = "1",
            ["playerInput:keyInputMap:a:@Comment.released"] = "MoveDirection.Stop",
            ["playerInput:keyInputMap:a:released"] = "0",
            ["playerInput:keyInputMap:s:key"] = "s",
            ["playerInput:keyInputMap:s:type"] = "PlayerMove",
            ["playerInput:keyInputMap:s:@Comment.pressed"] = "MoveDirection.Backwards",
            ["playerInput:keyInputMap:s:pressed"] = "3",
            ["playerInput:keyInputMap:s:@Comment.released"] = "MoveDirection.Stop",
            ["playerInput:keyInputMap:s:released"] = "0",
            ["playerInput:keyInputMap:d:key"] = "d",
            ["playerInput:keyInputMap:d:type"] = "PlayerMove",
            ["playerInput:keyInputMap:d:@Comment.pressed"] = "MoveDirection.Right",
            ["playerInput:keyInputMap:d:pressed"] = "2",
            ["playerInput:keyInputMap:d:@Comment.released"] = "MoveDirection.Stop",
            ["playerInput:keyInputMap:d:released"] = "0",
            ["playerInput:keyInputMap:1:key"] = "1",
            ["playerInput:keyInputMap:1:type"] = "SetActiveCamera",
            ["playerInput:keyInputMap:1:camera"] = "player_universal_camera",
            ["playerInput:keyInputMap:2:key"] = "2",
            ["playerInput:keyInputMap:2:type"] = "SetActiveCamera",
            ["playerInput:keyInputMap:2:camera"] = "player_follow_camera",
            ["playerInput:keyInputMap:f:key"] = "f",
            ["playerInput:keyInputMap:f:type"] = "RunInteraction",
        };

        var expectedList = new Dictionary<string, InputKeyMapControlModel.ControlKeyInput>
        {
            ["w"] = new()
            {
                Key = "w",
                Type = "PlayerMove",
                Pressed = MoveDirection.Forward, // "4",
                Released = MoveDirection.Stationary, // "0",
                // CommentPressed = "MoveDirection.Forward",
                // CommentReleased = "MoveDirection.Stop",
            },
            ["a"] = new()
            {
                Key = "a",
                Type = "PlayerMove",
                Pressed = MoveDirection.Left, // "1",
                Released = MoveDirection.Stationary, // "0",
                // CommentPressed = "MoveDirection.Left",
                // CommentReleased = "MoveDirection.Stop",
            },
            ["s"] = new()
            {
                Key = "s",
                Type = "PlayerMove",
                Pressed = MoveDirection.Backwards, // "3",
                Released = MoveDirection.Stationary, // "0",
                // CommentPressed = "MoveDirection.Backwards",
                // CommentReleased = "MoveDirection.Stop",
            },
            ["d"] = new()
            {
                Key = "d",
                Type = "PlayerMove",
                Pressed = MoveDirection.Right, // "2",
                Released = MoveDirection.Stationary, // "0",
                // CommentPressed = "MoveDirection.Right",
                // CommentReleased = "MoveDirection.Stop",
            },
            ["1"] = new()
            {
                Key = "1",
                Type = "SetActiveCamera",
                Camera = "player_universal_camera",
            },
            ["2"] = new()
            {
                Key = "2",
                Type = "SetActiveCamera",
                Camera = "player_follow_camera",
            },
            ["f"] = new() { Key = "f", Type = "RunInteraction", },
        };
        var expected = JsonSerializer.Serialize(expectedList, JsonExtensions.DEFAULT_OPTIONS);

        // When
        var actual = WizardStepFormInputBase.GetInputKeyMap("playerInput:keyInputMap", data);

        // Then
        actual.Should().Be(expected);
    }

    [Fact]
    public void FlattenPlayerInputItemMapIntoWizardData()
    {
        // Given
        var inputKeyMap = new Dictionary<string, InputKeyMapControlModel.ControlKeyInput>
        {
            ["w"] = new()
            {
                Key = "w",
                Type = "PlayerMove",
                Pressed = MoveDirection.Forward, // "4",
                Released = MoveDirection.Stationary, // "0",
                // CommentPressed = "MoveDirection.Forward",
                // CommentReleased = "MoveDirection.Stop",
            },
            ["a"] = new()
            {
                Key = "a",
                Type = "PlayerMove",
                Pressed = MoveDirection.Left, // "1",
                Released = MoveDirection.Stationary, // "0",
                // CommentPressed = "MoveDirection.Left",
                // CommentReleased = "MoveDirection.Stop",
            },
            ["s"] = new()
            {
                Key = "s",
                Type = "PlayerMove",
                Pressed = MoveDirection.Backwards, // "3",
                Released = MoveDirection.Stationary, // "0",
                // CommentPressed = "MoveDirection.Backwards",
                // CommentReleased = "MoveDirection.Stop",
            },
            ["d"] = new()
            {
                Key = "d",
                Type = "PlayerMove",
                Pressed = MoveDirection.Right, // "2",
                Released = MoveDirection.Stationary, // "0",
                // CommentPressed = "MoveDirection.Right",
                // CommentReleased = "MoveDirection.Stop",
            },
            ["1"] = new()
            {
                Key = "1",
                Type = "SetActiveCamera",
                Camera = "player_universal_camera",
            },
            ["2"] = new()
            {
                Key = "2",
                Type = "SetActiveCamera",
                Camera = "player_follow_camera",
            },
            ["f"] = new() { Key = "f", Type = "RunInteraction", },
        };

        var wizardData = new WizardData();
        var expected = new WizardData
        {
            ["playerInput:keyInputMap:w:key"] = "w",
            ["playerInput:keyInputMap:w:type"] = "PlayerMove",
            ["playerInput:keyInputMap:w:pressed"] = "4",
            ["playerInput:keyInputMap:w:released"] = "0",
            ["playerInput:keyInputMap:a:key"] = "a",
            ["playerInput:keyInputMap:a:type"] = "PlayerMove",
            ["playerInput:keyInputMap:a:pressed"] = "1",
            ["playerInput:keyInputMap:a:released"] = "0",
            ["playerInput:keyInputMap:s:key"] = "s",
            ["playerInput:keyInputMap:s:type"] = "PlayerMove",
            ["playerInput:keyInputMap:s:pressed"] = "3",
            ["playerInput:keyInputMap:s:released"] = "0",
            ["playerInput:keyInputMap:d:key"] = "d",
            ["playerInput:keyInputMap:d:type"] = "PlayerMove",
            ["playerInput:keyInputMap:d:pressed"] = "2",
            ["playerInput:keyInputMap:d:released"] = "0",
            ["playerInput:keyInputMap:1:key"] = "1",
            ["playerInput:keyInputMap:1:type"] = "SetActiveCamera",
            ["playerInput:keyInputMap:1:camera"] = "player_universal_camera",
            ["playerInput:keyInputMap:2:key"] = "2",
            ["playerInput:keyInputMap:2:type"] = "SetActiveCamera",
            ["playerInput:keyInputMap:2:camera"] = "player_follow_camera",
            ["playerInput:keyInputMap:f:key"] = "f",
            ["playerInput:keyInputMap:f:type"] = "RunInteraction",
        };

        // When
        var actual = WizardStepFormInputBase.FlattenInputKeyMapIntoData(
            wizardData,
            "playerInput:keyInputMap",
            inputKeyMap
        );

        // Then
        actual.Should().BeEquivalentTo(expected);
    }
}
