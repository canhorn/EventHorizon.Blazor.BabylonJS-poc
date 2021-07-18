export function focusElement(element) {
    if (element) {
        element.focus();
    }
}

export function focusElementBySelector(elementSelector) {
    focusElement(document.querySelector(elementSelector));
}
