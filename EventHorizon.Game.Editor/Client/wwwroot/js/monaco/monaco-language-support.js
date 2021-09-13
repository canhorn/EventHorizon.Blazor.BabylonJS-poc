(function () {
    window.addEventListener("load", function () {
        function createCSharpDependencyProposals(range) {
            // Services 
            // - ServerInfo ServerInfo { get; }
            // - IMediator Mediator { get; }
            // - IRandomNumberGenerator Random { get; }
            // - IDateTimeService DateTime { get; }
            // - I18nLookup I18n { get; }
            // - ObserverState ObserverState { get; }
            // - DataStore DataStore { get; }
            // Actions
            // - var ${1:localizedText} = I18n.Lookup("${2:default}", "${3:key}");
            // - var ${1:variableName} = data.Get<${2:Type}>("${3:key}");
            // - ${1:entityName}.GetProperty<${2:Type}>("${3:PropertyName}");
            // - ${1:entityName}.SetProperty("${2:PropertyName}", ${3:PropertyValue});
            // - await services.Mediator.Send(${1:Command});

            // TODO: Load in a list of all Command/Event/Queries supported by C# Scripts
            return [
                {
                    label: '"my-third-party-library"',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "Describe your library here",
                    insertText: '"${1:my-third-party-library}": "${2:1.2.3}"',
                    insertTextRules: monaco.languages.CompletionItemInsertTextRule.InsertAsSnippet,
                    range: range
                },
                // Services
                {
                    label: 'server-info',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The Server Info API.",
                    insertText: 'services.ServerInfo',
                    range: range
                },
                {
                    label: 'mediator',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The Mediator API.",
                    insertText: 'services.Mediator',
                    range: range
                },
                {
                    label: 'random',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The Random API.",
                    insertText: 'services.Random',
                    range: range
                },
                {
                    label: 'date-time',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The Date Time API.",
                    insertText: 'services.DateTime',
                    range: range
                },
                {
                    label: 'i18n',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The I18n API.",
                    insertText: 'services.I18n',
                    range: range
                },
                {
                    label: 'observer-state',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The Observer State API.",
                    insertText: 'services.ObserverState',
                    range: range
                },
                {
                    label: 'data-store',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The Data Store API.",
                    insertText: 'services.DataStore',
                    range: range
                },
                // Actions
                // - var ${1:localizedText} = I18n.Lookup("${2:default}", "${3:key}");
                {
                    label: 'translation',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "Lookup a localized value from Internalization.",
                    insertText: 'var ${1:localizedText} = I18n.Lookup("${2:default}", "${3:key}");',
                    insertTextRules: monaco.languages.CompletionItemInsertTextRule.InsertAsSnippet,
                    range: range
                },
                // - var ${1:variableName} = data.Get<${2:Type}>("${3:key}");
                {
                    label: 'get-data',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "Lookup a localized value from Internalization.",
                    insertText: 'var ${1:variableName} = data.Get<${2:Type}>("${3:key}");',
                    insertTextRules: monaco.languages.CompletionItemInsertTextRule.InsertAsSnippet,
                    range: range
                },
                // - ${1:entityName}.GetProperty<${2:Type}>("${3:PropertyName}");
                {
                    label: 'entity-get',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "Lookup a localized value from Internalization.",
                    insertText: '${1:entityName}.GetProperty<${2:Type}>("${3:PropertyName}");',
                    insertTextRules: monaco.languages.CompletionItemInsertTextRule.InsertAsSnippet,
                    range: range
                },
                // - ${1:entityName}.SetProperty("${2:PropertyName}", ${3:PropertyValue});
                {
                    label: 'entity-set',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "Lookup a localized value from Internalization.",
                    insertText: '${1:entityName}.SetProperty("${2:PropertyName}", ${3:PropertyValue});',
                    insertTextRules: monaco.languages.CompletionItemInsertTextRule.InsertAsSnippet,
                    range: range
                },
                // - await services.Mediator.Send(${1:Command});
                {
                    label: 'send',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "Lookup a localized value from Internalization.",
                    insertText: 'await services.Mediator.Send(${1:Command});',
                    insertTextRules: monaco.languages.CompletionItemInsertTextRule.InsertAsSnippet,
                    range: range
                },
                // Commands (TESTING)
                {
                    label: 'SendUserCommand',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The Data Store API.",
                    insertText: 'SendUserCommand',
                    range: range
                },
                {
                    label: 'QueryForAllAgents',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The Data Store API.",
                    insertText: 'QueryForAllAgents',
                    range: range
                },
                {
                    label: 'UserStateWasUpdatedEvent',
                    kind: monaco.languages.CompletionItemKind.Function,
                    documentation: "The Data Store API.",
                    insertText: 'UserStateWasUpdatedEvent',
                    range: range
                },
            ];
        }

        monaco.languages.registerCompletionItemProvider('csharp', {
            provideCompletionItems: function (model, position) {
                console.log({model, position})
                // find out if we are completing a property in the 'dependencies' object.
                //var textUntilPosition = model.getValueInRange({ startLineNumber: 1, startColumn: 1, endLineNumber: position.lineNumber, endColumn: position.column });
                //var match = textUntilPosition.match(/"Run"\(\s*("[^"]*"\s*:\s*"[^"]*"\s*,\s*)*([^"]*)?$/);
                //if (!match) {
                //    return { suggestions: [] };
                //}
                var word = model.getWordUntilPosition(position);
                var range = {
                    startLineNumber: position.lineNumber,
                    endLineNumber: position.lineNumber,
                    startColumn: word.startColumn,
                    endColumn: word.endColumn
                };
                return {
                    suggestions: createCSharpDependencyProposals(range)
                };
            }
        });
    })
})();