if (!SilkFlo)
    SilkFlo = {};

SilkFlo.DataAccess = {
    AttributeName: {
        Url: 'silkflo-url',
        Json: 'silkflo-json',
        IncludePath: 'silkflo-url-include-path',
        Callback: 'silkflo-callback',
        LoadOnce: 'silkflo-loadOnce',
        GetManually: 'silkflo-getManually'
    },

    ProductionHostName: 'app.silkflo.com',


    // SilkFlo.DataAccess.GetAttributes
    GetAttributes: function (element)
    {
        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.DataAccess.GetAttributes: ';
            console.log(`${logPrefix}element parameter missing`);
            return null;
        }

        if (element.SilkFlo)
            return element.SilkFlo;
        else
            element.SilkFlo = {};


        const deleteAttributes = this.ProductionHostName.toLowerCase() === location.hostname.toLowerCase();


        // Get IncludePath
        if (element.hasAttribute ( SilkFlo.DataAccess.AttributeName.IncludePath ))
        {
            element.SilkFlo.IncludePath = element.getAttribute(SilkFlo.DataAccess.AttributeName.IncludePath);

            if (deleteAttributes)
                element.removeAttribute(SilkFlo.DataAccess.AttributeName.IncludePath);
        }

        // Get Url
        if (element.hasAttribute ( SilkFlo.DataAccess.AttributeName.Url ))
        {
            const url = element.getAttribute(SilkFlo.DataAccess.AttributeName.Url);
            if (url)
            {
                element.SilkFlo.Url = url;

                if (deleteAttributes)
                    element.setAttribute (
                        SilkFlo.DataAccess.AttributeName.Url,
                        '' );
            }
        }

        // Get JSON
        if (element.hasAttribute(SilkFlo.DataAccess.AttributeName.Json)) {
            const json = element.getAttribute(SilkFlo.DataAccess.AttributeName.Json);
            if (json) {
                element.SilkFlo.Json = json;

                if (deleteAttributes)
                    element.setAttribute(
                        SilkFlo.DataAccess.AttributeName.Json,
                        '');
            }
        }


        // Get Callback
        if (element.hasAttribute ( SilkFlo.DataAccess.AttributeName.Callback ))
        {
            element.SilkFlo.Callback = element.getAttribute(SilkFlo.DataAccess.AttributeName.Callback);

            if (deleteAttributes)
                element.removeAttribute(SilkFlo.DataAccess.AttributeName.Callback);
        }


        // Get LoadOnce
        if (element.hasAttribute(SilkFlo.DataAccess.AttributeName.LoadOnce))
        {
            element.SilkFlo.LoadOnce = true;

            if (deleteAttributes)
                element.removeAttribute(SilkFlo.DataAccess.AttributeName.LoadOnce);
        }
        else
            element.SilkFlo.LoadOnce = false;;


        return element.SilkFlo;
    },

    // <summary>
    //   Update the contents of an HTML element using a string from the server.
    //   Example apiUrl: "./api/blog/...";
    //   <parameters>
    //     <parameter>
    //        apiUrl - The url used to get the element.
    //     <parameter>
    //     <parameter>
    //        data - .send(data) is used to not null.
    //     <parameter>
    //     <parameter>
    //        targetElementId - The target element id whose content will be updated.
    //     <parameter>
    //     <parameter>
    //        displayUrl - The URL displayed in the browsers address text box.
    //     <parameter>
    //     <parameter>
    //        verb - GET|POST etc. Default = 'GET'.
    //     <parameter>
    //     <parameter>
    //        callbackFunction - The callback function that will run after that update is completed.
    //     <parameter>
    //     <parameter>
    //        contentType - Default = 'application/x-www-form-urlencoded'.
    //                      JSON example: 'application/json; charset=utf-8'
    //     <parameter>   
    //   </parameters>
    // </summary>
    UpdateElementById: function (
                           apiUrl,
                           data,
                           targetElementId,
                           displayUrl = '',
                           verb = 'GET',
                           callbackFunction = null,
                           focusElementId = '',
                           contentType = 'application/x-www-form-urlencoded')
    {
        const element = document.getElementById ( targetElementId );

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.DataAccess.UpdateElementById: ';
            console.log ( `${logPrefix}Could not find element with id "${targetElementId}".` );
            return;
        }

        SilkFlo.DataAccess.UpdateElement (
            apiUrl,
            data,
            element,
            displayUrl,
            verb,
            callbackFunction,
            focusElementId,
            contentType );
    },


    // <summary>
    //   Update the contents of an HTML element using a string from the server.
    //   Example apiUrl: "./api/blog/...";
    //   <parameters>
    //     <parameter>
    //        apiUrl - The url used to get the element.
    //     <parameter>
    //     <parameter>
    //        data - .send(data) is used to not null
    //     <parameter>
    //     <parameter>
    //        targetElement - The target element whose content will be updated.
    //     <parameter>
    //     <parameter>
    //        displayUrl - The URL displayed in the browsers address text box.
    //     <parameter>
    //     <parameter>
    //        verb - GET|POST etc. Default = 'GET'.
    //     <parameter>
    //     <parameter>
    //        callbackFunction - The callback function that will run after that update is completed.
    //     <parameter>
    //    <parameter>
    //        contentType - Default = 'application/x-www-form-urlencoded'.
    //                      JSON example: 'application/json; charset=utf-8'
    //     <parameter>   
    //   </parameters>
    // </summary>
    // SilkFlo.DataAccess.UpdateElement
    UpdateElement: function (
                       apiUrl,
                       data,
                       targetElement,
                       displayUrl = '',
                       verb = 'GET',
                       callbackFunction = null,
                       focusElementId = '',
                       contentType = 'application/x-www-form-urlencoded',
                       callbackStatusOther,
                       parentId)
    {
        const logPrefix = 'SilkFlo.DataAccess.UpdateElement: ';

        // Guard Clause
        if (!apiUrl)
        {
            console.log ( `${logPrefix}apiUrl parameter missing` );
            return;
        }

        // Guard Clause
        if (!targetElement)
        {
            console.log ( `${logPrefix}targetElement parameter missing` );
            return;
        }

        // Guard Clause
        if (!verb)
        {
            console.log ( `${logPrefix}verb parameter missing` );
            return;
        }


        if (!contentType)
            contentType = 'application/x-www-form-urlencoded';


        const inner = targetElement.innerHTML;
        if (inner.trim().length === 0)
        {
            targetElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';
            targetElement.classList.add ( 'transitionOpacity' );
        }


        const http = new XMLHttpRequest ();
        http.open (
            verb,
            apiUrl,
            true ); // false for synchronous request

        if (typeof data === 'object')
            http.setRequestHeader(
                'Content-type',
                'application/json; charset=utf-8');
        else
            http.setRequestHeader (
                'Content-type',
                contentType );


        http.onreadystatechange = function ()
        {
            return function ()
            {
                if (http.readyState === XMLHttpRequest.DONE)
                {
                    targetElement.style.opacity = 0;
                    setTimeout (
                        function ()
                        {
                            let feedback = http.responseText;

                            if (http.status === 200)
                            {
                                const messagePrefix = 'message: ';
                                if (feedback.length > messagePrefix.length)
                                {
                                    if (feedback.substr (
                                            0,
                                            messagePrefix.length ) ===
                                        messagePrefix)
                                    {
                                        feedback = feedback.Right ( feedback.length - messagePrefix.length );
                                        bootbox.alert ( feedback );
                                        return;
                                    }
                                }


                                SilkFlo.UpdateInnerHTML (
                                    feedback,
                                    targetElement,
                                    focusElementId );

                                if (displayUrl)
                                {
                                    window.scroll (
                                        0,
                                        0 );
                                    window.history.pushState (
                                        displayUrl,
                                        location.hostname,
                                        displayUrl );
                                }

                                if (callbackFunction)
                                    callbackFunction ();

                                setTimeout (
                                    function ()
                                    {
                                        targetElement.style.opacity = 1;
                                    },
                                    250 );
                            }
                            else
                            {
                                if (callbackStatusOther)
                                {
                                    if (!parentId)
                                        parentId = targetElement.id;

                                    callbackStatusOther (
                                        feedback,
                                        parentId );
                                }
                            }
                        },
                        250 );
                }
            };
        } ( this );


        if (data)
        {
            if (typeof data === 'object')
            {
                const json = JSON.stringify(data);
                http.send(json);
            }
            else
                http.send ( data );
        }
        else
            http.send ();
    },


    // SilkFlo.DataAccess.UpdateElementFromAttribute
    UpdateElementFromAttribute: function (id)
    {
        // Guard Clause
        if (!id)
        {
            const logPrefix = 'SilkFlo.DataAccess.UpdateElementFromAttribute: ';
            console.log ( logPrefix + 'id is missing.' );
            return;
        }

        const element = document.getElementById ( id );

        // Guard Clause
        if (element)
            SilkFlo.DataAccess.UpdateTargetElement ( element );
    },


    // SilkFlo.DataAccess.UpdateTargetElement
    UpdateTargetElement: function (target)
    {
        const logPrefix = 'SilkFlo.DataAccess.UpdateTargetElement: ';

        // Guard Clause
        if (!target)
        {
            console.log ( logPrefix + 'target parameter missing' );
            return;
        }

        const attributes = SilkFlo.DataAccess.GetAttributes ( target );

        // Guard Clause
        // Get the silkflo-url attribute content
        if (!attributes.Url )
        {
            console.log ( `${logPrefix + SilkFlo.DataAccess.AttributeName.Url} is not present for ${target}` );
            return;
        }


        let url = `/api/${attributes.Url}`;

        if (attributes.IncludePath
            && window.location.pathname.length > 1)
            url += `/Path?path=${window.location.pathname}`;


        // Get content
        SilkFlo.DataAccess.UpdateTargetElementWithURL (
            target,
            url,
            attributes.Callback );
    },


    // SilkFlo.DataAccess.UpdateTargetElementWithURL
    UpdateTargetElementWithURL: function (
                                    target,
                                    url,
                                    callbackFunction)
    {
        const logPrefix = 'SilkFlo.DataAccess.UpdateTargetElementWithURL: ';

        // Guard Clause
        if (!target)
        {
            console.log ( `${logPrefix}target parameter missing` );
            return;
        }

        // Guard Clause
        if (!url)
        {
            console.log ( `${logPrefix}url parameter missing.` );
            return;
        }

        if (HotSpot.Card)
            HotSpot.Card.Close();

        // Do the business
        target.IsLoading = true;
        if (target.innerHTML.trim().length === 0)
            target.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

        // Setup request
        const http = new XMLHttpRequest ();
        http.open (
            'GET',
            url,
            true ); // false for synchronous request

        http.setRequestHeader (
            'Content-type',
            'application/x-www-form-urlencoded' );

        http.onreadystatechange = function (target)
        {
            return function ()
            {
                if (http.readyState === XMLHttpRequest.DONE && http.status === 200)
                {
                    const str = http.responseText;

                    // Update the element
                    if (target.innerHTML.trim () !== str)
                    {
                        if (target.parentElement && target.parentElement.children)
                        {
                            const indexInParent = [].slice.call ( target.parentElement.children )
                                .indexOf ( target );
                            target.innerHTML = str;

                            if (target.ResizeColumnHeader)
                                target.ResizeColumnHeader ();

                            target = target.parentElement.children[indexInParent];


                            HotSpot.Get ( target );
                            
                            SilkFlo.DataAccess.GetJsonComponents(target);
                            Delaney.UI.DatePicker.CreateDatePickers ( target );
                            SilkFlo.ReplaceImgWithSVG ( target );
                            Delaney.UI.Grid.Create ( target );
                            SilkFlo.TabBar.SetUp ( target );
                            SilkFlo.SVGTools.AnimatePaths ( target );
                            SilkFlo.Analytics.AssignClickEvents ( target );
                            SilkFlo.DataAccess.GetSubComponents ( target );
                        }
                    }

                    if (callbackFunction)
                        SilkFlo.ExecuteFunctionByName ( callbackFunction, null, [target, str] );

                    SilkFlo.DataAccess.LoadOnce ( target );
                }
            };
        } ( target );

        // Send request
        http.send ();
    },


    // SilkFlo.DataAccess.GetJsonComponents
    GetJsonComponents: function (element)
    {
        // Get Json for target
        const elements = Array.apply(
            null,
            element.querySelectorAll(`[${SilkFlo.DataAccess.AttributeName.Json}]:not([${SilkFlo.DataAccess.AttributeName.GetManually}])`));

        const length = elements.length;
        for (let i = 0; i < length; i++) {
            const target = elements[i];
            SilkFlo.DataAccess.GetJson(target);
        }
    },


    // SilkFlo.DataAccess.GetJson
    GetJson: function (
                 target)
    {
        const logPrefix = 'SilkFlo.DataAccess.GetJson: ';

        // Guard Clause
        if (!target) {
            console.log(logPrefix + 'target parameter missing');
            return false;
        }


        const attributes = SilkFlo.DataAccess.GetAttributes(target);

        // Guard Clause
        // Get the silkflo-url attribute content
        if (!attributes.Json) {
            return false;
        }

        // Guard Clause
        if (!attributes.Callback)
        {
            console.log(`${logPrefix}Callback function missing missing.`);
            return false;
        }

        const callbackFunction = attributes.Callback;

        const url = `/api/${attributes.Json}`;

        // Setup request
        const http = new XMLHttpRequest();
        http.open(
            'GET',
            url,
            true); // false for synchronous request

        http.setRequestHeader(
            'Content-type',
            'application/x-www-form-urlencoded');

        http.onreadystatechange = function (target)
        {
            return function ()
            {
                if (http.readyState === XMLHttpRequest.DONE && http.status === 200)
                {
                    const str = http.responseText;

                    SilkFlo.ExecuteFunctionByName (
                        callbackFunction,
                        null,
                        [
                            target, str
                        ] );
                }
            }
        }(target);

        // Send request
        http.send();

        return true;
    },

    // SilkFlo.DataAccess.LoadOnce
    LoadOnce: function (element)
    {
        const attributes = SilkFlo.DataAccess.GetAttributes ( element );

        if (!attributes.LoadOnce)
            return;

        element.removeAttribute(SilkFlo.DataAccess.AttributeName.Url);
        element.removeAttribute(SilkFlo.DataAccess.AttributeName.Json);
        element.removeAttribute ( SilkFlo.DataAccess.AttributeName.IncludePath );
        element.removeAttribute ( SilkFlo.DataAccess.AttributeName.Callback );
        element.removeAttribute(SilkFlo.DataAccess.AttributeName.LoadOnce);
        element.removeAttribute(SilkFlo.DataAccess.AttributeName.GetManually);
        element.SilkFlo = undefined;
    },


    // SilkFlo.DataAccess.GetComponents
    GetComponents: function ()
    {
        // Get the element with the silkflo-url attribute
        let elements = Array.apply (
            null,
            document.querySelectorAll(`[${SilkFlo.DataAccess.AttributeName.Url}]:not([${SilkFlo.DataAccess.AttributeName.GetManually}])` ) );


        let length = elements.length;
        for (let i = 0; i < length; i++)
        {
            const element = elements[i];

            element.IsLoading = false;
            const count = SilkFlo.DataAccess.GetParents (
                element,
                'count' );
            element.SilkFloCount = count;
        }

        // Sort the list
        elements = elements.sort (
            (a, b) =>
            {
                return a.SilkFloCount - b.SilkFloCount;
            } );


        const ancestors = [];

        for (let i = 0; i < length; i++)
        {
            const element = elements[i];

            if (element.IsLoading)
                continue;


            if (SilkFlo.DataAccess.IsInFamilyTree (
                ancestors,
                element ))
            {
                continue;
            }

            ancestors[ancestors.length] = element;

            SilkFlo.DataAccess.UpdateTargetElement ( element );
        }


    },



    // SilkFlo.DataAccess.IsInFamilyTree
    IsInFamilyTree: function (
                        ancestors,
                        element)
    {
        const logPrefix = 'SilkFlo.DataAccess.IsInFamilyTree: ';

        // Guard Clause
        if (!ancestors)
        {
            console.log ( `${logPrefix}ancestors parameter missing` );
            return null;
        }


        // Guard Clause
        if (!element)
        {
            console.log ( `${logPrefix}element parameter missing` );
            return null;
        }


        // Guard Clause
        if (element.parentElement === null)
            return false;


        const length = ancestors.length;

        // Guard Clause
        if (length === 0)
            return false;


        // Do the business
        for (let i = 0; i < length; i++)
        {
            const ancestor = ancestors[i];

            let parent = element.parentElement;

            while (parent)
            {
                if (parent === ancestor)
                    return true;

                parent = parent.parentElement;
            }
        }

        return false;
    },


    /*
     * Summary
     * -------
     * Get a list of parent elements for the provided leaf node
     *
     * Parameters
     * ----------
     * element    - The leaf element.
     * returnWhat - name, count or null.
     * isDescend  - true or null
     */
    // SilkFlo.DataAccess.GetParents
    GetParents: function (
                    element,
                    returnWhat,
                    isDescend)
    {
        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.DataAccess.GetParents: ';
            console.log ( `${logPrefix}element parameter missing` );
            return null;
        }

        // Do the business
        const elements = [];

        while (element.parentElement !== null)
        {
            elements[elements.length] = returnWhat === 'name' ? element.nodeName : element;
            element = element.parentElement;
        }

        if (returnWhat === 'count')
            return elements.length;


        if (isDescend)
            return elements.reverse ();

        return elements;
    },


    // SilkFlo.DataAccess.GetSubComponents
    GetSubComponents: function (parent)
    {
        // Get the element with the Silkflo-url attribute
        const elements = parent.querySelectorAll(`[${SilkFlo.DataAccess.AttributeName.Url}]:not([${SilkFlo.DataAccess.AttributeName.GetManually}])` );

        if (elements.length === 0)
            return;


        for (let i = 0; i < elements.length; i++)
        {
            SilkFlo.DataAccess.UpdateTargetElement ( elements[i] );
        }
    },

    AppendElement: function (
                       apiUrl,
                       data,
                       targetElement,
                       callbackFunction,
                       insertBefore,
                       neighbourElement)
    {
        const logPrefix = 'SilkFlo.DataAccess.AppendElement: ';

        // Guard Clause
        if (!apiUrl)
        {
            console.log ( `${logPrefix}apiUrl parameter missing` );
            return;
        }

        // Guard Clause
        if (!targetElement)
        {
            console.log ( `${logPrefix}targetElement parameter missing` );
            return;
        }


        const http = new XMLHttpRequest ();
        http.open (
            'GET',
            apiUrl,
            true ); // false for synchronous request

        http.setRequestHeader (
            'Content-type',
            'application/x-www-form-urlencoded' );

        http.onreadystatechange = function ()
        {
            return function ()
            {
                if (http.readyState === XMLHttpRequest.DONE && http.status === 200)
                {
                    const template = document.createElement ( 'template' );
                    template.innerHTML = http.responseText;

                    const element = template.content.cloneNode ( true );

                    if (neighbourElement)
                    {
                        targetElement.insertBefore (
                            element,
                            neighbourElement );
                    }
                    else
                    {
                        if (insertBefore)
                        {
                            targetElement.insertBefore (
                                element,
                                targetElement.firstChild );
                        }
                        else
                        {
                            targetElement.appendChild (
                                element );
                        }
                    }


                    if (callbackFunction)
                        callbackFunction ();
                }
            };
        } ( this );


        if (data)
            http.send ( data );
        else
            http.send ();
    },


    // SilkFlo.DataAccess.GetObject
    GetObject: function (
        apiUrl,
        callbackFunction,
        callbackFunctionFailed,
        parentId)
    {
        // Guard Clause
        if (!apiUrl) {
            const logPrefix = 'SilkFlo.DataAccess.GetObject: ';
            console.log(`${logPrefix}apiUrl parameter missing`);
            return;
        }


        const http = new XMLHttpRequest();
        http.open(
            'GET',
            apiUrl,
            true); // false for synchronous request

        http.setRequestHeader(
            'Content-type',
            'application/x-www-form-urlencoded');

        http.onreadystatechange = function () {
            return function () {
                if (http.readyState === XMLHttpRequest.DONE) {

                    if (http.status === 200)
                    {
                        const response = http.responseText;

                        if (callbackFunction)
                            callbackFunction(response);
                    }
                    else
                    {
                        const feedback = http.responseText;

                        if (callbackFunctionFailed) {
                            callbackFunctionFailed(
                                feedback, parentId);
                        }
                    }
                }
            };
        }(this);


        http.send();
    },



    InsertAfter: function (
                     newNode,
                     existingNode)
    {
        existingNode.parentNode.insertBefore (
            newNode,
            existingNode.nextSibling );
    },



    // SilkFlo.DataAccess.Feedback
    Feedback: function (feedback, parentId)
    {
        const logPrefix = 'SilkFlo.DataAccess.Feedback: ';

        // Guard Clause
        if (!feedback)
            return;

        // Guard Clause
        if (!parentId)
        {
            console.log(`${logPrefix}parentId parameter missing`);
            return;
        }


        const parent = document.getElementById(parentId);

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${parentId} missing`);
            return;
        }




        const object = JSON.parse(feedback);
        let errors = [];

        const elements = parent.querySelectorAll(`[silkFlo-feedback]`);
        if (elements && elements.length > 0)
        {
            const length = elements.length;
            for (let i = 0; i < length; i++)
            {
                const element = elements[i];

                // Guard Clause
                if (!element)
                    continue;

                // Hide feedback element
                element.style.display = '';
            }
        }


        if (object.elements) {
            const keys = Object.keys(object.elements);
            const length = keys.length;

            let prefix = parentId + '.';
            if (object.namePrefix) {
                prefix = object.namePrefix;
            }

            for (let i = 0; i < length; i++)
            {
                const camel = keys[i];
                const pascal = SilkFlo.DataAccess.ToPascalCase(camel);
                const value = object.elements[camel];

                let name = prefix + pascal;
                let element = parent.querySelector(`[name="${name}"]`);

                // Guard Clause
                if (!element)
                {
                    console.log(`${logPrefix}Element with name ${name} missing`);
                    continue;
                }

                element.classList.add('is-invalid');

                name += '_InvalidFeedback';

                element = parent.querySelector(`[name="${name}"]`);

                // Guard Clause
                if (element)
                {
                    if (value)
                        element.innerHTML = value;

                    element.style.display = 'block';
                }
                else {
                    console.log(`${logPrefix}Element with name ${name} missing`);
                }

                errors.push ( value );
            }
        }

        if (object.message) {
            SilkFlo.Models.Abstract.Message(object.message, parent);

            bootbox.dialog({
                title: 'SilkFlo',
                message: object.message,
                onEscape: true,
                backdrop: true,
                buttons: {
                    Ok: {
                        label: 'Ok',
                        className: 'btn-danger'
                    }
                }
            });
        }


        let id = 'errorButton';
        const buttonElement = document.getElementById(id);

        id = 'errorModalBody';
        const modalBodyElement = document.getElementById(id);

        if (buttonElement
         && modalBodyElement)
        {
            const length = errors.length;

            if (length === 0)
                return;

            let buttonText = 'There is 1 error.';
            if (length > 1)
                buttonText = `There are ${length} errors.`;

            buttonElement.innerHTML = buttonText;
            buttonElement.style.display = 'block';


            let content = '<ul>';
            for (let i = 0; i < length; i++)
            {
                content += `<li class="text-danger">${errors[i]}</li>`;
            }
            content += '</ul>';

            modalBodyElement.innerHTML = content;
        }
    },


    // SilkFlo.DataAccess.ResetFeedback
    ResetFeedback: function (
        parent,
        elementNames,
        namePrefix = '',
        logErrors = false)
    {
        const logPrefix = 'SilkFlo.DataAccess.ResetFeedback: ';


        // Guard Clause
        if (!elementNames) {
            console.log(`${logPrefix}elementNames parameter missing`);
            return;
        }

        if (!Array.isArray(elementNames))
        {
            console.log(`${logPrefix}elementNames is not an array`);
            return;
        }

        let length = elementNames.length;
        for (let i = 0; i < length; i++)
        {
            let name = namePrefix + elementNames[i];

            let element = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element) {
                if (logErrors)
                {
                    console.log ( `${logPrefix}Element with name ${name} missing` );
                }
                continue;
            }


            element.classList.remove('is-invalid');

            name += '_InvalidFeedback';

            element = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (element)
                element.style.display = 'none';
            else
            {
                if (logErrors)
                {
                    console.log ( `${logPrefix}Element with name ${name} missing` );
                }
            }
        }

        const elements = parent.querySelectorAll(`[silkFlo-feedback]`);
        if (elements && elements.length > 0)
        {
            length = elements.length;
            for (let i = 0; i < length; i++)
            {
                const element = elements[i];
                element.style.display = '';
            }
        }
    },


    // SilkFlo.DataAccess.ToPascalCase
    /*
     * Convert a string from camel to pascal case.
     */
    ToPascalCase: function (str) {
        if (!str)
            return '';

        if (str.length === 1)
            return str.toUpperCase();

        return str.substring(
                0,
                1)
            .toUpperCase() +
            str.substring(
                1,
                str.length);
    }
};