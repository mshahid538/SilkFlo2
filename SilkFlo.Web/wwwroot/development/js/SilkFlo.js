$(function () {
    window.$('#chkShowDeleted').change(function () {
        if (window.$(this).prop('checked')) {
            document.cookie = 'ShowDeleted=true';
        }
        else {
            document.cookie = 'ShowDeleted=false';
        }
    });
});

$(function () {
    window.$('#chkShowDeleted').change(function () {
        if (window.$(this).prop('checked')) {
            document.cookie = 'ShowDeleted=true';
        }
        else {
            document.cookie = 'ShowDeleted=false';
        }
    });
});

var SilkFlo = {

    CheckCookie: function (name) {
        const cookie = SilkFlo.GetCookie(name);
        if (cookie === '') {
            return false;
        }
        else {
            return true;
        }
    },

    DeleteCookie: function (name) {
        document.cookie = `${name}= ; expires = Thu, 01 Jan 1970 00:00:00 GMT;path=/;SameSite=Lax;`;
    },

    GetCookie: function (name) {
        name = `${name}=`;
        const decodedCookie = decodeURIComponent(document.cookie);

        const ca = decodedCookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return '';
    },

    SetCookie: function (name, value, expireDays) {

        const str = `${name}=${value};path=/; SameSite=Lax;`;

        if (expireDays === null) {
            document.cookie = str;
        }
        else {
            const date = new Date();
            date.setTime(date.getTime() + (expireDays * 24 * 60 * 60 * 1000));

            const expires = ` expires=${date.toUTCString()}`;
            //document.cookie = name + "=" + value + ";" + expires + ";path=/";
            document.cookie = str + expires;
        }
    },

    ClearLog: function () {

        if (confirm('Are you want to delete all the logs?')) {
            const url = '/api/log/clearAll';

            const http = new XMLHttpRequest();
            http.open('DELETE', url, true);
            http.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
            http.onreadystatechange = function () {
                return function () {
                    if (http.readyState === XMLHttpRequest.DONE
                        && http.status === 200) {

                        window.location.href = '../Administration/log';
                    }
                };
            }(this);
            http.send();
        }
    },


    ReplaceImgWithSVG: function (parent) {
        let collection;

        if (parent)
            collection = $(parent).find('img[src$=".svg"]');
        else
            collection = window.$('img[src$=".svg"]');


        collection.each(function () {
            const $img = jQuery(this);

            const ignore = $img.attr('ignore');

            if (ignore)
                return;


            const imgURL = $img.attr('src');
            const attributes = $img.prop("attributes");

            window.$.get(imgURL,
                function (data) {
                    // Get the SVG tag, ignore the rest
                    let $svg = jQuery(data).find('svg');

                    // Remove any invalid XML tags
                    $svg = $svg.removeAttr('xmlns:a');

                    // Loop through IMG attributes and apply on SVG
                    window.$.each(attributes,
                        function () {
                            $svg.attr(this.name,
                                this.value);
                        });

                    // Replace IMG with SVG
                    $img.replaceWith($svg);
                }, 'xml');
        });
    },

    UpdateInnerHTMLById: function (
        innerHtml,
        targetElementId,
        focusElementId) {
        const logPrefix = 'SilkFlo.UpdateInnerHTMLById: ';

        // Guard Clause
        if (!targetElementId) {
            console.log(logPrefix + 'The targetElementId parameter is missing.');
            return;
        }

        const element = document.getElementById(targetElementId);
        if (!element) {
            console.log(logPrefix + 'An element with id of "' + targetElementId + '" could not be found.');
            return;
        }

        SilkFlo.UpdateInnerHTML(innerHtml, element, focusElementId);
    },


    // SilkFlo.UpdateInnerHTML
    UpdateInnerHTML: function (
        innerHtml,
        targetElement,
        focusElementId) {
        // Guard Clause
        if (!targetElement) {
            const logPrefix = 'SilkFlo.UpdateInnerHTML: ';
            console.log(logPrefix + 'The targetElement parameter is missing.');
            return;
        }

        // Update the targetElement
        if (targetElement.innerHTML !== innerHtml) {
            targetElement.innerHTML = innerHtml;
            if (focusElementId) {
                setTimeout(function () {
                    const focusElement = document.getElementById(focusElementId);
                    if (focusElement) {
                        focusElement.focus();
                    }
                }, 0);
            }

            HotSpot.Get(targetElement);
            Delaney.UI.DatePicker.CreateDatePickers(targetElement);
            SilkFlo.ReplaceImgWithSVG(targetElement);
            Delaney.UI.Grid.Create(targetElement);
            SilkFlo.TabBar.SetUp(targetElement);
            SilkFlo.Analytics.AssignClickEvents(targetElement);
        }
    },

    TextArea_OnInput: function (source,
        targetName) {
        const span = document.getElementById(targetName);
        span.innerHTML = source.value;
    },

    TextArea_Resize: function (source, targetElementName) {
        let resizeInterval = null;

        // The handler function
        const onResize = function () {
            const target = document.getElementById(targetElementName);
            target.value = source.outerHeight();
        };

        // This provides a "real-time" (actually 15 fps)
        // event, while resizing.
        // Unfortunately, mousedown is not fired on Chrome when
        // clicking on the resize area, so the real-time effect
        // does not work under Chrome.
        source.on('mousedown',
            function (e) {
                resizeInterval = setInterval(onResize, 1000 / 15);
            });

        // The mouseup event stops the interval,
        // then call the resize event one last time.
        // We listen for the whole window because in some cases,
        // the mouse pointer may be on the outside of the textarea.
        $(window).on('mouseup',
            function (e) {
                if (resizeInterval) {
                    clearInterval(resizeInterval);
                }

                onResize();
            });
    },



    // SilkFlo.Compare
    Compare: function (
        parentId,
        sourceName,
        targetName,
        invalidName,
        errorMessage,
        ignoreCase) {
        const logPrefix = 'SilkFlo.Compare: ';


        // Guard Clause
        if (!parentId) {
            console.log(`${logPrefix}parentId parameter is missing.`);
            return false;
        }

        // Guard Clause
        if (!sourceName) {
            console.log(`${logPrefix}sourceName parameter is missing.`);
            return false;
        }

        // Guard Clause
        if (!targetName) {
            console.log(`${logPrefix}targetName parameter is missing.`);
            return false;
        }

        // Guard Clause
        if (!invalidName) {
            console.log(`${logPrefix}invalidName parameter is missing.`);
            return false;
        }


        const parent = document.getElementById(parentId);

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}Element with id ${parentId} missing`);
            return false;
        }


        const sourceElement = parent.querySelector(`[name="${sourceName}"]`);

        // Guard Clause
        if (!sourceElement) {
            console.log(`${logPrefix}Element with name ${sourceName} missing`);
            return false;
        }


        const targetElement = parent.querySelector(`[name="${targetName}"]`);

        // Guard Clause
        if (!targetElement) {
            console.log(`${logPrefix}Element with name ${targetName} missing`);
            return false;
        }


        const invalidElement = parent.querySelector(`[name="${invalidName}"]`);

        // Guard Clause
        if (!invalidElement) {
            console.log(`${logPrefix}Element with name ${invalidName} missing`);
            return false;
        }


        let source = sourceElement.value;
        let target = targetElement.value;

        if (ignoreCase) {
            source = source.toLowerCase();
            target = target.toLowerCase();
        }

        if (!target) {
            sourceElement.classList.remove('is-invalid');
            return true;
        }
        else {
            if (source === target) {
                sourceElement.classList.remove('is-invalid');
                return true;
            }
            else {
                sourceElement.classList.add('is-invalid');
                invalidElement.innerHTML = errorMessage;
                return false;
            }
        }
    },


    // SilkFlo.IsEmailAlreadyUsed
    IsEmailAlreadyUsed: function (
        email,
        callbackFunction) {
        const http = new XMLHttpRequest();
        const url = '/api/account/IsEmailAlreadyUsed';
        const data = email;
        http.open(
            'POST',
            url,
            true); // false for synchronous request

        http.onreadystatechange = function () {
            return function () {
                if (http.readyState === XMLHttpRequest.DONE
                    && http.status === 200) {
                    const s = http.responseText;

                    if (s.length > 0
                        && callbackFunction)
                        callbackFunction(s);
                }
            };
        }(this);
        http.send(data);
    },


    // SilkFlo.IsInRange
    IsInRange: function
        (element,
            min,
            max) {


        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.IsInRange: ';
            console.log(logPrefix + 'element parameter is missing.');
            return false;
        }


        const invalidElement = document.getElementById(element.id + 'Invalid');


        const value = element.value;

        if (value.length === 0) {
            element.classList.remove('is-invalid');
            return true;
        }


        let isInvalidMin = false;
        let isInvalidMax = false;

        // Check Min
        if (min !== undefined
            && value < min)
            isInvalidMin = true;


        // Check Max
        if (max !== undefined
            && value > max)
            isInvalidMax = true;



        if (isInvalidMin || isInvalidMax) {
            // Error message
            if (invalidElement) {
                let text = 'The value must be ';

                if (min !== undefined && max !== undefined)
                    text += `between ${min} and ${max}.`;
                else if (max === undefined)
                    text += `greater than ${min}`;
                else if (min === undefined)
                    text += `less than ${min}`;

                invalidElement.innerText = text + '.';
            }

            element.classList.add('is-invalid');
            return false;
        }



        element.classList.remove('is-invalid');
        return true;
    },


    IsRequired: function
        (element,
            min,
            max,
            errorMessage,
            returnTrue = true) {
        const logPrefix = 'SilkFlo.IsRequired: ';


        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter is missing.`);
            return returnTrue;
        }


        let parent = element.parentElement;
        let parent2 = null;
        if (parent) {
            parent2 = parent.parentElement;

            if (!parent2.hasAttribute('SilkFlo-suffix-container'))
                parent2 = null;
        }


        let invalidElement = document.getElementById(`${element.id}_InvalidFeedback`);

        if (!invalidElement)
            invalidElement = parent.querySelector(`[name="${element.name}_InvalidFeedback"]`);;

        if (!invalidElement) {
            const parentInvalid = parent.parentElement;
            invalidElement = parentInvalid.querySelector(`[name="${element.name}_InvalidFeedback"]`);;
        }

        if (!invalidElement) {
            const parentInvalid = parent.parentElement.parentElement;
            invalidElement = parentInvalid.querySelector(`[name="${element.name}_InvalidFeedback"]`);;
        }


        const value = element.value;


        let prefixId = element.id;
        if (!prefixId) {
            prefixId = element.name + '_Prefix';
        }
        const prefixElement = document.getElementById(prefixId);


        let suffixId = element.id;
        if (!suffixId) {
            suffixId = element.name + '_Suffix';
        }
        const suffixElement = document.getElementById(suffixId);



        if (value === undefined || value.length === 0) {

            // Error message
            if (invalidElement) {
                if (errorMessage) {
                    invalidElement.innerText = errorMessage;
                    invalidElement.style.display = 'block';
                }
                else {
                    invalidElement.style.display = '';
                }
            }

            element.classList.add('is-invalid');

            if (parent2) {
                parent2.classList.add('is-invalid');
            }


            if (parent.classList.contains('form-floating')) {
                if (prefixElement)
                    prefixElement.style.alignItems = 'center';

                if (suffixElement)
                    suffixElement.style.alignItems = 'center';
            }

            return returnTrue;
        }
        else {
            if (invalidElement) {
                invalidElement.style.display = '';
            }


            if (parent.classList.contains('form-floating')) {
                if (prefixElement)
                    prefixElement.style.alignItems = 'end';

                if (suffixElement)
                    suffixElement.style.alignItems = 'end';
            }


            if (min && value < min
                || max && value > max) {
                // Error message
                if (invalidElement)  // if (!s)
                {
                    let text = 'The value must be ';

                    let errorText = '';
                    if (errorMessage)
                        errorText = errorMessage;

                    text = `${errorText}. ${text}`;

                    if (min && max)
                        text += `between ${min} and ${max}`;
                    else if (!max)
                        text += `greater than ${min}`;
                    else if (!min)
                        text += `less than ${min}`;

                    invalidElement.innerHTML = text;
                }

                element.classList.add('is-invalid');


                if (parent2)
                    parent2.classList.add('is-invalid');

                return returnTrue;
            }
        }


        element.classList.remove('is-invalid');

        if (parent2)
            parent2.classList.remove('is-invalid');

        return true;
    },


    FormatToolTip: function () {
        window.$('[data-bs-target="tooltip"]').tooltip();
    },



    // Get a collection of <option> elements. i.e:
    //  <option value=""><Empty></option>
    //  <option value="0">Value 0</option>
    //  <option value="1">Value 1</option>
    UpdateList: function (
        id,
        apiUrl) {
        const logPrefix = 'SilkFlo.UpdateList: ';


        // Guard Clauses
        if (!id) {
            console.log(logPrefix + 'id parameter is missing.');
            return;
        }

        // Guard Clauses
        if (!apiUrl) {
            console.log(logPrefix + 'apiUrl parameter is missing.');
            return;
        }


        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(logPrefix + 'Could not find an element with id "' + id + '".');
            return;
        }

        const value = element.value;

        // Get the Data
        SilkFlo.DataAccess.UpdateElementById(
            apiUrl,
            null,
            id,
            '',
            'GET',
            function () {
                SilkFlo.SelectListItem(
                    id,
                    value);
            });
    },


    // Select an item in a list.
    // listId - This is the id for the list.
    // id - This is the id for the item for selection
    SelectListItem: function (listId,
        id) {
        const logPrefix = 'SilkFlo.UpdateList: ';


        // Guard Clauses
        if (!listId) {
            console.log(logPrefix + 'listId parameter is missing.');
            return;
        }


        // Guard Clauses
        if (!id) {
            console.log(logPrefix + 'id parameter is missing.');
            return;
        }




        //Get the select element by it's unique ID.
        const element = document.getElementById(listId);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Could not find an element with id "${id}".`);
            return;
        }



        //Get the list items.
        const listItems = element.options;

        //Loop through these list items using a for loop.
        const length = listItems.length;
        for (let i = 0; i < length; i++) {
            const item = listItems[i];

            //If the item of value is equal to the item we want to select.
            if (item.value === id) {
                //Select the item and break out of the for loop.
                element.selectedIndex = i;
                break;
            }
        }
    },




    GetElementInParentId: function (parentId, elementName) {
        const logPrefix = 'SilkFlo.GetElementInParentId: ';

        // Guard Clause
        if (!parentId) {
            console.log(logPrefix + 'The parentId parameter is missing.');
            return null;
        }

        // Guard Clause
        if (!elementName) {
            console.log(logPrefix + 'The elementName parameter is missing.');
            return null;
        }


        const parentElement = document.getElementById(parentId);

        // Guard Clause
        if (!parentElement) {
            console.log(logPrefix + 'The parent element with id ' + parentId + ' was not found.');
            return null;
        }


        return SilkFlo.GetElementInParent(parentElement, elementName);
    },



    GetElementInParent: function (parentElement, elementName) {
        const logPrefix = 'SilkFlo.GetElementInParent: ';

        // Guard Clause
        if (!parentElement) {
            console.log(logPrefix + 'The parentElement parameter is missing.');
            return null;
        }


        // Guard Clause
        if (!elementName) {
            console.log(logPrefix + 'The elementName parameter is missing.');
            return null;
        }


        // Get the element
        const element = parentElement.querySelector(`[name="${elementName}"]`);


        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}The element was not found with name ${elementName}.`);
            return null;
        }

        return element;
    },


    CharacterCount: function (element) {
        const logPrefix = 'SilkFlo.CharacterCount: ';

        // Guard Clauses
        if (!element) {
            console.log(logPrefix + 'element parameter is missing.');
            return;
        }

        const parentElement = element.parentElement;

        const characterCounter = SilkFlo.GetElementInParent(parentElement, 'characterCounter');


        // Guard Clauses
        if (!characterCounter) {
            console.log(logPrefix + 'Could not find element with the name "characterCounter" within the parent.');
            return;
        }

        const current = SilkFlo.GetElementInParent(characterCounter, 'current');

        // Guard Clauses
        if (!current) {
            console.log(logPrefix + 'Could not find element with the name "current" within the parent with name "characterCounter".');
            return;
        }


        const characterCount = element.value.length;
        current.innerHTML = characterCount;
    },


    ExecuteFunctionByName: function (functionName, context /*, args */) {
        const logPrefix = 'DelaneysSlider.ExecuteFunctionByName';

        // Guard Clause
        if (!functionName) {
            //throw 'function name not specified';
            console.log(`${logPrefix}functionName parameter missing`);
            return null;
        }


        // Guard Clause
        if (typeof eval(functionName) !== 'function') {
            //throw functionName + ' is not a function';
            //console.log(`${logPrefix}functionName is not a function`);
            return null;
        }



        // Get the arguments
        let args;
        if (!context) {
            context = window;
        }
        else {
            if (typeof context === 'object'
                && context instanceof Array === false) {
                // Guard Clause
                if (typeof context[functionName] !== 'function') {
                    throw context + '.' + functionName + ' is not a function';
                }

                args = Array.prototype.slice.call(arguments, 2);
            }
            else {
                args = Array.prototype.slice.call(arguments, 1);
                context = window;
            }
        }





        // Get the context
        const namespaces = functionName.split('.');
        const fn = namespaces.pop();
        for (let i = 0; i < namespaces.length; i++) {
            context = context[namespaces[i]];
        }


        if (args) {
            for (let i = 0; i < args[0].length; i++) {
                let val = args[0][i];
                val = val.trim();

                if (val.length > 0) {
                    if (val.substring(
                        0,
                        1) ===
                        '"' ||
                        val.substring(
                            0,
                            1) ===
                        "'") {
                        val = val.substring(
                            1,
                            val.length);
                    }
                }

                if (val.length > 0) {
                    if (val.substring(
                        val.length - 1,
                        val.length) ===
                        '"' ||
                        val.substring(
                            val.length - 1,
                            val.length) ===
                        "'") {
                        val = val.substring(
                            0,
                            val.length - 1);
                    }
                }

                args[0][i] = val;
            }
        }


        if (args) {
            return context[fn].apply(context, args[0]);
        }

        return context[fn].apply(context);
    },


    // SilkFlo.Redirect
    Redirect: function (url) {
        // Guard Clause
        if (!url) {
            const logPrefix = 'SilkFlo.Redirect: ';
            console.log(`${logPrefix}url parameter missing`);
            return;
        }

        window.open(url);
    },

    // SilkFlo.SpinnerSmall
    SpinnerSmall: '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" style="margin-right: 0.5rem;"></span>',


    // SilkFlo.DisableButtonById
    DisableButtonById: function (
        id) {
        const logPrefix = 'SilkFlo.DisableButtonById: ';

        // Guard Clause
        if (!id) {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }


        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.DisableButton(element);
    },

    // SilkFlo.DisableButtonByName
    DisableButtonByName: function (
        name,
        parent) {
        const logPrefix = 'SilkFlo.DisableButtonByName: ';

        // Guard Clause
        if (!name) {
            console.log(`${logPrefix}name parameter missing`);
            return;
        }

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.DisableButton(element);
    },

    // SilkFlo.DisableButton
    DisableButton: function (button) {
        // Guard Clause
        if (!button) {
            const logPrefix = 'SilkFlo.DisableButton: ';
            console.log(`${logPrefix}button parameter missing`);
            return;
        }

        button.setAttribute('disabled', '');
        button.innerHTML = SilkFlo.SpinnerSmall + button.innerHTML;
    },





    // SilkFlo.EnableButtonById
    EnableButtonById: function (
        id) {
        const logPrefix = 'SilkFlo.EnableButtonById: ';

        // Guard Clause
        if (!id) {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }


        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.EnableButton(element);
    },

    // SilkFlo.EnableButtonByName
    EnableButtonByName: function (
        name,
        parent) {
        const logPrefix = 'SilkFlo.EnableButtonByName: ';

        // Guard Clause
        if (!name) {
            console.log(`${logPrefix}name parameter missing`);
            return;
        }

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.EnableButton(element);
    },

    // SilkFlo.EnableButton
    EnableButton: function (button) {
        // Guard Clause
        if (!button) {
            const logPrefix = 'SilkFlo.EnableButton: ';
            console.log(`${logPrefix}button parameter missing`);
            return;
        }

        button.removeAttribute('disabled');
        SilkFlo.RemoveSpinner(button);
    },






    // SilkFlo.DisableButtonById
    RemoveSpinnerById: function (
        id) {
        const logPrefix = 'SilkFlo.RemoveSpinnerById: ';

        // Guard Clause
        if (!id) {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }


        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.RemoveSpinner(element);
    },

    // SilkFlo.DisableButtonByName
    RemoveSpinnerByName: function (
        name,
        parent) {
        const logPrefix = 'SilkFlo.RemoveSpinnerByName: ';

        // Guard Clause
        if (!name) {
            console.log(`${logPrefix}name parameter missing`);
            return;
        }

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.RemoveSpinner(element);
    },

    RemoveSpinner: function (button) {
        // Guard Clause
        if (!button) {
            const logPrefix = 'SilkFlo.RemoveSpinner: ';
            console.log(`${logPrefix}button parameter missing`);
            return;
        }

        button.innerHTML = button.innerHTML.replace(
            SilkFlo.SpinnerSmall,
            '');
    },


    // SilkFlo.AddSpinner
    AddSpinner: function (button) {
        // Guard Clause
        if (!button) {
            const logPrefix = 'SilkFlo.AddSpinner: ';
            console.log(`${logPrefix}button parameter missing`);
            return;
        }

        button.innerHTML = SilkFlo.SpinnerSmall + button.innerHTML;
    }
};

