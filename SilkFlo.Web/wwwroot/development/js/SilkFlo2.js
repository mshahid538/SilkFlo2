
SilkFlo.ShowApplicationMode = function ()
{
    const isPractice = SilkFlo.GetCookie('IsPractice');

    if (isPractice.toLowerCase() === 'true')
    {
        const id = 'practiceAccountPlaceHolder';
        const element = document.getElementById(id);

        // Guard Clause
        if (element)
        {
            element.classList.remove ('hide');
        }

    }


    // Release the UI to render the element off screen,
    // by setting the timeout to 1 ms.
    setTimeout(function ()
    {
        const id = 'applicationMode';
        const element = document.getElementById(id);

        if (!element)
            return;

        element.classList.remove('offScreen');
    }, 10);

};


SilkFlo.TogglePracticeMode = function ()
{
    // Release the UI to render the element off screen,
    // by setting the timeout to 1 ms.
    setTimeout(function ()
    {
        let id = 'practiceMode';
        const element = document.getElementById(id);

        if (!element)
            return;

        const isPractice = SilkFlo.GetCookie('IsPractice');

        id = 'practiceAccountPlaceHolder';
        const elementPlaceHolder = document.getElementById(id);


        if (isPractice.toLowerCase() === 'true')
        {
            element.classList.remove('offScreen');

            if (elementPlaceHolder)
            {
                elementPlaceHolder.style.display = '';
            }
        }
        else
        {
            element.classList.add('offScreen');

            if (elementPlaceHolder)
            {
                elementPlaceHolder.style.display = 'none';
            }
        }
    }, 10);
};




SilkFlo.SelectApplicationPage = function (page)
{
    const rows = document.querySelectorAll('[pageId]');

    let length = rows.length;
    for (let i = 0; i < length; i++)
    {
        const pageId = rows[i].getAttribute("pageId");

        if (pageId === page)
            rows[i].style.display = 'table-row';
        else
            rows[i].style.display = 'none';
    }


    const pageButtons = document.querySelectorAll('[paginationId]');
    length = pageButtons.length;
    for (let i = 0; i < length; i++)
    {
        const paginationId = pageButtons[i].getAttribute("paginationId");
        if (paginationId === page)
            pageButtons[i].classList.add('active');
        else
            pageButtons[i].classList.remove('active');
    }


    const paginationFirst = document.getElementById('paginationFirst');
    if (paginationFirst)
    {
        if (page === '1')
        {
            paginationFirst.removeAttribute('onclick');
        }
        else
        {
            paginationFirst.setAttribute('onclick', "SilkFlo.SelectApplicationPage('" + page-- + "');return false;");
        }
    }


    const paginationLast = document.getElementById('paginationLast');
    if (paginationLast !== null)
    {
        const paginationId = pageButtons[pageButtons.length-1].getAttribute("paginationId");
        if (page === paginationId) {
            paginationFirst.removeAttribute('onclick');
        }
        else {
            paginationFirst.setAttribute('onclick', "SilkFlo.SelectApplicationPage('" + page++ + "');return false;");
        }
    }
};


SilkFlo.ShowDashboard = function ()
{
    SilkFlo.SideBar.OnClick('/Dashboard');
};


SilkFlo.ShowOKMessage = function (title, message)
{
    const logPrefix = 'SilkFlo.ShowOKMessage: ';

    const messageBox = document.getElementById('MessageBox_Ok');

    //Guard Clause
    if (!messageBox)
    {
        console.log(logPrefix + 'element with id "MessageBox_Ok" missing');
        return;
    }

    const titleElement = messageBox.querySelector('[name="Title"]');

    //Guard Clause
    if (!titleElement)
    {
        console.log(logPrefix + 'element with id "Title" missing');
        return;
    }

    const messageElement = messageBox.querySelector('[name="Message"]');

    //Guard Clause
    if (!messageElement)
    {
        console.log(logPrefix + 'element with id "Message" missing');
        return;
    }

    titleElement.innerHTML = title;
    messageElement.innerHTML = message;
    window.$ ('#MessageBox_Ok').modal ('show');
};


