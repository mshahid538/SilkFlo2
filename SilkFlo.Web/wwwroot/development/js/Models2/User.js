if (!SilkFlo.Models2)
    SilkFlo.Models2 = {};

SilkFlo.Models2.User = {

    ValidateEmail: function (emailAddress)
    {
        const logPrefix = 'SilkFlo.Models2.User.ValidateEmail: ';

        // Check length
        if (!emailAddress)
        {
            console.log(`${logPrefix}emailAddress parameter missing`);
            return 'Required';
        }




        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        const isValid = regex.test(emailAddress);

        if (!isValid)
        {
            return 'Invalid email address format';
        }

        return '';
    },


    // SilkFlo.Models2.User.ValidateEmailDetailed
    ValidateEmailDetailed: function (
        emailElement,
        emailInvalidId,
        urlSignIn) {

        const logPrefix = 'SilkFlo.Models2.User.ValidateEmailDetailed: ';

        // Guard Clause
        if (!emailElement) {
            console.log(`${logPrefix}emailElement parameter missing`);
            return false;
        }


        // Guard Clause
        if (!emailInvalidId) {
            console.log(`${logPrefix}emailInvalidId parameter missing`);
            return false;
        }

        let emailInvalidElement = document.getElementById(emailInvalidId);
        if (!emailInvalidElement) {
            const parent = emailElement.parentElement;

            emailInvalidElement = parent.querySelector(`[name="${emailInvalidId}"]`);

            // Guard Clause
            if (!emailInvalidElement) {
                console.log(`${logPrefix}Element with name ${emailInvalidId} missing`);
                return false;
            }
        }

        const email = emailElement.value;


        if (!email) {
            emailElement.classList.add('is-invalid');
            emailInvalidElement.innerHTML = 'Required';
            return true;
        }


        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        let isValid = regex.test(email);

        if (isValid) {
            // Is email already used?
            if (urlSignIn) {
                SilkFlo.IsEmailAlreadyUsed(email,
                    function (s) {
                        if (s === 'true') {

                            const message = `There is already an account with this email address. <a style="color: black" href="${urlSignIn}">Sign&nbsp;in</a> or <a style="color: black" href="/Account/ForgotPassword">Reset&nbsp;password</a>.`;

                            emailElement.classList.add('is-invalid');
                            emailInvalidElement.innerHTML = message;
                            isValid = true;
                        }
                        else {
                            emailElement.classList.remove('is-invalid');
                            emailInvalidElement.innerHTML = 'Required';
                            isValid = false;
                        }
                    });
            }

            emailElement.classList.remove('is-invalid');
            return isValid;
        }

        emailElement.classList.add('is-invalid');
        emailInvalidElement.innerHTML = 'Email is not correctly formatted';
        return false;
    },





    // SilkFlo.Models2.User.SendInvite
    SendInvite: function (userId, callbackFunction)
    {
        const url = `/api/Business/Tenant/SendInvite/UserId/${userId}`;

        const http = new XMLHttpRequest();
        http.open(
            'GET',
            url,
            true); // false for synchronous request


        http.setRequestHeader('Content-type',
            'application/x-www-form-urlencoded');


        http.onreadystatechange = function ()
        {
            return function ()
            {
                if (http.readyState === XMLHttpRequest.DONE && http.status === 200)
                {
                    const str = http.responseText;

                    if (callbackFunction)
                        callbackFunction ( str );
                }
            };
        }(this);


        http.send();
    }
};