function IsNumeric(value) {
    let isNumeric = true;
    if (isNaN(value)) {
        isNumeric = false;
    }
    else {
        if (value === null
            || value === undefined
            || typeof value === 'boolean'
            || typeof value === 'function'
            || typeof value === 'object'
            || value.length === 0) {
            isNumeric = false;
        }
    }

    return isNumeric;
}




// Usage:
// onkeydown="KeyPressPositiveIntegerOnly(event)"
function KeyPressPositiveIntegerOnly(event, followOnFunction) {
    if (event.which === 189) // -
        event.preventDefault();

    else if (event.which === 69) // e
        event.preventDefault();

    else if (event.which === 190) // .
        event.preventDefault();


    if ((event.keyCode < 48 || event.keyCode > 57)
        && event.keyCode !== 46
        && event.keyCode !== 39
        && event.keyCode !== 37
        && event.keyCode !== 36
        && event.keyCode !== 35
        && event.keyCode !== 9
        && event.keyCode !== 8)
        event.preventDefault();

    if (followOnFunction)
        followOnFunction(event);
}

// Usage:
// onkeydown="KeyPressPositiveDecimalOnly(event)"
function KeyPressPositiveDecimalOnly(event, followOnFunction) {
    if (event.which === 189) // -
        event.preventDefault();

    else if (event.which === 69) // e
        event.preventDefault();

    // console.log('event.keyCode = ' + event.keyCode);

    if ((event.keyCode < 48 || event.keyCode > 57)
        && event.keyCode !== 190 // .
        && event.keyCode !== 46  // delete
        && event.keyCode !== 39  // right arrow
        && event.keyCode !== 37  // left arrow
        && event.keyCode !== 36  // home
        && event.keyCode !== 35  // end
        && event.keyCode !== 27  // escape
        && event.keyCode !== 13  // enter
        && event.keyCode !== 9   // tab
        && event.keyCode !== 8)  // backspace
        event.preventDefault();

    if (followOnFunction)
        followOnFunction(event);
}

function Redirect(url) {

    // Guard Clause
    if (!url) {
        const logPrefix = 'Redirect: ';
        console.log(`${logPrefix}url parameter missing`);
        return;
    }
    console.log(url);
    window.location.href = url;
}