SilkFlo.ShowYesNoMessage = function (
    title,
    message,
    onClickYes,
    onClickNo,
    yesLabel = 'Yes',
    noLabel = 'No')
{
    const logPrefix = 'SilkFlo.ShowYesNoMessage: ';

    const messageBox = document.getElementById('MessageBox_YesNo');

    //Guard Clause
    if (!messageBox)
    {
        console.log(logPrefix + 'element with id "MessageBox_Ok" missing');
        return;
    }


    const titleElement = messageBox.querySelector('[name="Title"]');

    //Guard Clause
    if (!titleElement)
    {
        console.log(logPrefix + 'element with id "Title" missing');
        return;
    }

    const messageElement = messageBox.querySelector('[name="Message"]');

    //Guard Clause
    if (!messageElement)
    {
        console.log(logPrefix + 'element with id "Message" missing');
        return;
    }


    const yesElement = messageBox.querySelector('[name="Yes"]');

    //Guard Clause
    if (!yesElement)
    {
        console.log(logPrefix + 'element with id "Yes" missing');
        return;
    }


    const noElement = messageBox.querySelector('[name="No"]');

    //Guard Clause
    if (!noElement)
    {
        console.log(logPrefix + 'element with id "No" missing');
        return;
    }


    titleElement.innerHTML = title;
    messageElement.innerHTML = message;
    yesElement.innerHTML = yesLabel;
    yesElement.addEventListener ('click',
        onClickYes);
    noElement.innerHTML = noLabel;
    noElement.addEventListener ('click',
        onClickNo);

    window.$ ('#MessageBox_YesNo').modal ('show');
};


SilkFlo.SendToClickboard = function (text)
{
    if (!text)
        return;

    navigator.clipboard.writeText(text);
};


SilkFlo.KeyPressNumberOnly = function (event, followOnFunction)
{
    if (event.key === 'Backspace'
        || event.key === 'ArrowLeft'
        || event.key === 'ArrowRight'
        || event.key === 'Delete'
        || event.key === 'Tab'
            )
        return;


    if (event.which === 189) // -
        event.preventDefault();

    if (event.which === 69) // e
        event.preventDefault();

    if (event.key === '.'
        && event.target.innerHTML.indexOf('.') > -1)
        event.preventDefault();

    if (!IsNumeric(event.key) && event.key !== '.')
        event.preventDefault();

    if (followOnFunction)
        followOnFunction(event);
};