SilkFlo.Models2.User.Search = {

    //SilkFlo .SearchUser
    Input_OnKeyUp: function (element, modalId)
    {
        const logPrefix = 'SilkFlo.Models2.User.Search.Input_OnKeyUp: ';

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        // Guard Clause
        if (!modalId)
        {
            console.log(`${logPrefix}modalId parameter missing`);
            return;
        }


        const modal = document.getElementById(modalId);

        if (!modal)
        {
            console.log(`${logPrefix}modal element with  id "${modal}" missing`);
            return;
        }

        const attributeName = 'silkflo-search-url';
        if (!modal.hasAttribute
            ( attributeName ))
        {
            console.log(`${logPrefix}"${attributeName}" attribute missing`);
            return;
        }


        let url = modal.getAttribute (attributeName);


        if (!url)
        {
            console.log(`${logPrefix}url missing`);
            return;

        }


        let name = 'people';
        const peopleElement = modal.querySelector(`[name="${name}"]`);

        if (!peopleElement)
        {
            console.log(`${logPrefix}element with name "${name}" missing`);
            return;
        }


        name = 'btnSelect';
        const buttonElement = modal.querySelector(`[name="${name}"]`);

        if (!buttonElement)
        {
            console.log(`${logPrefix}element with name "${name}" missing`);
            return;
        }

        buttonElement.Modal = modal;


        peopleElement.Modal = modal;
        peopleElement.UserId = null;
        peopleElement.Fullname = null;
        peopleElement.Email = null;

        url += element.value;

        SilkFlo.DataAccess.UpdateElement (
            url,
            null,
            peopleElement,
            null );
    },

    Row_OnDblClick: function (
                        element,
                        id,
                        fullname,
                        email,
                        iconPath)
    {
        const logPrefix = 'SilkFlo.Models2.User.Search.Row_OnDblClick: ';

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }



        SilkFlo.Models2.User.Search.Row_OnClick(
            element,
            id,
            fullname,
            email,
            iconPath);

        const parent = element.parentElement;

        const modal = parent.Modal;
        


        // Guard Clause
        if (!modal)
        {
            console.log(`${logPrefix}Modal property missing`);
            return;
        }


        SilkFlo.Models2.User.Search.Select(modal);
    },

    Row_OnClick: function (
                     element,
                     id,
                     fullname,
                     email,
                     iconPath)
    {
        const logPrefix = 'SilkFlo.Models2.User.Search.Row_OnClick: ';


        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }


        // Guard Clause
        if (!id)
        {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }


        // Guard Clause
        if (!fullname)
        {
            console.log(`${logPrefix}fullname parameter missing`);
            return;
        }


        // Guard Clause
        if (!email)
        {
            console.log(`${logPrefix}email parameter missing`);
            return;
        }


        const parent = element.parentElement;

        parent.UserId = id;
        parent.Fullname = fullname;
        parent.Email = email;
        parent.IconPath = iconPath;


        // Highlight
        const rows = parent.children;

        const length = rows.length;
        for (let i = 0; i < length; i++)
        {
            rows[i].style.background = '';
        }

        element.style.background = 'var(--bs-gray-lightest)';
    },

    Select_OnClick: function (element)
    {
        const logPrefix = 'SilkFlo.Models2.User.Search.Select_OnClick: ';

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }



        const modal = element.Modal;
        
        // Guard Clause
        if (!modal)
        {
            console.log(`${logPrefix}Modal property missing`);
            return;
        }


        SilkFlo.Models2.User.Search.Select (modal);
    },

    Select: function (modal)
    {
        const logPrefix = 'SilkFlo.Models2.User.Search.Select: ';

        // Guard Clause
        if (!modal)
        {
            console.log(`${logPrefix}modal parameter missing`);
            return;
        }


        const peopleElement = modal.querySelector('[name="people"]');

        // Guard Clause
        if (!peopleElement)
        {
            console.log(`${logPrefix}element with name "people" missing`);
            return;
        }




        const updateTarget = function (
            attributeName,
            modal,
            value)
        {
            const logPrefix2 = 'SilkFlo.Models2.User.Search_OnClick.UpdateTarget: ';

            // Guard Clause
            if (!attributeName) {
                console.log(`${logPrefix2}attributeName parameter missing`);
                return false;
            }

            // Guard Clause
            if (!modal) {
                console.log(`${logPrefix2}modal parameter missing`);
                return false;
            }


            // Guard Clause
            if (!value) {
                console.log(`${logPrefix2}value parameter missing`);
                return false;
            }


            // Guard Clause
            if (!modal.hasAttribute
                ( attributeName ))
            {
                console.log(`${logPrefix2}"${attributeName}" attribute missing`);
                return false;
            }

            const id = modal.getAttribute(attributeName);


            // Guard Clause
            if (!id)
            {
                console.log(`${logPrefix2}id missing`);
                return false;
            }


            const element = document.getElementById(id);

            // Guard Clause
            if (!element)
            {
                console.log(`${logPrefix2}Element with id "${id}" missing`);
                return false;
            }


            // Update Element
            if (element.localName === 'span' || element.localName === 'div')
                element.innerHTML = value;
            else if (element.localName === 'img')
                element.src = value;
            else
                element.value = value;


            return true;
        };



        let attributeName = 'silkflo-target-id-id';
        if (!updateTarget
            (attributeName, modal, peopleElement.UserId ))
        {
            console.log(`${logPrefix}Could not update the targetId element`);
            return;
        }

        attributeName = 'silkflo-target-fullname-id';
        if (!updateTarget
            (attributeName, modal, peopleElement.Fullname ))
        {
            console.log(`${logPrefix}Could not update the fullname element`);
            return;
        }


        attributeName = 'silkflo-target-status-id';
        if (!updateTarget
            (attributeName, modal, peopleElement.IconPath))
        {
            console.log(`${logPrefix}Could not update the status element`);
            return;
        }


        attributeName = 'silkflo-target-email-id';
        if (!updateTarget
            (attributeName, modal, peopleElement.Email ))
        {
            console.log(`${logPrefix}Could not update the email element`);
            return;
        }


        window.$(`#${modal.id}`).modal('hide');
    }
};