SilkFlo.FormatNumber = function (value)
{
    if (typeof value !== 'number')
    {
        const number = parseFloat(value);
        return number.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    return value.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
};



SilkFlo.SVGTools = {

    // SilkFlo.SVGTools.AnimatePaths
    // https://stackoverflow.com/questions/14275249/how-can-i-animate-a-progressive-drawing-of-svg-path
    // https://codepen.io/GreenSock/pen/nwxQvy
    // SilkFlo.SVGTools.AnimatePaths
    AnimatePaths: function (parentElement)
    {

        // Guard Clause
        if (!parentElement)
        {
            const logPrefix = 'SilkFlo.SVGTools.AnimatePaths: ';
            console.log(`${logPrefix}parentElement parameter missing`);
            return;
        }

        const distancePerPoint = 1;

        const paths = parentElement.querySelectorAll('[silkflo-fps]');


        const count = paths.length;

        for (let i = 0; i < count; i++)
        {
            const path = paths[i];

            if (path.IntervalId)
                continue;

            path.length = 0;
            path.TotalLength = path.getTotalLength();



            path.IncreaseLength = function (element)
            {
                // element is a svg path element.

                const pathLength = element.TotalLength;
                element.length += distancePerPoint;

                element.style.strokeDasharray = [element.length, pathLength].join(' ');

                if (element.length >= pathLength)
                {
                    //console.log(logPrefix + 'Timer Stopped - ' + element.IntervalId);
                    clearInterval(element.IntervalId);
                    element.IntervalId = null;
                }
            };

            let fps = path.getAttribute('silkflo-fps');
            if (!fps)
                fps = 120;


            let colour = path.getAttribute('silkflo-stroke');
            if (!colour)
                colour = 'black';

            const seconds = path.getAttribute('silkflo-animate-seconds');
            if (seconds && IsNumeric(seconds))
            {
                fps = path.TotalLength / seconds;
            }


            path.style.strokeDasharray = '';
            path.style.stroke = colour;
            //path.IntervalId  = setInterval(
            //    function () { path.IncreaseLength(path); }, 1000 / fps);

            path.IntervalId = setInterval(path.IncreaseLength, 1000 / fps, path);
        }

    }
};


SilkFlo.GetClosestParent = function (
                               element,
                               selector)
{
    // https://gomakethings.com/how-to-get-the-closest-parent-element-with-a-matching-selector-using-vanilla-javascript/

    const logPrefix = 'SilkFlo.GetClosestParent: ';

    // Guard Clause
    if (!element)
    {
        console.log ( `${logPrefix}element parameter missing` );
        return null;
    }

    // Guard Clause
    if (!selector)
    {
        console.log ( `${logPrefix}selector parameter missing` );
        return null;
    }

    // Element.matches() polyfill
    if (!Element.prototype.matches)
    {
        Element.prototype.matches =
            Element.prototype.matchesSelector ||
            Element.prototype.mozMatchesSelector ||
            Element.prototype.msMatchesSelector ||
            Element.prototype.oMatchesSelector ||
            Element.prototype.webkitMatchesSelector ||
            function (s)
            {
                const matches = (this.document || this.ownerDocument).querySelectorAll ( s );
                let i = matches.length;
                while (--i >= 0 && matches.item ( i ) !== this)
                {}
                return i > -1;
            };
    }

    // Get the closest matching element
    for (; element && element !== document; element = element.parentNode)
    {
        if (element.matches ( selector ))
            return element;
    }

    return null;
};

SilkFlo.GReCaptchaPublicKey = '6Ldpx1IfAAAAAPe-KeEQw7z2sB_wmwqkD6AKEGpV';
SilkFlo.Radio_Click = function (
                          selected,
                          func)
{
    const logPrefix = 'SilkFlo.Radio_Click: ';

    // Guard Clause
    if (!selected)
    {
        console.log ( `${logPrefix}selected parameter missing` );
        return;
    }

    const siblings = SilkFlo.GetSiblings ( selected.parentElement );
    const options = [];
    const length = siblings.length;
    for (let i = 0; i < length; i++)
    {
        const element = siblings[i];
        if (!element.classList.contains ( 'form-check' ))
            continue;

        const children = element.children;
        const length2 = children.length;
        for (let j = 0; j < length2; j++)
        {
            const child = children[j];
            const type = child.getAttribute ( 'type' );
            if (type && type === 'radio')
                options.push ( child );
        }
    }

    const length3 = options.length;
    for (let k = 0; k < length3; k++)
    {
        const option = options[k];
        option.checked = false;
    }

    if (func)
    {
        func ( selected );
    }
};

SilkFlo.GetSiblings = function (element)
{
    // Guard Clause
    if (!element)
    {
        const logPrefix = 'SilkFlo.GetSiblings: ';
        console.log ( `${logPrefix}element parameter missing` );
        return null;
    }

    const siblings = [];

    // if no parent, return empty list
    if (!element.parentNode)
    {
        return siblings;
    }

    // first child of the parent node
    let sibling = element.parentNode.firstElementChild;

    // loop through next siblings until `null`
    if (sibling)
        do
        {
            // push sibling to array
            if (sibling !== element)
            {
                siblings.push ( sibling );
            }
            sibling = sibling.nextElementSibling;
        }
        while (sibling);

    return siblings;
};

SilkFlo.SetFocus = function (element)
{
    // Guard Clause
    if (!element)
    {
        const logPrefix = 'SilkFlo.SetFocus: ';
        console.log ( `${logPrefix}element parameter missing` );
        return;
    }

    setTimeout(function ()
    {
        element.focus();
    }, 500);
};

SilkFlo.GetPadding = function (element)
{
    const style = element.currentStyle || window.getComputedStyle ( element );

    const result = {
        GetLeft: function ()
        {
            return parseInt ( style.paddingLeft );
        },
        GetTop: function ()
        {
            return parseInt ( style.paddingTop );
        },
        GetRight: function ()
        {
            return parseInt ( style.paddingRight );
        },
        GetBottom: function ()
        {
            return parseInt ( style.paddingBottom );
        }
    };

    return result;
};

SilkFlo.ToggleFilter = function (
                           event,
                           populateFunction)
{
    const logPrefix = 'SilkFlo.ToggleFilter: ';

    // Guard Clause
    if (!event) {
        console.log(`${logPrefix}event parameter missing`);
        return;
    }

    event.stopPropagation();

    const button = document.getElementById('filter');


    let card = button.Card;

    if (!card)
    {
        const name = 'filterContainer';
        card = button.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!card) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        button.Card = card;

        document.addEventListener (
            'scroll',
            (e) =>
            {
                card.SetPosition ();
            });


        window.addEventListener(
            'resize',
            (e) => {
                card.SetPosition();
            });


        document.addEventListener (
            'click',
            SilkFlo.Pages.Cards.FilterCriteria.Close,
            false );
    }


    if (card.style.display === 'none')
    {
        // Show
        if (card.parentElement === window.body)
        {
            card.style.display = '';
        }
        else
        {
            // Delete old
            const elements = window.body.querySelectorAll(`[name="${name}"]`);
            const length = elements.length;
            for (let i = 0; i < length; i++)
            {
                const element = elements[i];
                if (element !== card)
                {
                    element.remove ();
                }
            }


            window.body.appendChild(card);
            card.style.position = 'fixed';
            card.onclick = function (event) {
                event.preventDefault();
                event.stopPropagation();
            };
        }

        card.style.display = '';

        card.SetPosition = function ()
        {
            const nameLists = 'lists';
            const elementLists = card.querySelector(`[name="${nameLists}"]`);

            // Guard Clause
            if (!elementLists) {
                console.log(`${logPrefix}Element with name ${nameLists} missing`);
                return;
            }

            const buttonRectangle = button.getBoundingClientRect ();

            const buttonMarginLeft = buttonRectangle.left;
            const buttonMarginRight = window.innerWidth - buttonRectangle.right;

            // right align
            let left = buttonMarginRight;

            if (buttonMarginLeft < buttonMarginRight)
                left = buttonMarginLeft;


            const top = buttonRectangle.top + buttonRectangle.height;

            // Width
            let width = buttonRectangle.left + buttonRectangle.width - buttonMarginRight;

            if (buttonMarginLeft < buttonMarginRight)
                width = window.innerWidth - buttonMarginLeft - 20;

            const maxWidthStyle = card.style.maxWidth;
            const maxWidth = maxWidthStyle.ToPixel();

            const minWidthStyle = card.style.minWidth;
            const minWidth = minWidthStyle.ToPixel();

            let maxHeight = '40vh';
            if (document.documentElement.clientHeight > document.documentElement.clientWidth)
                maxHeight = '80vh';

            maxHeight = maxHeight.ToPixel();


            

            if (width > maxWidth)
            {
                width = maxWidth;
                left = buttonRectangle.right - width;
            }



            if (width < minWidth)
                width = minWidth;


            if (left + width > document.documentElement.clientWidth)
                left = (document.documentElement.clientWidth - width) / 2;

            card.style.left = left + 'px';
            card.style.top = top + 'px';
            card.style.width = width + 'px';



            maxHeight = maxHeight - buttonRectangle.top;
            elementLists.style.height = maxHeight + 'px';
        };

        
        card.SetPosition ();
  

        card.onclick = function (event)
        {
            event.preventDefault ();
            event.stopPropagation ();
        };

    }
    else
    {
        // Hide
        card.style.display = 'none';
    }

    if (populateFunction)
        populateFunction ();
};



// SilkFlo.ValidatePassword
SilkFlo.ValidatePassword = function (element)
{
    const logPrefix = 'SilkFlo.ValidatePassword: ';

    window.PasswordValid = false;

    // Guard Clause
    if (!element) {
        console.log(`${logPrefix}element parameter missing`);
        return;
    }


    let id = 'PasswordRules';
    const elementPasswordRules = document.getElementById(id);

    // Guard Clause
    if (!elementPasswordRules) {
        console.log(`${logPrefix}Element with id ${id} missing`);
        return;
    }


    let name = 'PasswordRules.Eight';
    const elementEight = elementPasswordRules.querySelector(`[name="${name}"]`);

    // Guard Clause
    if (!elementEight) {
        console.log(`${logPrefix}Element with name ${name} missing`);
        return;
    }

    name = 'PasswordRules.Three';
    const elementThree = elementPasswordRules.querySelector(`[name="${name}"]`);

    // Guard Clause
    if (!elementThree) {
        console.log(`${logPrefix}Element with name ${name} missing`);
        return;
    }

    name = 'PasswordRules.Lower';
    const elementLower = elementPasswordRules.querySelector(`[name="${name}"]`);

    // Guard Clause
    if (!elementLower) {
        console.log(`${logPrefix}Element with name ${name} missing`);
        return;
    }

    name = 'PasswordRules.Upper';
    const elementUpper = elementPasswordRules.querySelector(`[name="${name}"]`);

    // Guard Clause
    if (!elementUpper) {
        console.log(`${logPrefix}Element with name ${name} missing`);
        return;
    }

    name = 'PasswordRules.Numbers';
    const elementNumbers = elementPasswordRules.querySelector(`[name="${name}"]`);

    // Guard Clause
    if (!elementNumbers) {
        console.log(`${logPrefix}Element with name ${name} missing`);
        return;
    }

    name = 'PasswordRules.Special';
    const elementSpecial = elementPasswordRules.querySelector(`[name="${name}"]`);

    // Guard Clause
    if (!elementSpecial) {
        console.log(`${logPrefix}Element with name ${name} missing`);
        return;
    }


    elementPasswordRules.style.opacity = 1;

    const value = element.value;

    let eightIsValid = false;
    if (value.length < 8) {
        // is inValid
        elementEight.classList.add('isInValid');
        elementEight.classList.remove('isValid');
        elementEight.setAttribute('aria-invalid', 'true');
    }
    else {
        // is valid
        elementEight.classList.remove('isInValid');
        elementEight.classList.add('isValid');
        elementEight.setAttribute('aria-invalid', 'false');
        eightIsValid = true;
    }


    let count = 0;

    if ((/[a-z]/.test(value))) {
        // is valid
        count++;
        elementLower.classList.remove('isInValid');
        elementLower.classList.add('isValid');
        elementLower.setAttribute(
            'aria-invalid',
            'false');
    }
    else {
        // is inValid
        elementLower.classList.add('isInValid');
        elementLower.classList.remove('isValid');
        elementLower.setAttribute(
            'aria-invalid',
            'true');
    }

    if ((/[A-Z]/.test(value))) {
        // is valid
        count++;
        elementUpper.classList.remove('isInValid');
        elementUpper.classList.add('isValid');
        elementUpper.setAttribute(
            'aria-invalid',
            'false');
    }
    else {
        // is inValid
        elementUpper.classList.add('isInValid');
        elementUpper.classList.remove('isValid');
        elementUpper.setAttribute('aria-invalid', 'true');
    }


    if ((/[0-9]/.test(value))) {
        // is valid
        count++;
        elementNumbers.classList.remove('isInValid');
        elementNumbers.classList.add('isValid');
        elementNumbers.setAttribute('aria-invalid', 'false');
    }
    else {
        // is inValid
        elementNumbers.classList.add('isInValid');
        elementNumbers.classList.remove('isValid');
        elementNumbers.setAttribute('aria-invalid', 'true');
    }

    const specialChars = /[!@#\$%\^&\*]/;
    if (specialChars.test(value)) {
        // is valid
        count++;
        elementSpecial.classList.remove('isInValid');
        elementSpecial.classList.add('isValid');
        elementSpecial.setAttribute('aria-invalid', 'false');
    }
    else {
        // is inValid
        elementSpecial.classList.add('isInValid');
        elementSpecial.classList.remove('isValid');
        elementSpecial.setAttribute('aria-invalid', 'true');
    }


    let threeIsValid = false;
    if (count < 3) {
        // is inValid
        elementThree.classList.add('isInValid');
        elementThree.classList.remove('isValid');
        elementThree.setAttribute('aria-invalid', 'true');
    }
    else {
        // is valid
        elementThree.classList.remove('isInValid');
        elementThree.classList.add('isValid');
        elementThree.setAttribute('aria-invalid', 'false');
        threeIsValid = true;
    }

    window.PasswordValid = eightIsValid && threeIsValid;
};

SilkFlo.ResetPassword = {
    // SilkFlo.ResetPassword.ToggleButtonState
    ToggleButtonState: function ()
    {
        const logPrefix = 'SilkFlo.ResetPassword.ToggleButtonState: ';

        const id = 'ResetPassword';
        const parent = document.getElementById ( id );

        // Guard Clause
        if (!parent)
        {
            console.log ( `${logPrefix}Element with id ${id} missing` );
            return;
        }

        let name = 'Email';
        const elementEmail = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!elementEmail)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }

        name = 'Password';
        const elementPassword = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!elementPassword)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
        }

        name = 'ConfirmPassword';
        const elementConfirmPassword = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementConfirmPassword) {
            console.log(`${logPrefix}Element with name ${name} missing`);
        }


        name = 'btn';
        const btn = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!btn) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        //console.log('elementEmail.value: ' + elementEmail.value);
        //console.log('elementPassword.value: ' + elementPassword.value);
        //console.log('elementConfirmPassword.value: ' + elementConfirmPassword.value);


        if (elementEmail.value.length > 0
            && elementPassword.value === elementConfirmPassword.value) {
            if (window.PasswordValid)
                btn.classList.remove('disabled');
            else
                btn.classList.add('disabled');
        }
        else
            btn.classList.add('disabled');
    }
};

SilkFlo.SignUpShort = {
    // SilkFlo.ResetPassword.ToggleButtonState
    ToggleButtonState: function () {
        const logPrefix = 'SilkFlo.ResetPassword.ToggleButtonState: ';

        const id = 'SignUpShort';
        const parent = document.getElementById(id);

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        const model = SilkFlo.Models.Abstract.GetModelFromParent (
            parent,
            ['FirstName', 'LastName', 'Password', 'ConfirmPassword', 'Button']);

        //console.log('model.FirstName: ' + model.FirstName);
        //console.log('model.LastName: ' + model.LastName);
        //console.log('model.Password: ' + model.Password);
        //console.log('model.ConfirmPassword: ' + model.ConfirmPassword);
        //console.log(parent.ModelElements);

        const button = parent.ModelElements.Button;

        if (model.FirstName.length > 0
            && model.LastName.length > 0
            && model.Password === model.ConfirmPassword) {
            if (window.PasswordValid)
                button.classList.remove('disabled');
            else
                button.classList.add('disabled');
        }
        else
            button.classList.add('disabled');
    }
};

function ToggleMobileMenuButton()
{
    const btn = document.getElementById('MobileMenuButton');

    btn.classList.toggle('opened');
    btn.setAttribute('aria-expanded', btn.classList.contains('opened'));
}